using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace GW2_Win10.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharacterPage
    {
        public CharacterPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var character = (Character) e.Parameter;
            if (character != null)
            {
                DataContext = character;
                PageHeader.Text = character.Name;
            }
            base.OnNavigatedTo(e);
        }
    }
}
