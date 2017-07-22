using System.Collections.Generic;
using Impromptu.ViewModels;
using Xamarin.Forms;

namespace Impromptu.Views {
    public partial class MainPage {
        public MainPage() {
            InitializeComponent();

            MainScheduleList.ItemsSource = new List<TaskItemViewModel> {
                new TaskItemViewModel {
                    Name = "Study for 6.042",
                    TimeLeft = "ETA: 2 Hours",
                    TotalProgress = .25,
                    TodayGoal = .35
                },
                new TaskItemViewModel {
                    Name = "6.031 PSet",
                    TimeLeft = "ETA: 1 Hour",
                    TotalProgress = .5,
                    TodayGoal = 0.75
                },
                new TaskItemViewModel {
                    Name = "Story Two Rewrite",
                    TimeLeft = "ETA: 2 Hours",
                    TotalProgress = 0.0,
                    TodayGoal = .1
                },
                new TaskItemViewModel {
                    Name = "6.006 PSet",
                    TimeLeft = "ETA: 2 Hours",
                    TotalProgress = 0.0,
                    TodayGoal = 0.2
                },
                new TaskItemViewModel {
                    Name = "Story Two Rewrite But Really Long",
                    TimeLeft = "ETA: 3 Hours",
                    TotalProgress = 0.1,
                    TodayGoal = 0.25
                }
            };

            MainScheduleList.ItemSelected += (sender, e) => { ((ListView)sender).SelectedItem = null; };
        }
    }
}