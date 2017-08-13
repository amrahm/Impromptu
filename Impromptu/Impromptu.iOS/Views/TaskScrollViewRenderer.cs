using System.ComponentModel;
using Impromptu.iOS.Views;
using Impromptu.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TaskScrollView), typeof(TaskScrollViewRenderer))]

namespace Impromptu.iOS.Views {
    public class TaskScrollViewRenderer : ScrollViewRenderer {
        protected override void OnElementChanged(VisualElementChangedEventArgs e) {
            base.OnElementChanged(e);
            TaskScrollView taskScrollView = (TaskScrollView)e.NewElement;

            ScrollEnabled = taskScrollView.IsScrollEnabled;
            ShowsVerticalScrollIndicator = taskScrollView.IsVerticalScrollbarEnabled;
            ShowsHorizontalScrollIndicator = taskScrollView.IsHorizontalScrollbarEnabled;

            if(taskScrollView.IsNativeBouncyEffectEnabled) {
                Bounces = true;
                AlwaysBounceVertical = true;
            }

            taskScrollView.PropertyChanged += (sender, args) => {
                switch(args.PropertyName) {
                    case nameof(taskScrollView.IsScrollEnabled):
                        ScrollEnabled = taskScrollView.IsScrollEnabled;
                        break;
                }
            };
        }
    }
}