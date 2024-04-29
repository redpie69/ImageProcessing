using System;
using System.Drawing;
using System.Drawing.Drawing2D;

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

        public static void Translate(Bitmap image, double x, double y)
        {
            Bitmap oldImage = (Bitmap)image.Clone();
            double[,] TransformMatrix = TransformMatrisCreator(Transforms.Translation, x, y);
            double[,] reversed = MatrixInverse(TransformMatrix);
            double[,] V = new double[3, 1];
            V[2, 0] = 1;
            double[,] F;
            for (int lx = 0; lx < image.Width; lx++)
            {
                for (int ly = 0; ly < image.Height; ly++)
                {
                    V[0, 0] = lx;
                    V[1, 0] = ly;
                    F = MatrixMultiplication(reversed, V);
                    if (F[0, 0] < image.Width && F[0, 0] > 0 && F[1, 0] < image.Height && F[1, 0] > 0)
                    {
                        image.SetPixel(lx, ly, oldImage.GetPixel((int)Math.Round(F[0, 0]), (int)Math.Round(F[1, 0])));
                    }
                    else
                    {
                        image.SetPixel(lx, ly, Color.Black);
                    }
                }
            }

            oldImage.Dispose();
        }

        public static void Rotate(Bitmap image, double angle, InterpolationMode mode)
        {
            Bitmap oldImage = (Bitmap)image.Clone();
            int centreX = image.Width / 2;
            int centreY = image.Height / 2;

            double[,] translateToOrigin = TransformMatrisCreator(Transforms.Translation, -centreX, -centreY);
            double[,] rotate = TransformMatrisCreator(Transforms.Rotate, angle);
            double[,] translateBack = TransformMatrisCreator(Transforms.Translation, centreX, centreY);
            double[,] intermediate = MatrixMultiplication(translateBack, rotate);
            double[,] transformMatrix = MatrixMultiplication(intermediate, translateToOrigin);
            double[,] reversed = MatrixInverse(transformMatrix);

            double[,] V = new double[3, 1];
            double[,] F = new double[3, 1];
            switch (mode)
            {
                case InterpolationMode.NearestNeighbor:
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            V[0, 0] = x;
                            V[1, 0] = y;
                            V[2, 0] = 1;
                            F = MatrixMultiplication(reversed, V);
                            F[0, 0] = Math.Round(F[0, 0]);
                            F[1, 0] = Math.Round(F[1, 0]);
                            if (F[0, 0] >= 0 && F[0, 0] < oldImage.Width && F[1, 0] >= 0 && F[1, 0] < oldImage.Height)
                            {
                                Color colorToAssign = oldImage.GetPixel((int)F[0, 0], (int)F[1, 0]);
                                image.SetPixel(x, y, colorToAssign);
                            }
                            else
                                image.SetPixel(x, y, Color.Black);
                        }
                    }

                    break;
                case InterpolationMode.Bilinear:
                    for (int x = 0; x < oldImage.Width; x++)
                    {
                        for (int y = 0; y < oldImage.Height; y++)
                        {
                            V[0, 0] = x;
                            V[1, 0] = y;
                            V[2, 0] = 1;
                            F = MatrixMultiplication(reversed, V);
                            int[,] points = NeighbourhoodCreator(F, 4);
                            int[,] pointsR = (int[,])points.Clone();
                            int[,] pointsG = (int[,])points.Clone();
                            int[,] pointsB = (int[,])points.Clone();

                            for (int i = 0; i < points.GetLength(0); i++)
                            {
                                if (points[i, 0] >= 0 && points[i, 1] >= 0 && points[i, 0] < image.Width && points[i, 1] < image.Height)
                                {
                                    pointsR[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).R;
                                    pointsG[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).G;
                                    pointsB[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).B;
                                }
                                else
                                {
                                    pointsR[i, 2] = 0;
                                    pointsG[i, 2] = 0;
                                    pointsB[i, 2] = 0;
                                }
                            }

                            int red;
                            int green;
                            int blue;

                            red = (int)Interpolation(pointsR, F[0, 0], F[1, 0], InterpolationMode.Bilinear);
                            green = (int)Interpolation(pointsG, F[0, 0], F[1, 0], InterpolationMode.Bilinear);
                            blue = (int)Interpolation(pointsB, F[0, 0], F[1, 0], InterpolationMode.Bilinear);

                            Color colorToAssign = Color.FromArgb(red, green, blue);
                            image.SetPixel(x, y, colorToAssign);

                        }
                    }
                    break;
                case InterpolationMode.Bicubic:
                    for (int x = 0; x < oldImage.Width; x++)
                    {
                        for (int y = 0; y < oldImage.Height; y++)
                        {
                            V[0, 0] = x;
                            V[1, 0] = y;
                            V[2, 0] = 1;
                            F = MatrixMultiplication(reversed, V);
                            int[,] points = NeighbourhoodCreator(F, 16);
                            int[,] pointsR = (int[,])points.Clone();
                            int[,] pointsG = (int[,])points.Clone();
                            int[,] pointsB = (int[,])points.Clone();

                            for (int i = 0; i < points.GetLength(0); i++)
                            {
                                if (points[i, 0] >= 0 && points[i, 1] >= 0 && points[i, 0] < image.Width && points[i, 1] < image.Height)
                                {
                                    pointsR[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).R;
                                    pointsG[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).G;
                                    pointsB[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).B;
                                }
                                else
                                {
                                    pointsR[i, 2] = 0;
                                    pointsG[i, 2] = 0;
                                    pointsB[i, 2] = 0;
                                }
                            }
                            int red = (int)Interpolation(pointsR, F[0, 0], F[1, 0], InterpolationMode.Bicubic);
                            int green = (int)Interpolation(pointsG, F[0, 0], F[1, 0], InterpolationMode.Bicubic);
                            int blue = (int)Interpolation(pointsB, F[0, 0], F[1, 0], InterpolationMode.Bicubic);

                            Color colorToAssign = Color.FromArgb(red, green, blue);
                            image.SetPixel(x, y, colorToAssign);

                        }
                    }
                    break;
                default:
                    throw new ArgumentException("gecersiz enterpolasyon modu");
            }
            oldImage.Dispose();
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

        public static Bitmap Crop(Bitmap image, Point topLeft, Point bottomRight)
        {
            int height = topLeft.Y - bottomRight.Y;
            int width = topLeft.X - bottomRight.X;
            int mappedX;
            int mappedY;

            Bitmap croppedImage = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    mappedX = topLeft.X + x;
                    mappedY = topLeft.Y + y;
                    croppedImage.SetPixel(x, y, image.GetPixel(mappedX, mappedY));
                }
            }

            return croppedImage;
        }

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

        public static Bitmap ArithmeticOperation(Bitmap image1, Bitmap image2, ArithmeticOperations operation)
        {
            int width = image1.Width >= image2.Width ? image1.Width : image2.Width;
            int height = image1.Height >= image2.Height ? image1.Height : image2.Height;

            RGB2GrayScale(image1);
            RGB2GrayScale(image2);

            int[,] matrix1 = new int[width, height];
            int[,] matrix2 = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    matrix1[x, y] = 0;
                    matrix2[x, y] = 0;
                }
            }

            for (int y = 0; y < image1.Height; y++)
            {
                for (int x = 0; x < image1.Width; x++)
                {
                    matrix1[x, y] = image1.GetPixel(x, y).R;
                }
            }

            for (int y = 0; y < image2.Height; y++)
            {
                for (int x = 0; x < image2.Width; x++)
                {
                    matrix2[x, y] = image2.GetPixel(x, y).R;
                }
            }

            int[,] returnMatrix = MatrixElementwiseOperations(matrix1, matrix2, operation);

            Color newColor;
            Bitmap newImage = new Bitmap(returnMatrix.GetLength(0), returnMatrix.GetLength(1));

            for (int y = 0; y < newImage.Height; y++)
            {
                for (int x = 0; x < newImage.Width; x++)
                {
                    returnMatrix[x, y] = returnMatrix[x, y] < 256 ? returnMatrix[x, y] : 255;
                    returnMatrix[x, y] = returnMatrix[x, y] >= 0 ? returnMatrix[x, y] : 0;

                    newColor = Color.FromArgb(returnMatrix[x, y], returnMatrix[x, y], returnMatrix[x, y]);
                    newImage.SetPixel(x, y, newColor);
                }
            }

            return newImage;
        }

        public static int[] HistogramCreator(Bitmap image)
        {
            RGB2GrayScale(image);

            int[] histogram = new int[256];

            for (int i = 0; i < histogram.Length; i++)
            {
                histogram[i] = 0;
            }

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int gray = image.GetPixel(x, y).R;
                    histogram[gray]++;
                }
            }

            return histogram;
        }

        public static int[] CDFunctionCreator(Bitmap image)
        {
            int[] histogram = HistogramCreator(image);
            int[] cDF = new int[256];
            cDF[0] = histogram[0];
            for (int i = 1; i < histogram.Length; i++)
            {
                cDF[i] = cDF[i - 1] + histogram[i];
            }

            return cDF;

        }

        public static void HistogramGerme(Bitmap image, int max, int min)
        {
            RGB2GrayScale(image);

            int imgMin = 255;
            int imgMax = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (image.GetPixel(x, y).R < imgMin)
                        imgMin = image.GetPixel(x, y).R;
                    if (image.GetPixel(x, y).R > imgMax)
                        imgMax = image.GetPixel(x, y).R;
                }
            }

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    double oldPixel = image.GetPixel(x, y).R;
                    double newPixel = (oldPixel - imgMin) * (max - min) / (imgMax - imgMin) + min;
                    Color newColor = Color.FromArgb((int)newPixel, (int)newPixel, (int)newPixel);
                    image.SetPixel(x, y, newColor);
                }
            }
        }



        public static void HistogramGenisletme(Bitmap image)
        {
            RGB2GrayScale(image);

            double imgMin = 255;
            double imgMax = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (image.GetPixel(x, y).R < imgMin)
                        imgMin = image.GetPixel(x, y).R;
                    if (image.GetPixel(x, y).R > imgMax)
                        imgMax = image.GetPixel(x, y).R;
                }
            }

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    double oldPixel = image.GetPixel(x, y).R;
                    double newPixel = (oldPixel - imgMin) / (imgMax - imgMin) * 255;
                    Color newColor = Color.FromArgb((int)newPixel, (int)newPixel, (int)newPixel);
                    image.SetPixel(x, y, newColor);
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

            double[,] newMatrix = ScalarMultiplication(imageMatrix, oran);

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

            Bitmap cerceveli = CevresiniDoldurma(image);
            double[,] dondurulmusKernel = KerneliDondur(kernel);
            int[,] matrisHali = TurnItToMatrix(cerceveli);

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
