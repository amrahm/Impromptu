using System;
using Xamarin.Forms;

namespace Impromptu.Effects.TouchHandling
{
    public class TouchActionEventArgs : EventArgs
    {
        public TouchActionEventArgs(long id, TouchActionType type, Point location, bool isInContact)
        {
            Id = id;
            Type = type;
            Location = location;
        }

        public long Id { get; }

        public TouchActionType Type { get; }

        public Point Location { get; }
    }
}
