using Impromptu.ViewModels;

namespace Impromptu.Views {
    public partial class Tasks {
        private TasksViewModel _vm;
        public Tasks() { InitializeComponent(); }

        protected override void OnBindingContextChanged() {
            _vm = (TasksViewModel)BindingContext;
            TaskList.DayList = _vm.Days;
            TaskScrollView.BackgroundColor = _vm.BGColor;
        }
    }
}