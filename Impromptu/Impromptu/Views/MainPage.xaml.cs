using System;
using Impromptu.ViewModels;
using Xamarin.Forms;

namespace Impromptu.Views {
    public partial class MainPage {
        private MainPageViewModel _vm;
        public MainPage() { InitializeComponent(); }

        protected override void OnBindingContextChanged() {
            _vm = (MainPageViewModel)BindingContext;
            Constraint xConstraint = Constraint.RelativeToParent(parent => parent.X);
            MainScheduleList.Children.Add(new DayBlock {DayName = "Today", TimeLeft = 5.5f},
                xConstraint,
                Constraint.RelativeToParent(parent => parent.Y),
                Constraint.RelativeToParent(parent => parent.Width));
            View last;
            double PosAfterLastYConstraint(RelativeLayout parent, View view) => view.Y + view.Height + 10;
            foreach(TaskSlider taskSlider in _vm.TaskList) {
                last = MainScheduleList.Children[MainScheduleList.Children.Count - 1];
                MainScheduleList.Children.Add(taskSlider, xConstraint, Constraint.RelativeToView(last, PosAfterLastYConstraint));
            }

            last = MainScheduleList.Children[MainScheduleList.Children.Count - 1];
            MainScheduleList.Children.Add(new DayBlock {DayName = "Tomorrow", TimeLeft = 4f}, xConstraint,
                Constraint.RelativeToView(last, PosAfterLastYConstraint));
            foreach(TaskSlider taskSlider in _vm.TaskList2) {
                last = MainScheduleList.Children[MainScheduleList.Children.Count - 1];
                MainScheduleList.Children.Add(taskSlider, xConstraint, Constraint.RelativeToView(last, PosAfterLastYConstraint));
            }

            last = MainScheduleList.Children[MainScheduleList.Children.Count - 1];
            MainScheduleList.Children.Add(new DayBlock {DayName = "Thursday", TimeLeft = 5.75f}, xConstraint,
                Constraint.RelativeToView(last, PosAfterLastYConstraint));
            foreach(TaskSlider taskSlider in _vm.TaskList3) {
                last = MainScheduleList.Children[MainScheduleList.Children.Count - 1];
                MainScheduleList.Children.Add(taskSlider, xConstraint, Constraint.RelativeToView(last, PosAfterLastYConstraint));
            }
        }
    }
}