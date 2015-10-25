using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GW2_Win10.API;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GW2_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
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
                Frame.Navigate(typeof(HomePage));
                Frame.BackStack.Clear();
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

            if (App.Current.State.Session == null) return;

            Frame.Navigate(typeof(HomePage));
            Frame.BackStack.Clear();
        }
    }
}
