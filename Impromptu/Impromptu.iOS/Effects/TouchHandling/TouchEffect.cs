using System.Linq;
using Impromptu.iOS.Effects.TouchHandling;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("XamarinDocs")]
[assembly: ExportEffect(typeof(TouchEffect), "TouchEffect")]

namespace Impromptu.iOS.Effects.TouchHandling {
    public class TouchEffect : PlatformEffect {
        private UIView _view;
        private TouchRecognizer _touchRecognizer;

        protected override void OnAttached() {
            // Get the iOS UIView corresponding to the Element that the effect is attached to
            _view = Control ?? Container;

            // Get access to the TouchEffect class in the PCL
            Impromptu.Effects.TouchHandling.TouchEffect effect =
                (Impromptu.Effects.TouchHandling.TouchEffect)Element.Effects.FirstOrDefault(e => e is Impromptu.Effects.TouchHandling.TouchEffect);

            if(effect != null && _view != null) {
                // Create a TouchRecognizer for this UIView
                _touchRecognizer = new TouchRecognizer(Element, _view, effect);
                _view.AddGestureRecognizer(_touchRecognizer);
            }
        }

        protected override void OnDetached() {
            if(_touchRecognizer != null) {
                // Clean up the TouchRecognizer object
                _touchRecognizer.Detach();

                // Remove the TouchRecognizer from the UIView
                _view.RemoveGestureRecognizer(_touchRecognizer);
            }
        }
    }
}