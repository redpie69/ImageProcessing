using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace IPLibrary
{
    public static partial class IP
    {
        public static void FlipHorizontal(Bitmap image)
        {
            Size size = image.Size;

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width / 2; x++)
                {
                    Color temp = image.GetPixel(x, y);
                    image.SetPixel(x, y, image.GetPixel(size.Width - 1 - x, y));
                    image.SetPixel(size.Width - 1 - x, y, temp);
                }
            }
        }

        public static void Constrast(Bitmap image, double a)
        {
            Size size = image.Size;

            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    double red = color.R * a;
                    double green = color.G * a;
                    double blue = color.B * a;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;

                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;

                    image.SetPixel(x, y, Color.FromArgb(color.A, (int)red, (int)green, (int)blue));
                }
            }

        }

        public static void Brightness(Bitmap image, int a)
        {
            Size size = image.Size;

            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    double red = color.R + a;
                    double green = color.G + a;
                    double blue = color.B + a;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;

                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;

                    image.SetPixel(x, y, Color.FromArgb(color.A, (int)red, (int)green, (int)blue));
                }
            }

        }
        

        
    }
}
