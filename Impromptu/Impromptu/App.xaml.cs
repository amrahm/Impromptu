using Impromptu.Views;
using Prism.Unity;

namespace Impromptu {
    public partial class App {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized() {
            InitializeComponent();

            NavigationService.NavigateAsync(nameof(TasksPage));
        }

        protected override void RegisterTypes() {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<TasksPage>();
        }
    }
}
