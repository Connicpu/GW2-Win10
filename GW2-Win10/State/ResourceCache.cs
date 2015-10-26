using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using GW2_Win10.API;
using GW2_Win10.Helpers;
using Newtonsoft.Json;

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
            return (ResourceCache<TKey, TValue>)GetCache(typeof(TKey), typeof(TValue));
        }
    }

    public class ResourceCache<TKey, TValue> where TValue : class, IApiType, new()
    {
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
                return await RetrieveFromStorage(session, id);
            }
            catch
            {
                return new TValue();
            }
        }

        private static async Task<TValue> RetrieveFromStorage(Session session, TKey id)
        {
            try
            {
                var temp = ApplicationData.Current.LocalCacheFolder;
                var folder = await temp.GetFolderAsync(typeof(TValue).Name);
                var file = await folder.GetFileAsync($"{id}.json");
                using (var stream = await file.OpenStreamForReadAsync())
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var data = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<TValue>(data);
                }
            }
            catch
            {
                return await RetrieveAndSave(session, id);
            }
        }

        private static async Task<TValue> RetrieveAndSave(Session session, TKey id)
        {
            var value = await session.Retrieve<TValue>(new { id });
            var temp = ApplicationData.Current.LocalCacheFolder;
            var folder = await temp.CreateFolderAsync(typeof(TValue).Name, CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync($"{id}.json", CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenStreamForWriteAsync())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                var data = JsonConvert.SerializeObject(value);
                await writer.WriteAsync(data);
            }
            return value;
        }
    }
}
