using SkiaSharp;

namespace Impromptu.Effects.TouchHandling {
    class SKPathDraggable : SKPath {
        public bool IsBeingDragged { get; set; }
        public long TouchId { get; private set; }
        private SKPoint OldPosition { get; set; }
        public bool IsDraggableX { get; set; } = true;
        public bool IsDraggableY { get; set; } = true;
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        private float _oldOffsetX;
        private float _oldOffsetY;

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
                        float dx = IsDraggableX ? point.X - OldPosition.X : 0;
                        float dy = IsDraggableY ? point.Y - OldPosition.Y : 0;
                        OffsetX += dx;
                        OffsetY += dy;
                        OldPosition = point;
                        UpdatePosition();
                    }
                    break;

                case TouchActionType.Released:
                    if(IsBeingDragged && TouchId == args.Id) {
                        IsBeingDragged = false;
                    }
                    break;
            }
        }

        public void UpdatePosition() {
            Offset(OffsetX - _oldOffsetX, OffsetY - _oldOffsetY);
            _oldOffsetX = OffsetX;
            _oldOffsetY = OffsetY;
        }
    }
}