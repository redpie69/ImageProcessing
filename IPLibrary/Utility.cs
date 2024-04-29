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
        private static Bitmap CevresiniDoldurma(Bitmap image)
        {
            Bitmap CerceveliBitmap = new Bitmap(image.Width + 1, image.Height + 1);

            for (int y = 0; y < CerceveliBitmap.Height; y++)
            {
                for (int x = 0; x < CerceveliBitmap.Width; x++)
                {
                    if (x == 0 || y == 0 || y == CerceveliBitmap.Height - 1 || x == CerceveliBitmap.Width - 1)
                        CerceveliBitmap.SetPixel(x, y, Color.Black);
                    else
                        CerceveliBitmap.SetPixel(x, y, image.GetPixel(x - 1, y - 1));
                }
            }

            return CerceveliBitmap;
        }
        private static int[,] NeighbourhoodCreator(double[,] F, int size)
        {
            double sizesqr = Math.Sqrt(size);
            if (sizesqr % 1 != 0)
                throw new ArgumentException("gecersiz buyukluk");
            if (F.GetLength(0) != 3 || F.GetLength(1) != 1)
                throw new ArgumentException("gecersiz orjinal nokta");
            double xCentre = Math.Round(F[0, 0] - 0.5) + 0.5;
            double yCentre = Math.Round(F[1, 0] - 0.5) + 0.5;

            int uLCornerX = (int)(xCentre - (sizesqr / 2 - 0.5));
            int uLCornerY = (int)(yCentre - (sizesqr / 2 - 0.5));

            int[,] points = new int[size, 3];

            for (int i = 0; i < sizesqr; i++)
            {
                for (int j = 0; j < sizesqr; j++)
                {
                    points[i * (int)sizesqr + j, 0] = uLCornerX + j;
                    points[i * (int)sizesqr + j, 1] = uLCornerY + i;
                }

            }

            return points;
        }
        private static double[,] TransformMatrisCreator(Transforms type, params double[] parameters)
        {
            double[,] transformMatrix;
            switch (type)
            {
                case Transforms.Rotate:
                    transformMatrix = new double[3, 3] { { Math.Cos(parameters[0]), -Math.Sin(parameters[0]), 0 }, { Math.Sin(parameters[0]), Math.Cos(parameters[0]), 0 }, { 0, 0, 1 } };
                    break;
                case Transforms.Translation:
                    transformMatrix = new double[3, 3] { { 1, 0, parameters[0] }, { 0, 1, parameters[1] }, { 0, 0, 1 } };
                    break;
                case Transforms.Scaling:
                    transformMatrix = new double[3, 3] { { parameters[0], 0, 0 }, { 0, parameters[1], 0 }, { 0, 0, 1 } };
                    break;
                case Transforms.ShearVertical:
                    transformMatrix = new double[3, 3] { { 1, parameters[0], 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
                    break;
                case Transforms.ShearHorizantal:
                    transformMatrix = new double[3, 3] { { 1, 0, 0 }, { parameters[0], 1, 0 }, { 0, 0, 1 } };
                    break;
                default:
                    throw new ArgumentException("gecersiz arguman");
            }

            return transformMatrix;
        }
        private static double Interpolation(int[,] points, double destX, double destY, InterpolationMode interpolationMode)
        {
            //Points[,3] {x,y,f(x,y)}

            if (points.GetLength(1) != 3)
                throw new ArgumentException("hatali input dizisi formati");
            int loopCount;
            switch (interpolationMode)
            {
                case InterpolationMode.NearestNeighbor:
                    return points[0, 2];
                case InterpolationMode.Bilinear:
                    if (points.GetLength(0) != 4)
                        throw new ArgumentException("enterpolasyon icin yeterli sayida deger yok");
                    loopCount = 4;
                    break;
                case InterpolationMode.Bicubic:
                    if (points.GetLength(0) != 16)
                        throw new ArgumentException("enterpolasyon icin yeterli sayida deger yok");
                    loopCount = 16;
                    break;
                default:
                    throw new ArgumentException("hatali enterpolasyon secimi");
            }

            double[,] A = new double[loopCount, loopCount];
            double[] B = new double[loopCount];
            _ = new double[loopCount];

            for (int b = 0; b < loopCount; b++)
            {
                B[b] = points[b, 2];
                double x = points[b, 0];
                double y = points[b, 1];
                for (int i = 0; i < Math.Sqrt(loopCount); i++)
                {
                    for (int j = 0; j < Math.Sqrt(loopCount); j++)
                    {
                        A[b, (int)(i * Math.Sqrt(loopCount) + j)] = Math.Pow(x, i) * Math.Pow(y, j);
                    }
                }
            }

            double[] X = CramerMethod(A, B);

            double result = 0;
            for (int i = 0; i < Math.Sqrt(loopCount); i++)
            {
                for (int j = 0; j < Math.Sqrt(loopCount); j++)
                {
                    result += Math.Pow(destX, i) * Math.Pow(destY, j) * X[(int)(i * Math.Sqrt(loopCount) + j)];
                }
            }

            return result;
        }

    }
}
