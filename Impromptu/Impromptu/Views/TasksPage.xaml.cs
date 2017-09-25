using System;
using System.Diagnostics;
using Impromptu.ViewModels;
using Prism.Events;

namespace Impromptu.Views {
    public partial class TasksPage {
        private TasksPageViewModel _vm;

        public TasksPage() {
            InitializeComponent();
            TaskList.TouchedEvent += WasTouched;
        }
        public void WasTouched(object sender, Boolean shouldPassThrough) {
            //Change Transparency
            TaskScrollView.InputTransparent = shouldPassThrough;
        }

        protected override void OnBindingContextChanged() {
            _vm = (TasksPageViewModel)BindingContext;
            TaskList.DayList = _vm.Days;
            TaskScrollView.BackgroundColor = _vm.BGColor;
        }
    }
}