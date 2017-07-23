using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Impromptu.ViewModels {
    public class MainPageViewModel : BindableBase {
        private List<TaskItemViewModel> _taskList = new List<TaskItemViewModel> {
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
        public List<TaskItemViewModel> TaskList { get => _taskList; set => SetProperty(ref _taskList, value); }


        public MainPageViewModel() {
            foreach(TaskItemViewModel task in _taskList) {
                task.Name = "SIJF";
            }
        }
    }
}