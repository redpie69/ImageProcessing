using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLibrary
{
    public static partial class IP
    {
        private static Color MinColor(Bitmap image)
        {
            byte minRed = 255;
            byte minGreen = 255;
            byte minBlue = 255;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color piksel = image.GetPixel(x, y);
                    if (piksel.R < minRed)
                        minRed = piksel.R;
                    if (piksel.G < minGreen)
                        minGreen = piksel.G;
                    if (piksel.B < minBlue)
                        minBlue = piksel.B;
                }

            }
            return Color.FromArgb(minRed, minGreen, minBlue);
        }

        private static Color MaxColor(Bitmap image)
        {
            byte maxRed = 255;
            byte maxGreen = 255;
            byte maxBlue = 255;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color piksel = image.GetPixel(x, y);
                    if (piksel.R > maxRed)
                        maxRed = piksel.R;
                    if (piksel.G > maxGreen)
                        maxGreen = piksel.G;
                    if (piksel.B > maxBlue)
                        maxBlue = piksel.B;
                }

            }
            return Color.FromArgb(maxRed, maxGreen, maxBlue);
        }

        private static void Complement(Bitmap image)
        {
            Color oldColor;
            Color newColor;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    oldColor = image.GetPixel(x, y);
                    newColor = Color.FromArgb(255 - oldColor.R, 255 - oldColor.G, 255 - oldColor.B);
                    image.SetPixel(x, y, newColor);
                }
            }
        }

        private static int[,] MatrixMultiplication(int[,] first, int[,] second)
        {
            if (first.GetLength(1) != second.GetLength(0))
                throw new Exception("legal olmayan matris carpimi");

            int[,] outcomeMatrix = new int[first.GetLength(0),second.GetLength(1)];
            int sum = 0;

            for(int i=0; i<outcomeMatrix.GetLength(0);i++)
            {
                for(int j=0; j<outcomeMatrix.GetLength(1);j++)
                {
                    sum = 0;
                    for(int k=0; k<first.GetLength(1);k++)
                    {
                        sum += first[i, k] * second[k, j]; 
                    }
                    outcomeMatrix[i, j] = sum;
                }
            }
            return outcomeMatrix;
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

                for (int x=0; x<column;x++)  //birinci satirdan determinant al
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
                            ijOut[k,l] = matrix[i,j];
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

            for(int i =0; i<X.Length;i++) // her X[i] degiskeni icin hesap yap
            {
                for(int j=0;j<D.GetLength(0); j++) //D_i matrisini o anki X[i] icin doldur
                {
                    for(int k=0;k<D.GetLength(1);k++)
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
    }
}
