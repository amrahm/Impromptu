using Impromptu.UWP.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using ListView = Windows.UI.Xaml.Controls.ListView;

[assembly: ExportEffect(typeof(NoScrollListViewEffect), nameof(NoScrollListViewEffect))]
namespace Impromptu.UWP.Effects {
    class NoScrollListViewEffect : PlatformEffect {
        private ListView NativeList => Control as ListView;

        protected override void OnAttached() {
            if(NativeList != null) {
                NativeList.IsEnabled = false;
            }
        }

        protected override void OnDetached() {
        }
    }
}
