using Impromptu.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Android.Widget.ListView;

[assembly: ExportEffect(typeof(NoScrollListViewEffect), nameof(NoScrollListViewEffect))]
namespace Impromptu.Droid.Effects {
    class NoScrollListViewEffect : PlatformEffect {
        private ListView NativeList => Control as ListView;

        protected override void OnAttached() {
            if(NativeList != null) {
                NativeList.Enabled = false;
            }
        }

        protected override void OnDetached() {
        }
    }
}
