using System;

namespace IPLibrary.Utility
{
    internal partial class MatrixOps
    {
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
        public static int[,] MatrixMultiplication(int[,] first, int[,] second)
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

        public static double[,] MatrixMultiplication(double[,] first, double[,] second)
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

    }
}
