using System;
using System.Drawing;

namespace MyPhotoshop.Filters
{
    public class TransformFilter<TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new()
    {
        readonly ITransformer<TParameters> transformer;

        public TransformFilter(string name, ITransformer<TParameters> transformer)
        {
            this.name = name;
            this.transformer = transformer;
        }

        public override Photo Process(Photo original, TParameters parameters)
        {
            var oldSize = new Size(original.Width, original.Height);
            transformer.Prepare(oldSize, parameters);
            var result = new Photo(transformer.ResultSize.Width, transformer.ResultSize.Height);
            for (int x = 0; x < result.Width; x++)
                for (int y = 0; y < result.Height; y++)
                {
                    var point = new Point(x, y);
                    var oldPoint = transformer.MapPoint(point);
                    if (oldPoint.HasValue)
                        result[x, y] = original[oldPoint.Value.X, oldPoint.Value.Y];
                }

            return result;
        }
    }
}
