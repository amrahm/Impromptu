using Prism.Mvvm;

namespace Impromptu.ViewModels {
    public class TaskItemViewModel : BindableBase {
        private string _name = "Name Not Set!";
        private string _timeLeft;
        private double _totalProgress;
        private double _todayGoal;
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string TimeLeft { get => _timeLeft; set => SetProperty(ref _timeLeft, value); }
        public double TotalProgress { get => _totalProgress; set => SetProperty(ref _totalProgress, value); }
        public double TodayGoal { get => _todayGoal; set => SetProperty(ref _todayGoal, value); }
    }
}