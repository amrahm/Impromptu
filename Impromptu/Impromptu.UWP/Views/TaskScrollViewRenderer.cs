using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Impromptu.UWP.Views;
using Impromptu.Views;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TaskScrollView), typeof(TaskScrollViewRenderer))]

namespace Impromptu.UWP.Views {
    public class TaskScrollViewRenderer : ScrollViewRenderer {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            TaskScrollView taskScrollView = (TaskScrollView)sender;

            Control.IsEnabled = taskScrollView.IsScrollEnabled;
            Control.VerticalScrollBarVisibility = taskScrollView.IsVerticalScrollbarEnabled
                ? ScrollBarVisibility.Visible
                : ScrollBarVisibility.Hidden;
            Control.HorizontalScrollBarVisibility = taskScrollView.IsHorizontalScrollbarEnabled
                ? ScrollBarVisibility.Visible
                : ScrollBarVisibility.Hidden;

            taskScrollView.PropertyChanged += (s, args) => {
                switch(args.PropertyName) {
                    case nameof(taskScrollView.IsScrollEnabled):
                        Control.IsEnabled = taskScrollView.IsScrollEnabled;
                        break;
                }
            };
        }
    }
}