using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GW2_Win10
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public AppShell(NavigationService navService)
        {
            this.InitializeComponent();
            Menu.NavigationService = navService;
        }
    }
}
