using GW2_Win10.Services;
using Template10.Mvvm;

namespace GW2_Win10.ViewModels
{
    public class CharactersPageViewModel : ViewModelBase
    {

        private AccountService _accountService;

        public CharactersPageViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _accountService = AccountService.Instance;
        }

        public bool IsLoggedIn
        {
            get { return _accountService.APIKey.Length != 0; }
        }
    }
}
