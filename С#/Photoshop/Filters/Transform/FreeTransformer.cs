using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MyPhotoshop.Filters
{
    public class FreeTransformer : ITransformer<EmptyParameters>
    {
        Func<Size, Size> sizeTransformer;
        Func<Point, Size, Point> pointTransformer;

        public FreeTransformer(Func<Size, Size> sizeTransformer, Func<Point, Size, Point> pointTransformer)
        {
            this.sizeTransformer = sizeTransformer;
            this.pointTransformer = pointTransformer;
        }

        Size oldSize;
        public Size ResultSize { get; private set; }

        public Point? MapPoint(Point newPoint) => pointTransformer(newPoint, oldSize);

        public void Prepare(Size size, EmptyParameters parameters)
        {
            oldSize = size;
            ResultSize = sizeTransformer(size);
        }
    }
}
