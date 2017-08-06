using System;
using Impromptu.Views;
using Prism.Mvvm;
using PropertyChanged;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Impromptu.ViewModels {
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : BindableBase {
        public List<TaskSlider> TaskList { get; set; } = new List<TaskSlider> {
            new TaskSlider {
                Name = "Study for 6.042",
                TimeLeft = 1f,
                TotalProgress = 0f,
                TodayGoal = .35f,
                Priority = 0,
                Day = 0
            },
            new TaskSlider {
                Name = "6.031 PSet",
                TimeLeft = 2f,
                TotalProgress = .5f,
                TodayGoal = 0.75f,
                Priority = 1,
                Day = 0
            },
            new TaskSlider {
                Name = "Story Two Rewrite",
                TimeLeft = 3.5f,
                TotalProgress = 1.0f,
                TodayGoal = .1f,
                Priority = 2,
                Day = 0
            },
            new TaskSlider {
                Name = "A Fourth Task",
                TimeLeft = 1.2f,
                TotalProgress = 0.7f,
                TodayGoal = .9f,
                Priority = 3,
                Day = 0
            }
        };
        public List<TaskSlider> TaskList2 { get; set; } = new List<TaskSlider> {
            new TaskSlider {
                Name = "Study for 6.006",
                TimeLeft = 0.9999f,
                TotalProgress = 0f,
                TodayGoal = .35f,
                Priority = 0,
                Day = 1
            },
            new TaskSlider {
                Name = "6.031 PSet",
                TimeLeft = 2f,
                TotalProgress = .5f,
                TodayGoal = 0.75f,
                Priority = 1,
                Day = 1
            },
            new TaskSlider {
                Name = "Story Two Rewrite",
                TimeLeft = 3.5f,
                TotalProgress = 1.0f,
                TodayGoal = .1f,
                Priority = 2,
                Day = 1
            },
            new TaskSlider {
                Name = "A Fourth Task",
                TimeLeft = 1.2f,
                TotalProgress = 0.7f,
                TodayGoal = .9f,
                Priority = 3,
                Day = 1
            }
        };

        public List<TaskSlider> TaskList3 { get; set; } = new List<TaskSlider> {
            new TaskSlider {
                Name = "Study for 6.042",
                TimeLeft = 1f,
                TotalProgress = 0f,
                TodayGoal = .35f,
                Priority = 0,
                Day = 2
            },
            new TaskSlider {
                Name = "6.031 PSet",
                TimeLeft = 2f,
                TotalProgress = .5f,
                TodayGoal = 0.75f,
                Priority = 1,
                Day = 2
            },
            new TaskSlider {
                Name = "Story Two Rewrite",
                TimeLeft = 3.5f,
                TotalProgress = 1.0f,
                TodayGoal = .1f,
                Priority = 2,
                Day = 2
            },
            new TaskSlider {
                Name = "A Fourth Task",
                TimeLeft = 1.2f,
                TotalProgress = 0.7f,
                TodayGoal = .9f,
                Priority = 3,
                Day = 2
            }
        };
        public Color BGColor { get; set; } = Color.FromHex("#ffffff");

//        public MainPageViewModel() {
//            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
//                var rng = new Random();
//                BGColor = Color.FromRgb(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
//                foreach(var task in TaskList) {
//                    task.TotalProgress = (float)rng.NextDouble();
//                    task.TimeLeft = (float) rng.NextDouble() * 9;
//                    task.FilledColor = Color.FromRgb(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
//                    task.EmptyColor = Color.FromRgb(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
//                }
//                return true;
//            });
//        }
    }
}