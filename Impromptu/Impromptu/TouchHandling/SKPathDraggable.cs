using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Impromptu.TouchHandling {
    class SKPathDraggable : SKPath {
        public bool IsBeingDragged { get; set; }
        public long TouchId { get; private set; }
        public SKPoint OldPosition { get; set; } = new SKPoint(0, 0);
        public bool IsDraggableX { get; set; } = true;
        public bool IsDraggableY { get; set; } = true;

        public void OnTouchEffectAction(SKPoint point, TouchActionEventArgs args) {
            switch(args.Type) {
                case TouchActionType.Pressed:
                    if(Contains(point.X, point.Y) && !IsBeingDragged) {
                        IsBeingDragged = true;
                        TouchId = args.Id;
                        OldPosition = point;
                    }
                    break;

                case TouchActionType.Moved:
                    if(IsBeingDragged && TouchId == args.Id) {
                        Offset(IsDraggableX ? point.X - OldPosition.X : 0, IsDraggableY ? point.Y - OldPosition.Y : 0);
                        OldPosition = point;
                    }
                    break;

                case TouchActionType.Released:
                    if(IsBeingDragged && TouchId == args.Id) {
                        IsBeingDragged = false;
                    }
                    break;
            }
        }
    }
}