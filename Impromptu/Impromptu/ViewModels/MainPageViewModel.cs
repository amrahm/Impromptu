using Prism.Mvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace Impromptu.ViewModels {
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : BindableBase {
        public Color BGColor { get; set; } = Color.Red;
    }
}