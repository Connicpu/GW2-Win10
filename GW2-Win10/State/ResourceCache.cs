using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GW2_Win10.API;
using GW2_Win10.Helpers;

namespace GW2_Win10.State
{
    public class ResourceCaches
    {
        private static readonly RwLock<Dictionary<Tuple<Type, Type>, object>> Caches =
            new RwLock<Dictionary<Tuple<Type, Type>, object>>();

        public static object GetCache(Type key, Type value)
        {
            using (var readLock = Caches.Read())
            {
                var caches = readLock.Value;
                var cacheKey = new Tuple<Type, Type>(key, value);
                if (caches.ContainsKey(cacheKey))
                {
                    return caches[cacheKey];
                }

                using (readLock.Upgrade())
                {
                    // Guard against multi-insert race conditions
                    if (caches.ContainsKey(cacheKey))
                    {
                        return caches[cacheKey];
                    }
                    
                    var type = typeof(ResourceCache<int, Item>).GetTypeInfo().GetGenericTypeDefinition();
                    var generic = type.MakeGenericType(key, value);
                    return caches[cacheKey] = generic.GetConstructor(new Type[0]).Invoke(new object[0]);
                }
            }
        }

        public static ResourceCache<TKey, TValue> GetCache<TKey, TValue>() where TValue : class, IApiType, new()
        {
            return (ResourceCache<TKey, TValue>) GetCache(typeof(TKey), typeof(TValue));
        }
    }

    public class ResourceCache<TKey, TValue> where TValue : class, IApiType, new()
    {
        // TODO: EF-backed local storage cache
        private readonly RwLock<Dictionary<TKey, Task<TValue>>> _memCache =
            new RwLock<Dictionary<TKey, Task<TValue>>>();

        public Task<TValue> GetItem(Session session, TKey id)
        {
            using (var readLock = _memCache.Read())
            {
                var memCache = readLock.Value;
                if (memCache.ContainsKey(id))
                {
                    return memCache[id];
                }

                using (readLock.Upgrade())
                {
                    // Guard against multi-insert race conditions
                    if (memCache.ContainsKey(id))
                    {
                        return memCache[id];
                    }

                    return memCache[id] = Retrieve(session, id);
                }
            }
        }

        public async Task<object> GetItemOpaque(Session session, TKey id)
        {
            return await GetItem(session, id);
        }

        private static async Task<TValue> Retrieve(Session session, TKey id)
        {
            try
            {
                return await session.Retrieve<TValue>(new {id});
            }
            catch
            {
                return new TValue();
            }
        }
    }
}
