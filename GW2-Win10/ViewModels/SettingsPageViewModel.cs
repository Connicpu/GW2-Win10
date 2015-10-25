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
        AccountService _service;

        public AccountPartViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _service = AccountService.Instance;
        }

        public string APIKey
        {
            get { return _service.APIKey; }
            set { _service.APIKey = value; RaisePropertyChanged(); }
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
