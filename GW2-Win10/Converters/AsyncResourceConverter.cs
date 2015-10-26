using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using GW2_Win10.Helpers;
using GW2_Win10.State;

namespace GW2_Win10.Converters
{
    public class AsyncResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var paramType = typeof(AsyncResourceConverter).GetTypeInfo().Assembly.GetType($"GW2_Win10.API.{parameter}");

            var idProp = value.GetType().GetProperty("Id");
            var id = idProp.GetValue(value);
            var idType = id.GetType();
            if (idType.Name == "Nullable")
            {
                dynamic nid = id;
                if (!nid.HasValue)
                    return null;

                var cache = ResourceCaches.GetCache(idType.GetGenericArguments()[0], paramType);
                var cacheType = cache.GetType();
                var getItem = cacheType.GetMethod("GetItemOpaque");
                var task = (Task<object>)getItem.Invoke(cache, new object[] { App.Current.State.Session, nid.Value });
                return new AsyncViewValue<object>(task);
            }
            else
            {
                var cache = ResourceCaches.GetCache(idType, paramType);
                var cacheType = cache.GetType();
                var getItem = cacheType.GetMethod("GetItemOpaque");
                var task = (Task<object>)getItem.Invoke(cache, new[] { App.Current.State.Session, id });
                return new AsyncViewValue<object>(task);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
