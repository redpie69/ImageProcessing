using System;
using System.Drawing.Drawing2D;

namespace IPLibrary.Utility
{
    public partial class MatrixOps
    {
        /// <summary>
        /// Performs interpolation on a set of points to determine the value at a given destination point.
        /// </summary>
        /// <param name="points">The set of points.</param>
        /// <param name="destX">The x-coordinate of the destination point.</param>
        /// <param name="destY">The y-coordinate of the destination point.</param>
        /// <param name="interpolationMode">The interpolation mode.</param>
        /// <returns>The interpolated value.</returns>
        public static double Interpolation(int[,] points, double destX, double destY, InterpolationMode interpolationMode)
        {
            //Points[,3] {x,y,f(x,y)}

            if (points.GetLength(1) != 3)
                throw new ArgumentException("Invalid input array format");
            int loopCount;
            switch (interpolationMode)
            {
                case InterpolationMode.NearestNeighbor:
                    return points[0, 2];
                case InterpolationMode.Bilinear:
                    if (points.GetLength(0) != 4)
                        throw new ArgumentException("Insufficient number of values for interpolation");
                    loopCount = 4;
                    break;
                case InterpolationMode.Bicubic:
                    if (points.GetLength(0) != 16)
                        throw new ArgumentException("Insufficient number of values for interpolation");
                    loopCount = 16;
                    break;
                default:
                    throw new ArgumentException("Invalid interpolation mode");
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

        /// <summary>
        /// Creates a neighborhood matrix for a given point.
        /// </summary>
        /// <param name="F">The original point.</param>
        /// <param name="size">The size of the neighborhood matrix.</param>
        /// <returns>The neighborhood matrix.</returns>
        public static int[,] NeighbourhoodCreator(double[,] F, int size)
        {
            double sizesqr = Math.Sqrt(size);
            if (sizesqr % 1 != 0)
                throw new ArgumentException("Invalid size");
            if (F.GetLength(0) != 3 || F.GetLength(1) != 1)
                throw new ArgumentException("Invalid original point");
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

        public static double Determinant(double[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);

            if (row != column)
                throw new Exception("ancak kare matrisin determinanti alinabilir");
            if (row == 1 && column == 1)
                return matrix[row, column];
            else if (row == 2 && column == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
            else
            {
                double sum = 0; // determinant toplaminda kullanilacak degisken

                for (int x = 0; x < column; x++)  //birinci satirdan determinant al
                {
                    double[,] ijOut = new double[row - 1, column - 1]; //ij cikarilacak matris

                    int k = 0; //cikarilmis matris indeksleri
                    int l = 0; //

                    for (int i = 1; i < row; i++) // i. ve j. sutunu cikart
                    {
                        for (int j = 0; j < column; j++)
                        {
                            if (j == x)
                                continue;
                            ijOut[k, l] = matrix[i, j];
                            l++;
                        }
                        k++;
                        l = 0;
                    }
                    double minor = Determinant(ijOut); //recursive minor bulma;
                    double cofactor = x % 2 == 0 ? minor : -minor; //kofaktor
                    sum += cofactor * matrix[0, x];
                }

                return sum;
            }
        }

        public static double Determinant(int[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);

            if (row != column)
                throw new Exception("ancak kare matrisin determinanti alinabilir");
            if (row == 1 && column == 1)
                return matrix[row, column];
            else if (row == 2 && column == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
            else
            {
                double sum = 0; // determinant toplaminda kullanilacak degisken

                for (int x = 0; x < column; x++)  //birinci satirdan determinant al
                {
                    int[,] ijOut = new int[row - 1, column - 1]; //ij cikarilacak matris

                    int k = 0; //cikarilmis matris indeksleri
                    int l = 0; //

                    for (int i = 1; i < row; i++) // i. ve j. sutunu cikart
                    {
                        for (int j = 0; j < column; j++)
                        {
                            if (j == x)
                                continue;
                            ijOut[k, l] = matrix[i, j];
                            l++;
                        }
                        k++;
                        l = 0;
                    }
                    double minor = Determinant(ijOut); //recursive minor bulma;
                    double cofactor = x % 2 == 0 ? minor : -minor; //kofaktor
                    sum += cofactor * matrix[0, x];
                }

                return sum;


            }
        }
        public static double[] CramerMethod(int[,] A, int[] B)
        {

            if (A.GetLength(0) != A.GetLength(1))
                throw new ArgumentException("ancak kare matrise cramer metodu uygulanabilir");
            if (A.GetLength(0) != B.Length)
                throw new ArgumentException("A matrisindeki satir sayisi B matrisindeki satir sayisina esit degil");

            double determinantA = Determinant(A);

            if (determinantA == 0)
                throw new ArgumentException("A matrisinin tek cozumu yok");

            double[] X = new double[A.GetLength(0)];
            int[,] D = new int[A.GetLength(0), A.GetLength(1)];

            for (int i = 0; i < X.Length; i++) // her X[i] degiskeni icin hesap yap
            {
                for (int j = 0; j < D.GetLength(0); j++) //D_i matrisini o anki i icin doldur
                {
                    for (int k = 0; k < D.GetLength(1); k++)
                    {
                        if (i == k)
                            D[j, k] = B[j];
                        else
                            D[j, k] = A[j, k];
                    }
                }
                X[i] = Determinant(D) / determinantA;
            }

            return X;
        }
        public static double[] CramerMethod(double[,] A, double[] B)
        {

            if (A.GetLength(0) != A.GetLength(1))
                throw new ArgumentException("ancak kare matrise cramer metodu uygulanabilir");
            if (A.GetLength(0) != B.Length)
                throw new ArgumentException("A matrisindeki satir sayisi B matrisindeki satir sayisina esit degil");

            double determinantA = Determinant(A);

            if (determinantA == 0)
                throw new ArgumentException("A matrisinin tek cozumu yok");

            double[] X = new double[A.GetLength(0)];
            double[,] D = new double[A.GetLength(0), A.GetLength(1)];

            for (int i = 0; i < X.Length; i++) // her X[i] degiskeni icin hesap yap
            {
                for (int j = 0; j < D.GetLength(0); j++) //D_i matrisini o anki i icin doldur
                {
                    for (int k = 0; k < D.GetLength(1); k++)
                    {
                        if (i == k)
                            D[j, k] = B[j];
                        else
                            D[j, k] = A[j, k];
                    }
                }
                X[i] = Determinant(D) / determinantA;
            }

            return X;
        }
        public static double[,] MatrixInverse(double[,] A)
        {
            return ScalarMultiplication(AdjointMatrix(A), 1 / Determinant(A));
        }

        public static double[,] MatrixInverse(int[,] A)
        {
            return ScalarMultiplication(AdjointMatrix(A), 1 / Determinant(A));
        }

        public static T[,] Transpoze<T>(T[,] A)
        {
            T[,] result = new T[A.GetLength(1), A.GetLength(0)];

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    result[j, i] = A[i, j];
                }
            }
            return result;
        }

        public static double MinorOf(double[,] A, int i, int j)
        {
            return Determinant(IJOut<double>(A, i, j));
        }

        public static double MinorOf(int[,] A, int i, int j)
        {
            return Determinant(IJOut<int>(A, i, j));
        }

        public static T[,] IJOut<T>(T[,] A, int i, int j)
        {
            T[,] result = new T[A.GetLength(0) - 1, A.GetLength(1) - 1];

            int ri = 0;
            int rj = 0;

            for (int li = 0; li < A.GetLength(0); li++)
            {
                if (li == i)
                    continue;
                for (int lj = 0; lj < A.GetLength(1); lj++)
                {
                    if (lj == j)
                        continue;
                    result[ri, rj] = A[li, lj];
                    rj++;
                }
                ri++;
                rj = 0;
            }

            return result;
        }

        public static double[,] AdjointMatrix(int[,] A)
        {
            double[,] result = new double[A.GetLength(0), A.GetLength(1)];
            double factor = 1;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    factor = (i + j) % 2 == 0 ? 1 : -1;
                    result[i, j] = factor * MinorOf(A, i, j);
                }
            }

            return Transpoze<double>(result);
        }

        public static double[,] AdjointMatrix(double[,] A)
        {
            double[,] result = new double[A.GetLength(0), A.GetLength(1)];
            double factor = 1;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    factor = (i + j) % 2 == 0 ? 1 : -1;
                    result[i, j] = factor * MinorOf(A, i, j);
                }
            }

            return Transpoze<double>(result);
        }
    }
}
