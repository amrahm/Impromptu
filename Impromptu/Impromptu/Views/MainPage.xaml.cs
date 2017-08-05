using System.Collections.Generic;
using Impromptu.ViewModels;
using Xamarin.Forms;

namespace Impromptu.Views {
    public partial class MainPage {
        private MainPageViewModel _vm;
        public MainPage() { InitializeComponent(); }

        protected override void OnBindingContextChanged() {
            _vm = (MainPageViewModel)BindingContext;
            MainScheduleList.Children.Add(new DayBlock() {
                DayName = "Today",
                TimeLeft = 5.5f
            });
            foreach(TaskSlider taskSlider in _vm.TaskList) {
                MainScheduleList.Children.Add(taskSlider);
            }
            MainScheduleList.Children.Add(new DayBlock() {
                DayName = "Tomorrow",
                TimeLeft = 5.5f
            });
            foreach(TaskSlider taskSlider in _vm.TaskList) {
//                TaskSlider copy = taskSlider.MemberwiseClone();
                taskSlider.Day = 1;
                MainScheduleList.Children.Add(taskSlider);
            }
        }
    }
}