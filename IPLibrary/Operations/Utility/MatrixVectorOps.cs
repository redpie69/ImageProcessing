using System;

namespace IPLibrary.Utility
{
    internal partial class MatrixOps
    {
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
    }
}
