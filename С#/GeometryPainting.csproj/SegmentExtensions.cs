using GeometryTasks;
using System.Drawing;

namespace GeometryPainting
{ 
    public static class SegmentExtensions
    {
        public static int R;
        public static int G;
        public static System.Drawing.Color Color;
        public static void SetColor(this Segment segment, Color newColor) => segment.Color = newColor;
        public static Color GetColor(this Segment segment) => segment.Color;
    }
}
