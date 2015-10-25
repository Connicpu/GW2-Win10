using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GW2_Win10.API;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GW2_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage
    {
        public Account Account { get; private set; }
        public ObservableCollection<Character> Characters { get; private set; } 

        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var session = App.Current.State.Session;
            if (session == null)
            {
                Frame.Navigate(typeof(MainPage));
                return;
            }

            // Setup the initial objects
            Account = await session.Retrieve<Account>();
            Characters = new ObservableCollection<Character>();
            DataContext = this;

            // Load in the characters (in parallel)
            var loads = (
                from name in await session.Retrieve<Characters>()
                select session.Retrieve<Character>(new {id = name})
                ).ToList();
            foreach (var load in loads)
            {
                Characters.Add(await load);
            }
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            App.Current.State.LogOut();
            Frame.Navigate(typeof(MainPage));
            Frame.BackStack.Clear();
        }
    }
}
