using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GW2_Win10.API;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GW2_Win10.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ApiKey.IsEnabled = false;
            ((Button) sender).IsEnabled = false;

            try
            {
                await App.Current.State.LogIn(ApiKey.Text);
                Frame.GoBack();
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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            App.Current.State.Load();
        }
    }
}
