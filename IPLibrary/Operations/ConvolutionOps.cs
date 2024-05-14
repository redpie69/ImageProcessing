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

        public static void GaussianBlur(Bitmap image, double sigma)
        {
            int filterSize = (int)(4 * sigma + 0.5) + 1; // Ensure odd filter size
            if (filterSize % 2 == 0)
            {
                filterSize++;
            }

            double[,] gaussianKernel = new double[filterSize, filterSize];
            int center = filterSize / 2;

            for (int y = -center; y <= center; y++)
            {
                for (int x = -center; x <= center; x++)
                {
                    double exponent = -(x * x + y * y) / (2.0 * sigma * sigma);
                    double normal = 1.0 / (2.0 * Math.PI * sigma * sigma);
                    gaussianKernel[y + center, x + center] = normal * Math.Exp(exponent);
                }
            }

            Konvolusyon(image, gaussianKernel);
            return;
        }
        public static void MeanFilter(Bitmap image, int filterSize)
        {
            if (filterSize < 3)
                throw new ArgumentException("filtre boyutu 3 ve daha buyuk olabilir");
            if (filterSize % 2 != 1)
                throw new ArgumentException("filtre boyutu tek sayi olmalidir");

            Color[,] filter = new Color[filterSize, filterSize];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int leftTopX = x - ((filterSize + 1) / 2 - 1);
                    int leftTopY = y - ((filterSize + 1) / 2 - 1);

                    for (int i = 0; i < filterSize; i++)
                    {
                        for (int j = 0; j < filterSize; j++)
                        {
                            if ((leftTopX + i) < 0 || (leftTopX + i) >= image.Width || (leftTopY + j) < 0 || (leftTopY + j) >= image.Height)
                            {
                                filter[i, j] = Color.Black;
                                continue;
                            }
                            filter[i, j] = image.GetPixel(leftTopX + i, leftTopY + j);
                        }
                    }

                    int redSum = 0;
                    int greenSum = 0;
                    int blueSum = 0;

                    for (int i = 0; i < filterSize; i++)
                    {
                        for (int j = 0; j < filterSize; j++)
                        {
                            redSum += filter[i, j].R;
                            greenSum += filter[i, j].G;
                            blueSum += filter[i, j].B;
                        }
                    }

                    int pixelCount = filterSize * filterSize;
                    int redAverage = redSum / pixelCount;
                    int greenAverage = greenSum / pixelCount;
                    int blueAverage = blueSum / pixelCount;

                    Color newColor = Color.FromArgb(redAverage, greenAverage, blueAverage);

                    image.SetPixel(x, y, newColor);
                }
            }
        }

        public static void MedianFilter(Bitmap image, int filterSize) // filtre alaninin bir kenarinin uzunlugu
        {
            if (filterSize < 3)
                throw new ArgumentException("filtre boyutu 3 ve daha buyuk olabilir");
            if (filterSize % 2 != 1)
                throw new ArgumentException("filtre boyutu tek sayi olmalidir");


            Color[,] filter = new Color[filterSize, filterSize];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int leftTopX = x - ((filterSize + 1) / 2 - 1);
                    int leftTopY = y - ((filterSize + 1) / 2 - 1);

                    for (int i = 0; i < filterSize; i++)
                    {
                        for (int j = 0; j < filterSize; j++)
                        {
                            if ((leftTopX + i) < 0 || (leftTopX + i) >= image.Width || (leftTopY + j) < 0 || (leftTopY + j) >= image.Height)
                            {
                                filter[i, j] = Color.Black;
                                continue;
                            }
                            filter[i, j] = image.GetPixel(leftTopX + i, leftTopY + j);
                        }
                    }
                    int arraySize = filterSize * filterSize;
                    int[] red = new int[arraySize];
                    int[] green = new int[arraySize];
                    int[] blue = new int[arraySize];

                    for (int i = 0; i < arraySize; i++)
                    {
                        red[i] = filter[i / filterSize, i % filterSize].R;
                        green[i] = filter[i / filterSize, i % filterSize].G;
                        blue[i] = filter[i / filterSize, i % filterSize].B;
                    }

                    Array.Sort(red);
                    Array.Sort(green);
                    Array.Sort(blue);

                    int mid = (arraySize + 1) / 2;

                    Color newColor = Color.FromArgb(red[mid], green[mid], blue[mid]);

                    image.SetPixel(x, y, newColor);
                }
            }
        }
    }
}
