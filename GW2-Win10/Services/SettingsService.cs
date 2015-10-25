using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Services.SettingsService;

namespace GW2_Win10.Services
{
    public class SettingsService
    {
        public static SettingsService Instance { get; }

        static SettingsService()
        {
            Instance = Instance ?? new SettingsService();
        }

        ISettingsHelper _helper;

        private SettingsService()
        {
            _helper = new SettingsHelper();
        }
    }
}
