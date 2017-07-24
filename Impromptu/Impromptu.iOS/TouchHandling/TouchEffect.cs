using System.Linq;
using Impromptu.iOS.TouchHandling;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("XamarinDocs")]
[assembly: ExportEffect(typeof(TouchEffect), "TouchEffect")]

namespace Impromptu.iOS.TouchHandling {
    public class TouchEffect : PlatformEffect {
        UIView view;
        TouchRecognizer touchRecognizer;

        protected override void OnAttached() {
            // Get the iOS UIView corresponding to the Element that the effect is attached to
            view = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the PCL
            Impromptu.TouchHandling.TouchEffect effect =
                (Impromptu.TouchHandling.TouchEffect)Element.Effects.FirstOrDefault(e => e is Impromptu.TouchHandling.TouchEffect);

            if(effect != null && view != null) {
                // Create a TouchRecognizer for this UIView
                touchRecognizer = new TouchRecognizer(Element, view, effect);
                view.AddGestureRecognizer(touchRecognizer);
            }
        }

        protected override void OnDetached() {
            if(touchRecognizer != null) {
                // Clean up the TouchRecognizer object
                touchRecognizer.Detach();

                // Remove the TouchRecognizer from the UIView
                view.RemoveGestureRecognizer(touchRecognizer);
            }
        }
    }
}