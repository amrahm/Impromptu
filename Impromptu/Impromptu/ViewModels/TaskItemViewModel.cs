using Prism.Mvvm;

namespace Impromptu.ViewModels {
    public class TaskItemViewModel : BindableBase {

        public string Name { get; set; }
        public string TimeLeft { get; set; }
        public double TotalProgress { get; set; }
        public double TodayGoal { get; set; }

        public TaskItemViewModel() { }
    }
}