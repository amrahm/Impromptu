using Impromptu.ViewModels;

namespace Impromptu.Views {
    public partial class MainPage {
        private MainPageViewModel _vm;
        public MainPage() { InitializeComponent(); }

        protected override void OnBindingContextChanged() {
            _vm = (MainPageViewModel)BindingContext;
        }

    }
}