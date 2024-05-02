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

        /// <summary>
        /// Performs scalar multiplication on a matrix.
        /// </summary>
        /// <param name="A">The matrix to be multiplied.</param>
        /// <param name="s">The scalar value.</param>
        /// <returns>The result of the scalar multiplication.</returns>
        public static double[,] ScalarMultiplication(int[,] A, double s)
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

        /// <summary>
        /// Performs matrix multiplication on two matrices.
        /// </summary>
        /// <param name="first">The first matrix.</param>
        /// <param name="second">The second matrix.</param>
        /// <returns>The result of the matrix multiplication.</returns>
        public static int[,] MatrixMultiplication(int[,] first, int[,] second)
        {
            if (first.GetLength(1) != second.GetLength(0))
                throw new Exception("Illegal matrix multiplication");

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

        /// <summary>
        /// Performs matrix multiplication on two matrices.
        /// </summary>
        /// <param name="first">The first matrix.</param>
        /// <param name="second">The second matrix.</param>
        /// <returns>The result of the matrix multiplication.</returns>
        public static double[,] MatrixMultiplication(double[,] first, double[,] second)
        {
            if (first.GetLength(1) != second.GetLength(0))
                throw new Exception("Illegal matrix multiplication");

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

    }
}
