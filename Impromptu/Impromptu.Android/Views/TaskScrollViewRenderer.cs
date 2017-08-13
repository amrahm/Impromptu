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

            VerticalScrollBarEnabled = ((TaskScrollView)e.NewElement).IsVerticalScrollbarEnabled;
            HorizontalScrollBarEnabled = ((TaskScrollView)e.NewElement).IsHorizontalScrollbarEnabled;

            if(((TaskScrollView)e.NewElement).IsNativeBouncyEffectEnabled) {
                OverScrollMode = OverScrollMode.Always;
            }
        }
    }
}