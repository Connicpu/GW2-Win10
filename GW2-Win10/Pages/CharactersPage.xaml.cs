using GW2_Win10.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GW2_Win10.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharactersPage : Page
    {
        public CharactersPage()
        {
            this.InitializeComponent();
        }

        public CharactersPageViewModel ViewModel => DataContext as CharactersPageViewModel;

        private async void OnLoad(object sender, RoutedEventArgs args)
        {
            if (ViewModel?.AccountPart?.Session?.CharacterNames == null)
            {
                await App.Current.State.Session.Refresh();
                App.Current.State.Save();
            }
        }
    }
}
