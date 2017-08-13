using Android.Views;
using Impromptu.Droid.Views;
using Impromptu.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TaskScrollView), typeof(TaskScrollViewRenderer))]

namespace Impromptu.Droid.Views {
    public class TaskScrollViewRenderer : ScrollViewRenderer {
        protected override void OnElementChanged(VisualElementChangedEventArgs e) {
            base.OnElementChanged(e);
            TaskScrollView taskScrollView = (TaskScrollView)e.NewElement;

            taskScrollView.InputTransparent = !taskScrollView.IsScrollEnabled;
            VerticalScrollBarEnabled = taskScrollView.IsVerticalScrollbarEnabled;
            HorizontalScrollBarEnabled = taskScrollView.IsHorizontalScrollbarEnabled;

            if(taskScrollView.IsNativeBouncyEffectEnabled) {
                OverScrollMode = OverScrollMode.Always;
            }


            taskScrollView.PropertyChanged += (sender, args) => {
                switch(args.PropertyName) {
                    case nameof(taskScrollView.IsScrollEnabled):
                        taskScrollView.InputTransparent = !taskScrollView.IsScrollEnabled;
                        break;
                }
            };
        }
    }
}