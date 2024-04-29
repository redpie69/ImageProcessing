using System;
using System.Drawing;

namespace IPLibrary
{
    public enum ArithmeticOperations
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    public static partial class IP
    {
        private static int[,] TurnItToMatrix(Bitmap image)
        {
            RGB2GrayScale(image);

            int[,] matrix = new int[image.Width, image.Height];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    matrix[x, y] = image.GetPixel(x, y).R;
                }
            }

            return matrix;
        }

        private static int[,] KerneliDondur(int[,] kernel)
        {
            int[,] dondurulmusKernel = new int[kernel.GetLength(0), kernel.GetLength(1)];
            int tersiAlinacakSayi = kernel.GetLength(0);

            for (int i = 0; i < kernel.GetLength(0); i++)
            {
                for (int j = 0; j < kernel.GetLength(1); j++)
                {
                    dondurulmusKernel[tersiAlinacakSayi - i, tersiAlinacakSayi - j] = kernel[i, j];
                }
            }
            return dondurulmusKernel;
        }
        private static double[,] KerneliDondur(double[,] kernel)
        {
            double[,] dondurulmusKernel = new double[kernel.GetLength(0), kernel.GetLength(1)];
            int tersiAlinacakSayi = kernel.GetLength(0) - 1;

            for (int i = 0; i < kernel.GetLength(0); i++)
            {
                for (int j = 0; j < kernel.GetLength(1); j++)
                {
                    dondurulmusKernel[tersiAlinacakSayi - i, tersiAlinacakSayi - j] = kernel[i, j];
                }
            }
            return dondurulmusKernel;
        }
        private static double[,] ScalarMultiplication(double[,] A, double s)
        {
            double[,] result = new double[A.GetLength(0), A.GetLength(1)];

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    result[i, j] = A[i, j] * s;
                }
            }
            return result;
        }

        private static double[,] ScalarMultiplication(int[,] A, double s)
        {
            double[,] result = new double[A.GetLength(0), A.GetLength(1)];

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    result[i, j] = A[i, j] * s;
                }
            }
            return result;
        }
        private static int[,] MatrixMultiplication(int[,] first, int[,] second)
        {
            if (first.GetLength(1) != second.GetLength(0))
                throw new Exception("legal olmayan matris carpimi");

            int[,] outcomeMatrix = new int[first.GetLength(0), second.GetLength(1)];
            int sum = 0;

            for (int i = 0; i < outcomeMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < outcomeMatrix.GetLength(1); j++)
                {
                    sum = 0;
                    for (int k = 0; k < first.GetLength(1); k++)
                    {
                        sum += first[i, k] * second[k, j];
                    }
                    outcomeMatrix[i, j] = sum;
                }
            }
            return outcomeMatrix;
        }

        private static double[,] MatrixMultiplication(double[,] first, double[,] second)
        {
            if (first.GetLength(1) != second.GetLength(0))
                throw new Exception("legal olmayan matris carpimi");

            double[,] outcomeMatrix = new double[first.GetLength(0), second.GetLength(1)];
            double sum = 0;

            for (int i = 0; i < outcomeMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < outcomeMatrix.GetLength(1); j++)
                {
                    sum = 0;
                    for (int k = 0; k < first.GetLength(1); k++)
                    {
                        sum += first[i, k] * second[k, j];
                    }
                    outcomeMatrix[i, j] = sum;
                }
            }
            return outcomeMatrix;
        }

        private static double Determinant(double[,] matrix)
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

        private static double Determinant(int[,] matrix)
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

        private static int[,] MatrixElementwiseOperations(int[,] A, int[,] B, ArithmeticOperations operation)
        {
            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
                throw new ArgumentException("matris boyutlari esit degil");

            int[,] result = new int[A.GetLength(0), A.GetLength(1)];

            switch (operation)
            {
                case ArithmeticOperations.Add:
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            result[i, j] = A[i, j] + B[i, j];
                        }
                    }
                    break;
                case ArithmeticOperations.Subtract:
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            result[i, j] = A[i, j] - B[i, j];
                        }
                    }
                    break;
                case ArithmeticOperations.Multiply:
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            result[i, j] = A[i, j] * B[i, j];
                        }
                    }
                    break;
                case ArithmeticOperations.Divide:
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            result[i, j] = A[i, j] / B[i, j];
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("invalid operation");
            }

            return result;
        }

        private static double[,] MatrixElementwiseOperations(double[,] A, double[,] B, ArithmeticOperations operation)
        {
            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
                throw new ArgumentException("matris boyutlari esit degil");

            double[,] result = new double[A.GetLength(0), A.GetLength(1)];

            switch (operation)
            {
                case ArithmeticOperations.Add:
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            result[i, j] = A[i, j] + B[i, j];
                        }
                    }
                    break;
                case ArithmeticOperations.Subtract:
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            result[i, j] = A[i, j] - B[i, j];
                        }
                    }
                    break;
                case ArithmeticOperations.Multiply:
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            result[i, j] = A[i, j] * B[i, j];
                        }
                    }
                    break;
                case ArithmeticOperations.Divide:
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            result[i, j] = A[i, j] / B[i, j];
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("invalid operation");
            }

            return result;
        }

        private static double InnerProduct(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("vektorler ayni boyutta degil");

            double result = 0;

            for (int i = 0; i < a.Length; i++)
            {
                result += a[i] * b[i];
            }

            return result;
        }

        private static double EuclidianNorm(double[] a)
        {
            return Math.Sqrt(InnerProduct(a, a));
        }

        private static double EuclidianDistance(double[] a, double[] b)
        {
            return EuclidianNorm(VectorDifference(a, b));
        }

        private static double[] VectorSum(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("vektorler ayni boyutta degil");

            double[] result = new double[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] + b[i];
            }

            return result;
        }

        private static double[] VectorDifference(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("vektorler ayni boyutta degil");

            double[] result = new double[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i] - b[i];
            }

            return result;
        }

        private static double[,] MatrixInverse(double[,] A)
        {
            return ScalarMultiplication(AdjointMatrix(A), 1 / Determinant(A));
        }

        private static double[,] MatrixInverse(int[,] A)
        {
            return ScalarMultiplication(AdjointMatrix(A), 1 / Determinant(A));
        }



        private static T[,] Transpoze<T>(T[,] A)
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
        private static double MinorOf(double[,] A, int i, int j)
        {
            return Determinant(IJOut<double>(A, i, j));
        }

        private static double MinorOf(int[,] A, int i, int j)
        {
            return Determinant(IJOut<int>(A, i, j));
        }

        private static T[,] IJOut<T>(T[,] A, int i, int j)
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

        private static double[,] AdjointMatrix(int[,] A)
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

        private static double[,] AdjointMatrix(double[,] A)
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
