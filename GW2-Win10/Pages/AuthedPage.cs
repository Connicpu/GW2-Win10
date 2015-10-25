using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GW2_Win10.Pages
{
    public class AuthedPage : Page
    {
        public AuthedPage()
            : base()
        {
            Loaded += OnAuthLoad;
        }

        void OnAuthLoad(object sender, RoutedEventArgs args)
        {
            Loaded -= OnAuthLoad;

            var session = App.Current.State.Session;
            if (session != null)
            {
                Frame.Navigate(typeof(LoginPage));
            }
        }
    }
}
