using System;
using Impromptu.ViewModels;
using Prism.Events;

namespace Impromptu.Views {
    public partial class TasksPage {
        private TasksViewModel _vm;

        public TasksPage() {
            InitializeComponent();
            TaskList.TouchedEvent += WasTouched;
        }
        public void WasTouched(object sender, Boolean shouldPassThrough) {
            //Change Transparency
        }

        protected override void OnBindingContextChanged() {
            _vm = (TasksViewModel)BindingContext;
            TaskList.DayList = _vm.Days;
            TaskScrollView.BackgroundColor = _vm.BGColor;
        }
    }
}