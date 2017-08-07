using System.Diagnostics;
using Impromptu.Effects.TouchHandling;
using Impromptu.ViewModels;
using Xamarin.Forms;

namespace Impromptu.Views {
    public partial class Tasks {
        private TasksViewModel _vm;
        private bool IsBeingDragged { get; set; }
        private long TouchId { get; set; }
        private Point OldPosition { get; set; }
        private double OffsetY { get; set; }
        public Tasks() { InitializeComponent(); }

        protected override void OnBindingContextChanged() {
            _vm = (TasksViewModel)BindingContext;
            TaskList.DayList = _vm.Days;
            TaskScrollView.BackgroundColor = _vm.BGColor;
        }

        private void OnTouchEffectAction(object sender, TouchActionEventArgs args) {
            Point point = args.Location;
            switch(args.Type) {
                case TouchActionType.Pressed:
                    if(!IsBeingDragged) {
                        IsBeingDragged = true;
                        TouchId = args.Id;
                        OldPosition = point;
                        OffsetY = TaskList.TranslationY;
                        ViewExtensions.CancelAnimations(TaskList);
                    }
                    break;

                case TouchActionType.Moved:
                    if(IsBeingDragged && TouchId == args.Id) {
                        OffsetY += point.Y - OldPosition.Y;
                        OldPosition = point;
                        TaskList.TranslateTo(TaskList.TranslationX, OffsetY, 250, Easing.CubicOut);
                    }
                    break;

                case TouchActionType.Released:
                    if(IsBeingDragged && TouchId == args.Id) {
                        IsBeingDragged = false;
                        if(OffsetY < -TaskList.Height) {
                            TaskList.TranslateTo(TaskList.TranslationX, -TaskList.Height, 500, Easing.CubicOut);
                        } else if(OffsetY > 0) {
                            TaskList.TranslateTo(TaskList.TranslationX, 0, 500, Easing.CubicOut);
                        }
                    }
                    break;
            }
        }
    }
}