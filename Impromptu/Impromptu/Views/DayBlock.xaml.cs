using Impromptu.Effects.TouchHandling;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Impromptu.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayBlock {
        public string DayName { get; set; } = "Name Not Set!";
        public float TimeLeft { get; set; }
        public int Day { get; set; }
        public List<TaskSlider> Tasks { get; set; }
        private int _height;
        private int _width;

        #region paints
        private readonly SKPaint _colorPaint = new SKPaint {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#f5efe0")
        };

        private readonly SKPaint _shadowHardPaint = new SKPaint {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#20000000"),
            IsAntialias = true
        };
        #endregion

        public DayBlock() {
            InitializeComponent();
            PropertyChanged += (sender, args) => {
                switch(args.PropertyName) {
                    case nameof(TimeLeft):
                        TimeRemainingHeader.Text = Day == 0 ? "Time Remaining:" : "Total Time:";
                        TimeRemainingLabel.Text = TimeLeft.ToString("#.#") + " hour" + (TimeLeft.ToString("#.#") == "1" ? "" : "s");
                        break;
                    case nameof(DayName):
                        DayNameLabel.Text = DayName;
                        break;
                    case nameof(Day):
                    case nameof(Tasks):
                        TimeRemainingHeader.Text = Day == 0 ? "Time Remaining:" : "Total Time:";
                        TasksLayout.Children.Clear();
                        Constraint xConstraint = Constraint.Constant(0);
                        TasksLayout.Children.Add(new BoxView {Color = Color.FromHex("#60000000"), HeightRequest = 1, Margin = 0},
                            xConstraint,
                            Constraint.Constant(0),
                            Constraint.RelativeToParent(parent => parent.Width));
                        if(Tasks != null) {
                            for(int i = 0; i < Tasks.Count; i++) {
                                TaskSlider taskSlider = Tasks[i];
                                taskSlider.Day = Day;
                                taskSlider.Priority = i;
                                View last = TasksLayout.Children[TasksLayout.Children.Count - 1];
                                double PosAfterLastYConstraint(RelativeLayout parent, View view) => view.Y + view.Height + 10;
                                TasksLayout.Children.Add(taskSlider, xConstraint, Constraint.RelativeToView(last, PosAfterLastYConstraint));
                            }
                        }
                        break;
                }
            };
        }

        private void OnTouchEffectAction(object sender, TouchActionEventArgs args) {
            SKPoint point = new SKPoint((float)(CanvasView.CanvasSize.Width * args.Location.X / CanvasView.Width),
                (float)(CanvasView.CanvasSize.Height * args.Location.Y / CanvasView.Height));
            CanvasView.InvalidateSurface();
        }

        private void CanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e) {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            _width = e.Info.Width;
            _height = e.Info.Height;
            canvas.DrawRect(new SKRect(_height * 0.07f, 0, _width, _height), _shadowHardPaint);
            canvas.DrawRect(new SKRect(0, 0, _width, _height - _height * 0.07f), _colorPaint);
        }
    }
}