using GW2_Win10.API;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace GW2_Win10
{
    public class AppState
    {
        public Session Session { get; private set; }

        public async Task LogIn(string apiKey)
        {
            Session = new Session(apiKey);
            await Session.LoadInfo();
            await Save();
        }

        public async Task LogOut()
        {
            Session = null;
            await Save();
        }

        public async Task Load()
        {
            var roaming = ApplicationData.Current.RoamingFolder;
            try
            {
                var file = await roaming.GetFileAsync("settings.json");
                using (var stream = await file.OpenStreamForReadAsync())
                using (var reader = new StreamReader(stream))
                {
                    var data = await reader.ReadToEndAsync();
                    Session = JsonConvert.DeserializeObject<Session>(data);
                }
            }
            catch
            {
                Session = null;
            }
        }

        public async Task Save()
        {
            var roaming = ApplicationData.Current.RoamingFolder;
            if (Session != null)
            {
                var data = JsonConvert.SerializeObject(Session);
                var file = await roaming.CreateFileAsync("settings.json", CreationCollisionOption.ReplaceExisting);
                using (var stream = await file.OpenStreamForWriteAsync())
                using (var writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(data);
                    await writer.FlushAsync();
                }
            }
            else
            {
                try
                {
                    var file = await roaming.GetFileAsync("settings.json");
                    await file.DeleteAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}