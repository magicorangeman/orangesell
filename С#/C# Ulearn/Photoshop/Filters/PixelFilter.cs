using System;

namespace MyPhotoshop
{
    public class PixelFilter<TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new()
    {
        readonly Func<Pixel, TParameters, Pixel> processor;

        public PixelFilter (string name, Func<Pixel, TParameters, Pixel> processor)
        {
            this.name = name;
            this.processor = processor;
        }

        public override Photo Process(Photo original, TParameters parameters)
        {
            var result = new Photo(original.Width, original.Height);

            for (int x = 0; x < result.Width; x++)
                for (int y = 0; y < result.Height; y++)
                    result[x, y] = processor(original[x, y], parameters);

            return result;
        }
    }
}
