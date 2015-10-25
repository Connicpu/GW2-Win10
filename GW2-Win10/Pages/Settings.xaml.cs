using GW2_Win10.API;
using GW2_Win10.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GW2_Win10.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        public SettingsPageViewModel ViewModel => DataContext as SettingsPageViewModel;
        
        private async void SignInClick(object sender, RoutedEventArgs e)
        {
            ApiKey.IsEnabled = false;
            ((Button)sender).IsEnabled = false;

            try
            {
                await App.Current.State.LogIn(ApiKey.Text);
                ViewModel.AccountPart.Toggled();
            }
            catch (ApiException)
            {
                var dialog = new MessageDialog("Are you sure that's your API key?", "Couldn't log in");
                await dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.Message, "An error occurred");
                await dialog.ShowAsync();
            }

            ApiKey.IsEnabled = true;
            ((Button)sender).IsEnabled = true;
        }

        private void SignOutClick(object sender, RoutedEventArgs e)
        {
            App.Current.State.LogOut();
            ViewModel.AccountPart.Toggled();
        }

        private async void CreateNewKey(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://account.arena.net/applications/create"));
        }
    }
}
