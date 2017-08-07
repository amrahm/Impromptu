using System;
using System.Collections.Generic;
using Impromptu.Views;
using Prism.Mvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace Impromptu.ViewModels {
    [AddINotifyPropertyChangedInterface]
    public class TasksViewModel : BindableBase {
        public Color BGColor { get; set; } = Color.White;

        public List<DayBlock> Days { get; set; } = new List<DayBlock> {
            new DayBlock {
                DayName = "Today",
                TimeLeft = 5.5f,
                Day = 0,
                Tasks = new List<TaskSlider> {
                    new TaskSlider {
                        Name = "Study for 6.042",
                        TimeLeft = 1f,
                        TotalProgress = 0f,
                        TodayGoal = .35f
                    },
                    new TaskSlider {
                        Name = "6.031 PSet",
                        TimeLeft = 2f,
                        TotalProgress = .5f,
                        TodayGoal = 0.75f
                    },
                    new TaskSlider {
                        Name = "Story Two Rewrite",
                        TimeLeft = 3.5f,
                        TotalProgress = 1.0f,
                        TodayGoal = 1f
                    },
                    new TaskSlider {
                        Name = "A Fourth Task",
                        TimeLeft = 1.2f,
                        TotalProgress = 0.7f,
                        TodayGoal = .9f
                    }
                }
            },
            new DayBlock {
                DayName = "Tomorrow",
                TimeLeft = 4f,
                Day = 1,
                Tasks = new List<TaskSlider> {
                    new TaskSlider {
                        Name = "Study for 6.042",
                        TimeLeft = 1f,
                        TotalProgress = 0.35f,
                        TodayGoal = 0.7f
                    },
                    new TaskSlider {
                        Name = "6.031 PSet",
                        TimeLeft = 2f,
                        TotalProgress = .75f,
                        TodayGoal = 1.0f
                    },
                    new TaskSlider {
                        Name = "Story Two Rewrite",
                        TimeLeft = 3.5f,
                        TotalProgress = 0.6f,
                        TodayGoal = 0.9f
                    },
                    new TaskSlider {
                        Name = "A Fourth Task",
                        TimeLeft = 1.2f,
                        TotalProgress = 0.7f,
                        TodayGoal = .9f
                    }
                }
            },
            new DayBlock {
                DayName = "Thursday",
                TimeLeft = 5.88f,
                Day = 2,
                Tasks = new List<TaskSlider> {
                    new TaskSlider {
                        Name = "Study for 6.042",
                        TimeLeft = 1f,
                        TotalProgress = 0f,
                        TodayGoal = .35f
                    },
                    new TaskSlider {
                        Name = "6.031 PSet",
                        TimeLeft = 2f,
                        TotalProgress = .5f,
                        TodayGoal = 0.75f
                    },
                    new TaskSlider {
                        Name = "Story Two Rewrite",
                        TimeLeft = 3.5f,
                        TotalProgress = 0.2f,
                        TodayGoal = .4f
                    },
                    new TaskSlider {
                        Name = "A Fourth Task",
                        TimeLeft = 1.2f,
                        TotalProgress = 0.7f,
                        TodayGoal = .9f
                    }
                }
            }
        };

//        public TasksViewModel() {
//            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
//                var rng = new Random();
//                BGColor = Color.FromRgb(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
//                foreach(var task in TaskLi) {
//                    task.TotalProgress = (float)rng.NextDouble();
//                    task.TimeLeft = (float)rng.NextDouble() * 9;
//                    task.FilledColor = Color.FromRgb(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
//                    task.EmptyColor = Color.FromRgb(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
//                }
//                return true;
//            });
//        }
    }
}