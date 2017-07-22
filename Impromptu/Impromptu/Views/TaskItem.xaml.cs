using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Impromptu.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaskItem {
        public string Color { get; set; } = "e04242";
        public string LightColor { get; set; } = "ee9696";
        public float PercentComplete { get; set; } = .3f;

        #region paints
        readonly SKPaint _colorPaint;
        readonly SKPaint _colorBlurPaint;
        readonly SKPaint _colorExtraBlurPaint;
        readonly SKPaint _lightColorPaint;
        readonly SKPaint _lightStroke = new SKPaint() {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#50E0E0E0"),
            StrokeWidth = 3,
            IsAntialias = true
        };
        readonly SKPaint _shadowBlurPaint = new SKPaint() {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#60000000"),
            MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 5)
        };
        readonly SKPaint _shadowBlurOuter = new SKPaint() {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#22000000"),
            MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Outer, 10)
        };
        readonly SKPaint _shadowHardPaint = new SKPaint() {
            Style = SKPaintStyle.Fill,
            Color = SKColor.Parse("#30000000"),
            IsAntialias = true
        };
        #endregion

        public TaskItem() {
			InitializeComponent ();
            #region paints
            _colorPaint = new SKPaint() {
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse(Color)
            };
            _colorBlurPaint = new SKPaint() {
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse(Color),
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 3)
            };
            _colorExtraBlurPaint = new SKPaint() {
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse(Color),
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 10)
            };
            _lightColorPaint = new SKPaint() {
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse(LightColor)
            }; 
            #endregion
        }

        private void CanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e) {
	        SKSurface surface = e.Surface;
	        SKCanvas canvas = surface.Canvas;
	        canvas.Clear();
            canvas.Translate(0,12);
	        int width = e.Info.Width;
	        int height = e.Info.Height-24;

	        float circleRadius = height / 2f;
	        float circlePosX = width * PercentComplete + circleRadius;
	        float circlePosY = height / 2f;

            SKRect emptyRect = new SKRect(20, 14, width - 20, height - 14);
	        SKRect filledRect = new SKRect(20, 14, circlePosX, height - 14);

            const int hardShadowXOffset = 5;
	        const int hardShadowYOffset = 7;
	        SKPath path = new SKPath();
            path.AddCircle(circlePosX - 3, circlePosY, circleRadius);
            path.AddRect(emptyRect);
	        path.Offset(new SKPoint(hardShadowXOffset, hardShadowYOffset));
            canvas.DrawPath(path, _shadowHardPaint);

	        canvas.DrawRect(emptyRect, _lightColorPaint);
            canvas.DrawRect(filledRect, _colorPaint);

            //TODO: use paths instead, and move all possible paths outside the method
            canvas.DrawCircle(circlePosX - 8, circlePosY, circleRadius - 8, _shadowBlurPaint);
	        canvas.DrawCircle(circlePosX, circlePosY, circleRadius, _lightColorPaint);
	        canvas.DrawCircle(circlePosX - 8, circlePosY, circleRadius - 8, _colorBlurPaint);
	        canvas.DrawCircle(circlePosX, circlePosY, circleRadius - 16, _colorExtraBlurPaint);
            canvas.DrawOval(circlePosX - 1, circlePosY, circleRadius - 30, circleRadius - 14, _colorExtraBlurPaint);
            canvas.DrawCircle(circlePosX, circlePosY, circleRadius - 1, _lightStroke);
	        canvas.DrawCircle(circlePosX, circlePosY, circleRadius - 13, _shadowBlurOuter);
	        canvas.DrawCircle(circlePosX, circlePosY, circleRadius - 15, _lightStroke);
        }

    }
}