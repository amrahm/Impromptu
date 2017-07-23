using System.Collections.Generic;
using Impromptu.ViewModels;
using Xamarin.Forms;

namespace Impromptu.Views {
    public partial class MainPage {
        public MainPage() {
            InitializeComponent();

            MainScheduleList.ItemSelected += (sender, e) => { ((ListView)sender).SelectedItem = null; };
        }
    }
}