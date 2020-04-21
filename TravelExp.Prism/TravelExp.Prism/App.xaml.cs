using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using TravelExp.Common.Helpers;
using TravelExp.Common.Services;
using TravelExp.Prism.ViewModels;
using TravelExp.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TravelExp.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjM4ODM1QDMxMzgyZTMxMmUzMGFWVDdycTNLQUR3V2Z1YWkrKzBDcFdmbURZZWl4RjdRVnhWSU43RTZTOVk9");
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TripsPage, TripsPageViewModel>();
            containerRegistry.RegisterForNavigation<TripDetailsPage, TripDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<TravelExpMasterDetailPage, TravelExpMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<AddTripPage, AddTripPageViewModel>();
            containerRegistry.RegisterForNavigation<AddTripDetailPage, AddTripDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
        }
    }
}
