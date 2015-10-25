using GW2_Win10.Services;
using Template10.Mvvm;

namespace GW2_Win10.ViewModels
{
    public class CharactersPageViewModel : ViewModelBase
    {
        private AppState _appState;

        public CharactersPageViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                _appState = App.Current.State;
        }

        public bool IsLoggedIn
        {
            get { return _appState?.Session != null; }
        }
    }
}
