using System;

namespace IPLibrary.Utility
{
    internal partial class MatrixOps
    {
        /// <summary>
        /// Performs element-wise operations on two matrices of integers.
        /// </summary>
        /// <param name="A">The first matrix.</param>
        /// <param name="B">The second matrix.</param>
        /// <param name="operation">The arithmetic operation to perform.</param>
        /// <returns>The result matrix.</returns>
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
                            if (B[i, j] == 0) B[i, j] = 1;
                            result[i, j] = A[i, j] / B[i, j];
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("invalid operation");
            }

            return result;
        }

        /// <summary>
        /// Performs element-wise operations on two matrices of doubles.
        /// </summary>
        /// <param name="A">The first matrix.</param>
        /// <param name="B">The second matrix.</param>
        /// <param name="operation">The arithmetic operation to perform.</param>
        /// <returns>The result matrix.</returns>
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
