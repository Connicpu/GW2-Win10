using System.Threading.Tasks;
using Windows.Storage;
using GW2_Win10.API;
using Newtonsoft.Json;

namespace GW2_Win10
{
    public class AppState
    {
        public Session Session { get; private set; }

        public async Task LogIn(string apiKey)
        {
            Session = new Session(apiKey);
            await Session.LoadInfo();
            Save();
        }

        public void LogOut()
        {
            Session = null;
            Save();
        }

        public void Load()
        {
            var settings = ApplicationData.Current.RoamingSettings;
            var data = settings.Values["Session"] as string;
            Session = data != null ? JsonConvert.DeserializeObject<Session>(data) : null;
        }

        public void Save()
        {
            var settings = ApplicationData.Current.RoamingSettings;
            settings.Values["Session"] = Session != null ? JsonConvert.SerializeObject(Session) : null;
        }
    }
}