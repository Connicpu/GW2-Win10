using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Services.SettingsService;

namespace GW2_Win10.Services
{
    public class AccountService
    {
        public static AccountService Instance { get; }

        static AccountService()
        {
            Instance = Instance ?? new AccountService();
        }

        private ISettingsHelper _helper;

        private AccountService()
        {
            _helper = new SettingsHelper();
        }

        public string APIKey
        {
            get { return _helper.Read($"Account.{ nameof(APIKey) }", ""); }
            set
            {
                _helper.Write($"Account.{ nameof(APIKey) }", value);
            }
        }
    }
}
