namespace IPLibrary.Utility
{
    internal partial class MatrixOps
    {
        public static int[,] KerneliDondur(int[,] kernel)
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
        public static double[,] KerneliDondur(double[,] kernel)
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
    }
}
