using System;

namespace IPLibrary.Utility
{
    internal partial class MatrixOps
    {
        public static int[,] MatrixElementwiseOperations(int[,] A, int[,] B, ArithmeticOperations operation)
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

        public static double[,] MatrixElementwiseOperations(double[,] A, double[,] B, ArithmeticOperations operation)
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
    }
}
