using Impromptu.Effects.TouchHandling;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Impromptu.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayBlock {
        public string DayName { get; set; } = "Name Not Set!";
        public float TimeLeft { get; set; }
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
                        TimeRemainingLabel.Text = TimeLeft.ToString("N1") + " hour" + (Math.Abs(TimeLeft - 1) < 0.01f ? "" : "s");
                        break;
                    case nameof(DayName):
                        DayNameLabel.Text = DayName;
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