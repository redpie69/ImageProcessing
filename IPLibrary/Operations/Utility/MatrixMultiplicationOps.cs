using System;

namespace IPLibrary.Utility
{
    internal partial class MatrixOps
    {
        /// <summary>
        /// Performs scalar multiplication on a matrix.
        /// </summary>
        /// <param name="A">The matrix to be multiplied.</param>
        /// <param name="s">The scalar value.</param>
        /// <returns>The result of the scalar multiplication.</returns>
        public static double[,] ScalarMultiplication(double[,] A, double s)
        {
            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            double[,] result = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = A[i, j] * s;
                }
            }
            return result;
        }

        /// <summary>
        /// Performs scalar multiplication on a matrix.
        /// </summary>
        /// <param name="A">The matrix to be multiplied.</param>
        /// <param name="s">The scalar value.</param>
        /// <returns>The result of the scalar multiplication.</returns>
        public static double[,] ScalarMultiplication(int[,] A, double s)
        {
            int rows = A.GetLength(0);
            int columns = A.GetLength(1);
            double[,] result = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = A[i, j] * s;
                }
            }
            return result;
        }

        /// <summary>
        /// Performs matrix multiplication on two matrices.
        /// </summary>
        /// <param name="first">The first matrix.</param>
        /// <param name="second">The second matrix.</param>
        /// <returns>The result of the matrix multiplication.</returns>
        public static int[,] MatrixMultiplication(int[,] first, int[,] second)
        {
            int rowsA = first.GetLength(0);
            int colsA = first.GetLength(1);
            int colsB = second.GetLength(1);

            if (colsA != second.GetLength(0))
                throw new Exception("Illegal matrix multiplication");

            int[,] result = new int[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += first[i, k] * second[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        /// <summary>
        /// Performs matrix multiplication on two matrices.
        /// </summary>
        /// <param name="first">The first matrix.</param>
        /// <param name="second">The second matrix.</param>
        /// <returns>The result of the matrix multiplication.</returns>
        public static double[,] MatrixMultiplication(double[,] first, double[,] second)
        {
            int rowsA = first.GetLength(0);
            int colsA = first.GetLength(1);
            int colsB = second.GetLength(1);

            if (colsA != second.GetLength(0))
                throw new Exception("Illegal matrix multiplication");

            double[,] result = new double[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += first[i, k] * second[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }
    }
}
