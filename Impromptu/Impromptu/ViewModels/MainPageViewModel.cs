using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Impromptu.Views;
using PropertyChanged;
using Xamarin.Forms;

namespace Impromptu.ViewModels {
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : BindableBase {
        public List<TaskSlider> TaskList { get; set; } = new List<TaskSlider> {
            new TaskItemViewModel {
                Name = "Study for 6.042",
                TimeLeft = "ETA: 2 Hours",
                TotalProgress = 0f,
                TodayGoal = .35f,
                FilledColor = Color.FromHex("E04242"),
                EmptyColor = Color.FromHex("EE9696")
            },
            new TaskItemViewModel {
                Name = "6.031 PSet",
                TimeLeft = "ETA: 1 Hour",
                TotalProgress = .5f,
                TodayGoal = 0.75f,
                FilledColor = Color.FromHex("de7b4a"),
                EmptyColor = Color.FromHex("edb79c")
            },
            new TaskItemViewModel {
                Name = "Story Two Rewrite",
                TimeLeft = "ETA: 2 Hours",
                TotalProgress = 1.0f,
                TodayGoal = .1f,
                FilledColor = Color.FromHex("dead4a"),
                EmptyColor = Color.FromHex("f0daad")
            }
        };
        public Color BGColor { get; set; } = Color.FromHex("#FFFFFF");

        public MainPageViewModel() {
            Device.StartTimer(TimeSpan.FromSeconds(4), () => {
                var rng = new Random();
//                BGColor = Color.FromRgb(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
                foreach(var task in TaskList) {
//                    task.TotalProgress = (float)rng.NextDouble();
                    task.TimeLeft = "ETA: " + rng.Next(0, 9) + " Hours";
//                    task.FilledColor = Color.FromRgb(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
//                    task.EmptyColor = Color.FromRgb(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
                }
                return true;
            });
        }
    }
}