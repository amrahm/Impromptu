using System;
using Impromptu.Effects.TouchHandling;
using Impromptu.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Impromptu.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskItem {
        private Color _filledColor;
        private Color _emptyColor;
        private float _percentComplete;
        private SKPathDraggable _circlePath;
        private int _height;
        private int _width;
        private float _circleRadius;
        private TaskItemViewModel _vm;

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

        public TaskItem() { InitializeComponent(); }

        protected override void OnBindingContextChanged() {
            base.OnBindingContextChanged();
            _vm = (TaskItemViewModel)BindingContext;
            _percentComplete = _vm.TotalProgress;
            _filledColor = _vm.FilledColor;
            _emptyColor = _vm.EmptyColor;
            _colorPaint.Color = _filledColor.ToSKColor();
            _colorBlurPaint.Color = _filledColor.ToSKColor();
            _colorExtraBlurPaint.Color = _filledColor.ToSKColor();
            _lightColorPaint.Color = _emptyColor.ToSKColor();
            var viewModelPropertyChanged = (INotifyPropertyChanged)BindingContext;
            viewModelPropertyChanged.PropertyChanged += (sender, args) => {
                switch(args.PropertyName) {
                    case nameof(_vm.TotalProgress):
                        _percentComplete = _vm.TotalProgress;
                        Debug.WriteLine("v: " + _percentComplete + "  ::  vm: " + _vm.TotalProgress);
                        CanvasView.InvalidateSurface();
                        break;
                    case nameof(_vm.FilledColor):
                        _filledColor = _vm.FilledColor;
                        _colorPaint.Color = _filledColor.ToSKColor();
                        _colorBlurPaint.Color = _filledColor.ToSKColor();
                        _colorExtraBlurPaint.Color = _filledColor.ToSKColor();
                        CanvasView.InvalidateSurface();
                        break;
                    case nameof(_vm.EmptyColor):
                        _emptyColor = _vm.EmptyColor;
                        _lightColorPaint.Color = _emptyColor.ToSKColor();
                        CanvasView.InvalidateSurface();
                        break;
                }
            };
            CanvasView.InvalidateSurface();
        }


        private void OnTouchEffectAction(object sender, TouchActionEventArgs args) {
            SKPoint point = new SKPoint((float)(CanvasView.CanvasSize.Width * args.Location.X / CanvasView.Width),
                                        (float)(CanvasView.CanvasSize.Height * args.Location.Y / CanvasView.Height));
            _circlePath.OnTouchEffectAction(point, args);
            float vmTotalProgress = (_circlePath.OffsetX - 2) / (_width - 2 * _circleRadius - 2);
            _vm.TotalProgress = Math.Max(0, Math.Min(1, vmTotalProgress));
            CanvasView.InvalidateSurface();
        }

        private void CanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e) {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            canvas.Translate(12, 12);
            _width = e.Info.Width - 24;
            _height = e.Info.Height - 24;

            _circleRadius = _height / 2f;
            float circlePosY = _height / 2f;

            if(_circlePath == null) {
                _circlePath = new SKPathDraggable {IsDraggableY = false};
                _circlePath.AddCircle(_circleRadius, circlePosY, _circleRadius);
            }

            _circlePath.OffsetX = 2 + (_width - 2 * _circleRadius - 2) * _percentComplete;
            _circlePath.UpdatePosition();
            float circlePosX = _circlePath.TightBounds.MidX;

            SKRect emptyRect = new SKRect(0, 14, _width, _height - 14);
            SKRect filledRect = new SKRect(0, 14, circlePosX, _height - 14);

            const int hardShadowXOffset = 5;
            const int hardShadowYOffset = 7;
            SKPath path = new SKPath();
            path.AddCircle(circlePosX - 3, circlePosY, _circleRadius);
            path.AddRect(emptyRect);

            path.Offset(new SKPoint(hardShadowXOffset, hardShadowYOffset));
            canvas.DrawPath(path, _shadowHardPaint);

            canvas.DrawRect(emptyRect, _lightColorPaint);
            canvas.DrawRect(filledRect, _colorPaint);

            canvas.DrawCircle(circlePosX - _circleRadius * 0.16f, circlePosY, _circleRadius - _circleRadius * 0.16f, _shadowBlurPaint);
            canvas.DrawPath(_circlePath, _lightColorPaint);
            canvas.DrawCircle(circlePosX - _circleRadius * 0.16f, circlePosY, _circleRadius - _circleRadius * 0.16f, _colorBlurPaint);
            canvas.DrawCircle(circlePosX, circlePosY, _circleRadius * 0.7f, _colorExtraBlurPaint);
            canvas.DrawOval(circlePosX - 1, circlePosY, _circleRadius * 0.50f, _circleRadius * 0.75f, _colorExtraBlurPaint);
            canvas.DrawCircle(circlePosX, circlePosY, _circleRadius + 1, _lightStroke);
            canvas.DrawCircle(circlePosX, circlePosY, _circleRadius * 0.75f - 1, _shadowBlurOuter);
            canvas.DrawCircle(circlePosX, circlePosY, _circleRadius * 0.75f, _lightStroke);
        }
    }
}