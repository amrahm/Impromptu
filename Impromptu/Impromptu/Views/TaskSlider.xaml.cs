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
        private bool _shouldRedrawCircle = true;
        private SKPathDraggable _circlePath;
        private int _height;
        private int _width;
        private float _dip;
        private float _circleRadius;
        private SKSurface _circleSurface;
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
        private SKRect _sliderBox;
        #endregion

        public TaskSlider() {
            InitializeComponent();
            PropertyChanged += (sender, args) => {
                switch(args.PropertyName) {
                    case nameof(Priority):
                    case nameof(Day):
                        switch(Priority) {
                            case -1:
                                throw new ArgumentException("Priority not set for task " + Name);
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
                        _shouldRedrawCircle = true;
                        CanvasView.InvalidateSurface();
                        break;
                    case nameof(TotalProgress):
                        CanvasView.InvalidateSurface();
                        break;
                    case nameof(FilledColor):
                        _colorPaint.Color = FilledColor.ToSKColor();
                        _colorBlurPaint.Color = FilledColor.ToSKColor();
                        _colorExtraBlurPaint.Color = FilledColor.ToSKColor();
                        _shouldRedrawCircle = true;
                        CanvasView.InvalidateSurface();
                        break;
                    case nameof(EmptyColor):
                        _lightColorPaint.Color = EmptyColor.ToSKColor();
                        _shouldRedrawCircle = true;
                        CanvasView.InvalidateSurface();
                        break;
                    case nameof(TimeLeft):
                        ETALabel.Text = "ETA: " + TimeLeft.ToString("#.#") + " hour" + (TimeLeft.ToString("#.#") == "1" ? "" : "s");
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
            //Solve the _circlePath.OffsetX equation below for TotalProgress
            float totalProgress = (_circlePath.OffsetX - 3 * _dip) / (_width - 2 * _circleRadius - 3 * _dip);
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

            if(_shouldRedrawCircle) {
                _shouldRedrawCircle = false;
                _circlePath = new SKPathDraggable {IsDraggableY = false};
                float circleOffset = _circleRadius + 5 * _dip;
                _circlePath.AddCircle(circleOffset, circleOffset, _circleRadius);

                _lightStroke.StrokeWidth = 2 * _dip;
                _colorBlurPaint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 3 * _dip);
                _colorExtraBlurPaint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 10 * _dip);
                _shadowBlurPaint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 5 * _dip);
                _shadowBlurOuter.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Outer, 10 * _dip);
                _circleSurface = SKSurface.Create(_height + 10, _height + 10, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
                SKCanvas cirCanv = _circleSurface.Canvas;
                cirCanv.Clear();
                cirCanv.DrawCircle(circleOffset - _circleRadius * 0.16f, circleOffset, _circleRadius - _circleRadius * 0.16f, _shadowBlurPaint);
                cirCanv.DrawPath(_circlePath, _lightColorPaint);
                cirCanv.DrawCircle(circleOffset - _circleRadius * 0.16f, circleOffset, _circleRadius - _circleRadius * 0.16f, _colorBlurPaint);
                cirCanv.DrawCircle(circleOffset, circleOffset, _circleRadius * 0.7f, _colorExtraBlurPaint);
                cirCanv.DrawOval(circleOffset - _dip, circleOffset, _circleRadius * 0.7f, _circleRadius * 0.8f, _colorExtraBlurPaint);
                cirCanv.DrawCircle(circleOffset, circleOffset, _circleRadius + _dip, _lightStroke);
                cirCanv.DrawCircle(circleOffset, circleOffset, _circleRadius * 0.75f - _dip, _shadowBlurOuter);
                cirCanv.DrawCircle(circleOffset, circleOffset, _circleRadius * 0.75f, _lightStroke);

                _sliderBox = new SKRect(0, 7 * _dip, _width, _height - 7 * _dip);
            }

            _circlePath.OffsetX = 3 * _dip + (_width - 2 * _circleRadius - 3 * _dip) * TotalProgress;
            _circlePath.UpdatePosition();
            float circlePosX = _circlePath.TightBounds.MidX;

            float hardShadowXOffset = 6 * _dip;
            float hardShadowYOffset = 8 * _dip;
            SKPath path = new SKPath();
            path.AddCircle(circlePosX - 4 * _dip, circlePosY, _circleRadius);
            path.AddRect(_sliderBox);

            path.Offset(new SKPoint(hardShadowXOffset, hardShadowYOffset));
            canvas.DrawPath(path, _shadowHardPaint);

            canvas.DrawRect(_sliderBox, _lightColorPaint);
            _sliderBox.Right = circlePosX;
            canvas.DrawRect(_sliderBox, _colorPaint);
            _sliderBox.Right = _width;

            canvas.DrawSurface(_circleSurface, circlePosX - _circleRadius - 10 * _dip, -5 * _dip);
        }
    }
}