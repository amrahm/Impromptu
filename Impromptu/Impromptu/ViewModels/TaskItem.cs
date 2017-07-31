using Prism.Mvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace Impromptu.ViewModels {
    public class TaskItemViewModel {
        public string Name { get; set; } = "Name Not Set!";
        public string TimeLeft { get; set; }
        public float TotalProgress { get; set; }
        public float TodayGoal { get; set; }
        public Color FilledColor { get; set; }
        public Color EmptyColor { get; set; }
        public float SideMargins { get; set; }
    }
}