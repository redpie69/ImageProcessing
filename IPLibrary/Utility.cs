using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLibrary
{
    public static partial class IP
    {
        private static Color MinColor(Bitmap image)
        {
            byte minRed = 255;
            byte minGreen = 255;
            byte minBlue = 255;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color piksel = image.GetPixel(x, y);
                    if (piksel.R < minRed)
                        minRed = piksel.R;
                    if (piksel.G < minGreen)
                        minGreen = piksel.G;
                    if (piksel.B < minBlue)
                        minBlue = piksel.B;
                }

            }
            return Color.FromArgb(minRed, minGreen, minBlue);
        }

        private static Color MaxColor(Bitmap image)
        {
            byte maxRed = 255;
            byte maxGreen = 255;
            byte maxBlue = 255;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color piksel = image.GetPixel(x, y);
                    if (piksel.R > maxRed)
                        maxRed = piksel.R;
                    if (piksel.G > maxGreen)
                        maxGreen = piksel.G;
                    if (piksel.B > maxBlue)
                        maxBlue = piksel.B;
                }

            }
            return Color.FromArgb(maxRed, maxGreen, maxBlue);
        }

        private static void Complement(Bitmap image)
        {
            Color oldColor;
            Color newColor;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    oldColor = image.GetPixel(x, y);
                    newColor = Color.FromArgb(255 - oldColor.R, 255 - oldColor.G, 255 - oldColor.B);
                    image.SetPixel(x, y, newColor);
                }
            }
        }
    }
}
