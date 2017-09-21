using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Impromptu.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskList {
        public List<DayBlock> DayList { get; set; }
        public event EventHandler<Boolean> TouchedEvent;
        public void WasTouched(object sender, Boolean shouldPassThrough) { //Subscribe this method to all dayblock touched events
            TouchedEvent?.Invoke(this, shouldPassThrough);
        }

        public TaskList() {
            InitializeComponent();

            PropertyChanged += (sender, args) => {
                if(MainScheduleList != null) {
                    switch(args.PropertyName) {
                        case nameof(DayList):
                            Constraint xConstraint = Constraint.RelativeToParent(parent => parent.X);
                            MainScheduleList.Children.Add(DayList[0],
                                xConstraint,
                                Constraint.RelativeToParent(parent => parent.Y),
                                Constraint.RelativeToParent(parent => parent.Width));
                            for(int i = 1; i < DayList.Count; i++) {
                                View last = MainScheduleList.Children[MainScheduleList.Children.Count - 1];
                                double PosAfterLastYConstraint(RelativeLayout parent, View view) => view.Y + view.Height + 20;
                                MainScheduleList.Children.Add(DayList[i], xConstraint, Constraint.RelativeToView(last, PosAfterLastYConstraint));
                            }
                            break;
                    }
                }
            };
        }
    }
}