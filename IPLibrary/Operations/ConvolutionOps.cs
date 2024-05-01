using IPLibrary.Utility;
using System;
using System.Drawing;

namespace IPLibrary
{
    public static partial class IP
    {
        public static void Konvolusyon(Bitmap image, double[,] kernel)
        {
            if (kernel.GetLength(0) != kernel.GetLength(1))
            {
                throw new ArgumentException("kernel kare matris olmali");
            }
            if (kernel.GetLength(0) % 2 != 1)
            {
                throw new ArgumentException("kernelin eni ve boyu tek sayi olmali");
            }

            Bitmap cerceveli = ImageOps.CevresiniDoldurma(image);
            double[,] dondurulmusKernel = MatrixOps.KerneliDondur(kernel);
            int[,] matrisHali = ImageOps.TurnItToMatrix(cerceveli);

            int kernelBoyutu = kernel.GetLength(0);
            int merkezdenUzaklik = kernelBoyutu / 2;

            Bitmap newImage = new Bitmap(image.Width, image.Height);

            for (int y = 1; y < cerceveli.Height - 1; y++)
            {
                for (int x = 1; x < cerceveli.Width - 1; x++)
                {
                    double toplam = 0;

                    for (int i = 0; i < kernel.GetLength(0); i++)
                    {
                        for (int j = 0; j < kernel.GetLength(1); j++)
                        {
                            toplam += matrisHali[x - merkezdenUzaklik + i, y - merkezdenUzaklik + j] * dondurulmusKernel[i, j];
                        }
                    }
                    if (toplam > 255)
                    {
                        toplam = 255;
                    }
                    else if (toplam < 0)
                    {
                        toplam = 0;
                    }

                    newImage.SetPixel(x - 1, y - 1, Color.FromArgb((int)toplam, (int)toplam, (int)toplam));

                }
            }
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    image.SetPixel(x, y, newImage.GetPixel(x, y));
                }
            }
        }
    }
}
