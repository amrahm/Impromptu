using Impromptu.Effects.TouchHandling;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Impromptu.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskSlider {
        #region fields
        private const int DefaultSliderHeight = 60;
        public string Name { get; set; } = "Name Not Set!";
        public int Priority { get; set; } = -1;
        public int Day { get; set; }
        public float TimeLeft { get; set; }
        public float TotalProgress { get; set; }
        public float TodayGoal { get; set; }
        public Color FilledColor { get; set; }
        public Color EmptyColor { get; set; }
        private SKPathDraggable _circlePath;
        private int _height;
        private int _width;
        private float _dip;
        private float _circleRadius;
        #endregion

        #region paints
        private readonly SKPaint _colorPaint = new SKPaint {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Empty
        };

        private readonly SKPaint _colorBlurPaint = new SKPaint {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Empty,
            MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 3)
        };
        private readonly SKPaint _colorExtraBlurPaint = new SKPaint {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Empty,
            MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 10)
        };
        private readonly SKPaint _lightColorPaint = new SKPaint {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Empty
        };

        private readonly SKPaint _lightStroke = new SKPaint {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#50E0E0E0"),
            StrokeWidth = 2,
            IsAntialias = true
        };

        private readonly SKPaint _shadowBlurPaint = new SKPaint {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#60000000"),
            MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 5)
        };

        private readonly SKPaint _shadowBlurOuter = new SKPaint {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#22000000"),
            MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Outer, 10)
        };

        private readonly SKPaint _shadowHardPaint = new SKPaint {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#30000000"),
            IsAntialias = true
        };
        #endregion

        public TaskSlider() {
            InitializeComponent();
            PropertyChanged += (sender, args) => {
                switch(args.PropertyName) {
                    case nameof(Priority):
                    case nameof(Day):
                        switch(Priority) {
                            case -1:
                                break;
                            case 0:
                                FilledColor = Color.FromHex("e04242");
                                EmptyColor = Color.FromHex("ee9696");
                                break;
                            case 1:
                                FilledColor = Color.FromHex("de7b4a");
                                EmptyColor = Color.FromHex("edb79c");
                                break;
                            case 2:
                                FilledColor = Color.FromHex("dead4a");
                                EmptyColor = Color.FromHex("f0daad");
                                break;
                            default:
                                FilledColor = Color.FromHex("aead4a");
                                EmptyColor = Color.FromHex("c0aa8d");
                                break;
                        }
                        double sideMargin = Priority * 10 + Day * 20;
                        Margin = new Thickness(sideMargin, -3);
                        double barHeight = DefaultSliderHeight - (Priority * 3 + Day * 5);
                        SliderView.HeightRequest = barHeight;
                        break;
                    case nameof(TotalProgress):
                        Debug.WriteLine("totalProgress: " + TotalProgress);
                        CanvasView.InvalidateSurface();
                        break;
                    case nameof(FilledColor):
                        _colorPaint.Color = FilledColor.ToSKColor();
                        _colorBlurPaint.Color = FilledColor.ToSKColor();
                        _colorExtraBlurPaint.Color = FilledColor.ToSKColor();
                        CanvasView.InvalidateSurface();
                        break;
                    case nameof(EmptyColor):
                        _lightColorPaint.Color = EmptyColor.ToSKColor();
                        CanvasView.InvalidateSurface();
                        break;
                    case nameof(TimeLeft):
                        ETALabel.Text = "ETA: " + TimeLeft.ToString("N1") + " hour" + (Math.Abs(TimeLeft - 1) < 0.01f ? "" : "s") + " left";
                        break;
                    case nameof(Name):
                        NameLabel.Text = Name;
                        break;
                }
            };
        }

        private void OnTouchEffectAction(object sender, TouchActionEventArgs args) {
            SKPoint point = new SKPoint((float)(CanvasView.CanvasSize.Width * args.Location.X / CanvasView.Width),
                                        (float)(CanvasView.CanvasSize.Height * args.Location.Y / CanvasView.Height));
            _circlePath.OnTouchEffectAction(point, args);
            float totalProgress = (_circlePath.OffsetX - 2) / (_width - 2 * _circleRadius - 2);
            TotalProgress = Math.Max(0, Math.Min(1, totalProgress));
            CanvasView.InvalidateSurface();
        }

        private void CanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e) {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            canvas.Translate(12, 12);
            _width = e.Info.Width - 24;
            _height = e.Info.Height - 24;
            _dip = e.Info.Height / (float)CanvasView.Height / 2.5f;

            _circleRadius = _height / 2f;
            float circlePosY = _height / 2f;

            if(_circlePath == null) {
                _circlePath = new SKPathDraggable {IsDraggableY = false};
                _circlePath.AddCircle(_circleRadius, circlePosY, _circleRadius);

                _lightStroke.StrokeWidth = 2 * _dip;
                _colorBlurPaint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 3 * _dip);
                _colorExtraBlurPaint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 10 * _dip);
                _shadowBlurPaint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 5 * _dip);
                _shadowBlurOuter.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Outer, 10 * _dip);
            }

            _circlePath.OffsetX = 2 * _dip + (_width - 2 * _circleRadius - 2 * _dip) * TotalProgress;
            _circlePath.UpdatePosition();
            float circlePosX = _circlePath.TightBounds.MidX;

            SKRect emptyRect = new SKRect(0, 7 * _dip, _width, _height - 7 * _dip);
            SKRect filledRect = new SKRect(0, 7 * _dip, circlePosX, _height - 7 * _dip);

            float hardShadowXOffset = 6 * _dip;
            float hardShadowYOffset = 8 * _dip;
            SKPath path = new SKPath();
            path.AddCircle(circlePosX - 4 * _dip, circlePosY, _circleRadius);
            path.AddRect(emptyRect);

            path.Offset(new SKPoint(hardShadowXOffset, hardShadowYOffset));
            canvas.DrawPath(path, _shadowHardPaint);

            canvas.DrawRect(emptyRect, _lightColorPaint);
            canvas.DrawRect(filledRect, _colorPaint);
            
            canvas.DrawCircle(circlePosX - _circleRadius * 0.16f, circlePosY, _circleRadius - _circleRadius * 0.16f, _shadowBlurPaint);
            canvas.DrawPath(_circlePath, _lightColorPaint);
            canvas.DrawCircle(circlePosX - _circleRadius * 0.16f, circlePosY, _circleRadius - _circleRadius * 0.16f, _colorBlurPaint);
            canvas.DrawCircle(circlePosX, circlePosY, _circleRadius * 0.7f, _colorExtraBlurPaint);
            canvas.DrawOval(circlePosX - _dip, circlePosY, _circleRadius * 0.7f, _circleRadius * 0.8f, _colorExtraBlurPaint);
            canvas.DrawCircle(circlePosX, circlePosY, _circleRadius + _dip, _lightStroke);
            canvas.DrawCircle(circlePosX, circlePosY, _circleRadius * 0.75f - _dip, _shadowBlurOuter);
            canvas.DrawCircle(circlePosX, circlePosY, _circleRadius * 0.75f, _lightStroke);
        }
    }
}