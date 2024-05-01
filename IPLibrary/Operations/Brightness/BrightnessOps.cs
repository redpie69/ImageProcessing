using System.Drawing;

namespace IPLibrary.Brightness
{
    public partial class BrightnessOps
    {
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
