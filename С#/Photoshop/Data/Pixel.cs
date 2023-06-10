using System;
using System.CodeDom;
using System.Drawing;

namespace MyPhotoshop
{
    public struct Pixel
    {
        double r;
        double g;
        double b;

        public double R
        {
            get => r;
            set => r = Check(value);
        }
        public double G
        {
            get => g;
            set => g = Check(value);
        }
        public double B
        {
            get => b;
            set => b = Check(value);
        }

        private double Check(double value)
        {
            if (value < 0 || value > 1) throw new ArgumentException();
            return value;
        }

        static public double Trim(double value)
        {
            if (value < 0) return 0;
            if (value > 1) return 1;
            return value;
        }

        static public Pixel Trim(Pixel pixel) => new Pixel(Trim(pixel.R), Trim(pixel.G), Trim(pixel.B));

        public Pixel (double r, double g, double b)
        {
            this.r = this.g = this.b = 0;
            R = r;
            G = g;
            B = b;
        }

        public Pixel(Color color) : this((double)color.R / 255, (double)color.G / 255, (double)color.B / 255) { }

        static public Pixel operator *(Pixel pixel, double multiplier)
            => new Pixel(Trim(pixel.R * multiplier), Trim(pixel.G * multiplier), Trim(pixel.B * multiplier));
    }
}
