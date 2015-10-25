using GW2_Win10.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace GW2_Win10.ViewModels
{
    public class AccountSettingsPageViewModel : ViewModelBase
    {

        public APIKeyPartViewModel APIKeyPart { get; } = new APIKeyPartViewModel();

    }

    public class APIKeyPartViewModel : ViewModelBase
    {
        AccountService _service;

        public APIKeyPartViewModel()
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
}
