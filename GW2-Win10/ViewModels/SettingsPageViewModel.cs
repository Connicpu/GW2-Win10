using GW2_Win10.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace GW2_Win10.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public AccountPartViewModel AccountPart { get; } = new AccountPartViewModel();
        public SyncPartViewModel SyncPart { get; } = new SyncPartViewModel();
    }

    public class AccountPartViewModel : ViewModelBase
    {
        AppState _appState;
        string newKey;

        public AccountPartViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _appState = App.Current.State;
            newKey = _appState?.Session?.ApiKey;
        }

        public string ApiKey
        {
            get { return newKey; }
            set { newKey = value; }
        }

        public async Task VerifyKey()
        {
            await _appState?.LogIn(newKey);
        }
    }

    public class SyncPartViewModel : ViewModelBase
    {

        SettingsService _service;

        public SyncPartViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _service = SettingsService.Instance;
        }

        public bool SyncOwnCharacters
        {
            get { return _service.SyncOwnCharacters; }
            set { _service.SyncOwnCharacters = value; RaisePropertyChanged(); }
        }

    }
}
