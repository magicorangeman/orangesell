namespace MyPhotoshop
{
    public class Photo
    {
        public readonly int Width;
        public readonly int Height;
        private readonly Pixel[,] Data;

        public Photo(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new Pixel[width, height];
        }

        public Pixel this[int x, int y]
        {
            get { return Data[x, y]; }
            set { Data[x, y] = value; }
        }
    }
}

