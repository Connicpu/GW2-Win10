using GW2_Win10.API;
using GW2_Win10.Pages;
using GW2_Win10.Services;
using System.Threading.Tasks;
using Template10.Common;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace GW2_Win10
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : BootStrapper
    {
        public new static App Current => (App)BootStrapper.Current;

        public AppState State { get; } = new AppState();

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            InitializeComponent();

            State.Load().Wait();
        }

        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
            Window.Current.Content = new AppShell(nav);
            return Task.FromResult<object>(null);
        }

        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            NavigationService.Navigate(typeof(HomePage));
            return Task.FromResult<object>(null);
        }
    }
}
