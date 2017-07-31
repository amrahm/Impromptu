using Impromptu.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(NoScrollListViewEffect), nameof(NoScrollListViewEffect))]
namespace Impromptu.iOS.Effects {
    class NoScrollListViewEffect : PlatformEffect {
        private UITableView NativeList => Control as UITableView;

        protected override void OnAttached() {
            if(NativeList != null) {
                NativeList.ScrollEnabled = false;
            }
        }

        protected override void OnDetached() {
        }
    }
}
