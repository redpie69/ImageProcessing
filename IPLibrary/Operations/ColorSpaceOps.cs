using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace IPLibrary
{
    public static partial class IP
    {
        public static void RGB2GrayScale(Bitmap image)
        {
            Color newColor;
            Color oldColor;
            int grayValue;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    oldColor = image.GetPixel(x, y);
                    grayValue = (int)(oldColor.R * 0.2989 + oldColor.G * 0.5870 + oldColor.B * 0.1140);
                    newColor = Color.FromArgb(grayValue, grayValue, grayValue);

                    image.SetPixel(x, y, newColor);
                }
            }
        }

        public static void GrayScale2Binary(Bitmap image)
        {
            RGB2GrayScale(image);
            Color oldColor;
            Color newColor;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    oldColor = image.GetPixel(x, y);
                    if (oldColor.R >= 128)
                        newColor = Color.White;
                    else
                        newColor = Color.Black;

                    image.SetPixel(x, y, newColor);
                }
            }
        }

        public static void Color2HSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color HSV2Color (double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static void CiftEsikleme(Bitmap image,int altEsik,int ustEsik)
        {
            RGB2GrayScale(image);

            for(int y=0; y<image.Height; y++)
            {
                for(int x=0; x<image.Width; x++)
                {
                    int intensity = image.GetPixel(x, y).R;

                    if(intensity>=altEsik && intensity <=ustEsik)
                    {
                        image.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        image.SetPixel(x,y,Color.Black);
                    }
                }
            }
        }

    }
}
