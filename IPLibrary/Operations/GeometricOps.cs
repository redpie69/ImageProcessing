using IPLibrary.Utility;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IPLibrary
{
    public enum Transforms
    {
        Rotate,
        Translation,
        Scaling,
        ShearVertical,
        ShearHorizantal,
    }
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
        public static Bitmap Crop(Bitmap image, Point topLeft, Point bottomRight)
        {
            int height = topLeft.Y - bottomRight.Y;
            int width = bottomRight.X - topLeft.X;
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
        public static void Translate(Bitmap image, double x, double y)
        {
            Bitmap oldImage = (Bitmap)image.Clone();
            double[,] TransformMatrix = MatrixOps.TransformMatrisCreator(Transforms.Translation, x, y);
            double[,] reversed = MatrixOps.MatrixInverse(TransformMatrix);
            double[,] V = new double[3, 1];
            V[2, 0] = 1;
            double[,] F;
            for (int lx = 0; lx < image.Width; lx++)
            {
                for (int ly = 0; ly < image.Height; ly++)
                {
                    V[0, 0] = lx;
                    V[1, 0] = ly;
                    F = MatrixOps.MatrixMultiplication(reversed, V);
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

            double[,] translateToOrigin = MatrixOps.TransformMatrisCreator(Transforms.Translation, -centreX, -centreY);
            double[,] rotate = MatrixOps.TransformMatrisCreator(Transforms.Rotate, angle);
            double[,] translateBack = MatrixOps.TransformMatrisCreator(Transforms.Translation, centreX, centreY);
            double[,] intermediate = MatrixOps.MatrixMultiplication(translateBack, rotate);
            double[,] transformMatrix = MatrixOps.MatrixMultiplication(intermediate, translateToOrigin);
            double[,] reversed = MatrixOps.MatrixInverse(transformMatrix);

            double[,] v = new double[3, 1];
            double[,] f;
            switch (mode)
            {
                case InterpolationMode.NearestNeighbor:
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            v[0, 0] = x;
                            v[1, 0] = y;
                            v[2, 0] = 1;
                            f = MatrixOps.MatrixMultiplication(reversed, v);
                            f[0, 0] = Math.Round(f[0, 0]);
                            f[1, 0] = Math.Round(f[1, 0]);
                            if (f[0, 0] >= 0 && f[0, 0] < oldImage.Width && f[1, 0] >= 0 && f[1, 0] < oldImage.Height)
                            {
                                Color colorToAssign = oldImage.GetPixel((int)f[0, 0], (int)f[1, 0]);
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
                            v[0, 0] = x;
                            v[1, 0] = y;
                            v[2, 0] = 1;
                            f = MatrixOps.MatrixMultiplication(reversed, v);
                            int[,] points = MatrixOps.NeighbourhoodCreator(f, 4);
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

                            red = (int)MatrixOps.Interpolation(pointsR, f[0, 0], f[1, 0], InterpolationMode.Bilinear);
                            green = (int)MatrixOps.Interpolation(pointsG, f[0, 0], f[1, 0], InterpolationMode.Bilinear);
                            blue = (int)MatrixOps.Interpolation(pointsB, f[0, 0], f[1, 0], InterpolationMode.Bilinear);

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
                            v[0, 0] = x;
                            v[1, 0] = y;
                            v[2, 0] = 1;
                            f = MatrixOps.MatrixMultiplication(reversed, v);
                            int[,] points = MatrixOps.NeighbourhoodCreator(f, 16);
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
                            int red = (int)MatrixOps.Interpolation(pointsR, f[0, 0], f[1, 0], InterpolationMode.Bicubic);
                            int green = (int)MatrixOps.Interpolation(pointsG, f[0, 0], f[1, 0], InterpolationMode.Bicubic);
                            int blue = (int)MatrixOps.Interpolation(pointsB, f[0, 0], f[1, 0], InterpolationMode.Bicubic);

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

        public static Bitmap Scaling(Bitmap image, InterpolationMode mode, int width, int height)
        {
            Bitmap newImage = new Bitmap(width, height);
            double[] matrisCreatorParameters = { (double)width / image.Width, (double)height / image.Height };
            double[,] translationMatrix = MatrixOps.TransformMatrisCreator(Transforms.Scaling, matrisCreatorParameters);
            double[,] reversedTransMatrix = MatrixOps.MatrixInverse(translationMatrix);

            double[,] f;
            double[,] v = new double[3, 1];

            switch (mode)
            {
                case InterpolationMode.Bilinear:
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            v[0, 0] = x;
                            v[1, 0] = y;
                            v[2, 0] = 1;

                            f = MatrixOps.MatrixMultiplication(reversedTransMatrix, v);

                            int[,] pointsR = MatrixOps.NeighbourhoodCreator(f, 4);
                            int[,] pointsG = new int[pointsR.GetLength(0),pointsR.GetLength(1)];
                            int[,] pointsB = new int[pointsR.GetLength(0),pointsR.GetLength(1)];
                            pointsG=(int[,])pointsR.Clone();
                            pointsB=(int[,])pointsR.Clone();

                            for (int i = 0; i < pointsR.GetLength(0); i++)
                            {
                                if (pointsR[i, 0] >= 0 && pointsR[i, 0] < image.Width && pointsR[i, 1] >= 0 && pointsR[i, 1] < image.Height)
                                {
                                    pointsR[i, 2] = image.GetPixel(pointsR[i, 0], pointsR[i, 1]).R;
                                    pointsG[i, 2] = image.GetPixel(pointsG[i, 0], pointsG[i, 1]).G;
                                    pointsB[i, 2] = image.GetPixel(pointsB[i, 0], pointsB[i, 1]).B;
                                }
                                else
                                {
                                    pointsR[i, 2] = 0;
                                    pointsG[i, 2] = 0;
                                    pointsB[i, 2] = 0;
                                }
                            }

                            int red = (int)MatrixOps.Interpolation(pointsR, f[0, 0], f[1, 0], InterpolationMode.Bilinear);
                            int green = (int)MatrixOps.Interpolation(pointsG, f[0, 0], f[1, 0], InterpolationMode.Bilinear);
                            int blue = (int)MatrixOps.Interpolation(pointsB, f[0, 0], f[1, 0], InterpolationMode.Bilinear);

                            newImage.SetPixel(x, y, Color.FromArgb(red, green, blue));
                        }
                    }
                    break;
                case InterpolationMode.Bicubic:
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {

                            v[0, 0] = x;
                            v[1, 0] = y;
                            v[2, 0] = 1;

                            f = MatrixOps.MatrixMultiplication(reversedTransMatrix, v);

                            int[,] pointsR = MatrixOps.NeighbourhoodCreator(f, 16);
                            int[,] pointsG = new int[pointsR.GetLength(0), pointsR.GetLength(1)];
                            int[,] pointsB = new int[pointsR.GetLength(0), pointsR.GetLength(1)];
                            pointsG = (int[,])pointsR.Clone();
                            pointsB = (int[,])pointsR.Clone();

                            for (int i = 0; i < pointsR.GetLength(0); i++)
                            {
                                if (pointsR[i, 0] >= 0 && pointsR[i, 0] < width && pointsR[i, 1] >= 0 && pointsR[i, 1] < height)
                                {
                                    pointsR[i, 2] = image.GetPixel(pointsR[i, 0], pointsR[i, 1]).R;
                                    pointsG[i, 2] = image.GetPixel(pointsG[i, 0], pointsG[i, 1]).G;
                                    pointsB[i, 2] = image.GetPixel(pointsB[i, 0], pointsB[i, 1]).B;
                                }
                                else
                                {
                                    pointsR[i, 2] = 0;
                                    pointsG[i, 2] = 0;
                                    pointsB[i, 2] = 0;
                                }
                            }

                            int red = (int)MatrixOps.Interpolation(pointsR, f[0, 0], f[1, 0], InterpolationMode.Bicubic);
                            int green = (int)MatrixOps.Interpolation(pointsG, f[0, 0], f[1, 0], InterpolationMode.Bicubic);
                            int blue = (int)MatrixOps.Interpolation(pointsB, f[0, 0], f[1, 0], InterpolationMode.Bicubic);

                            newImage.SetPixel(x, y, Color.FromArgb(red, green, blue));
                        }
                    }
                    break;
                case InterpolationMode.NearestNeighbor:
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            v[0, 0] = x;
                            v[1, 0] = y;
                            v[2, 0] = 1;

                            f = MatrixOps.MatrixMultiplication(reversedTransMatrix, v);

                            int fX = (int)f[0, 0];
                            int fY = (int)f[1, 0];

                            newImage.SetPixel(x, y, image.GetPixel(fX, fY));
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("gecerli bir enterpolasyon yontemi secin");
            }

            return newImage;
        }
    }
}
