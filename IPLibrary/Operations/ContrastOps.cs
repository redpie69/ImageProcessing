using IPLibrary.Utility;
using System.Drawing;

namespace IPLibrary
{
    public static partial class IP
    {
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
        public static void KontrastAyari(Bitmap image, double oran)
        {
            RGB2GrayScale(image);
            double[,] imageMatrix = new double[image.Width, image.Height];
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    imageMatrix[x, y] = image.GetPixel(x, y).R;
                }
            }

            double[,] newMatrix = MatrixOps.ScalarMultiplication(imageMatrix, oran);

            for (int y = 0; y < newMatrix.GetLength(1); y++)
            {
                for (int x = 0; x < newMatrix.GetLength(0); x++)
                {
                    if (newMatrix[x, y] > 255)
                        newMatrix[x, y] = 255;
                    if (newMatrix[x, y] < 0)
                        newMatrix[x, y] = 0;
                }
            }

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color newColor = Color.FromArgb((int)newMatrix[x, y], (int)newMatrix[x, y], (int)newMatrix[x, y]);
                    image.SetPixel(x, y, newColor);
                }
            }
        }

    }
}
