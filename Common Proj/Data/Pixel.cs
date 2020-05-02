using System;

namespace MyPhotoshop.Data
{
    public struct Pixel
    {
        public Pixel(double r,double g, double b)
        {
            _r = _b = _g = 0;
            R = r;
            G = g;
            B = b;
        }
        private double _r;
        private double _g;
        private double _b;
        public double R {
            get => _r;
            set { if (CheckValue(value)) _r = value; }
        }
        public double G
        {
            get => _g;
            set { if (CheckValue(value)) _g = value; }
        }
        public double B
        {
            get => _b;
            set { if (CheckValue(value)) _b = value; }
        }
        public static double Trim(double value)
            => value < 0 ? 0 : value > 1 ? 1 : value;

        public static Pixel operator *(Pixel pix, double num)
            => new Pixel(
                Trim(pix.R * num),
                Trim(pix.G * num),
                Trim(pix.B * num));
        public static Pixel operator *(double num, Pixel pix) => pix * num;
        private bool CheckValue(double value)
            => value >= 0 && value <= 1 ? true : throw new ArgumentException();
    }
}
