using Impromptu.Views;
using Prism.Unity;
using Xamarin.Forms;

namespace Impromptu {
    public partial class App : PrismApplication {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized() {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes() {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
        }
    }
}
