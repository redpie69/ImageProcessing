using IPLibrary.Transformations;
using IPLibrary.Utility;
using System.Drawing;

namespace IPLibrary.Arithmetic
{
    public partial class ArithmeticOps
    {
        public static Bitmap ArithmeticOperation(Bitmap image1, Bitmap image2, ArithmeticOperations operation)
        {
            int width = image1.Width >= image2.Width ? image1.Width : image2.Width;
            int height = image1.Height >= image2.Height ? image1.Height : image2.Height;

            ColorSpaceTransformation.RGB2GrayScale(image1);
            ColorSpaceTransformation.RGB2GrayScale(image2);

            int[,] matrix1 = new int[width, height];
            int[,] matrix2 = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    matrix1[x, y] = 0;
                    matrix2[x, y] = 0;
                }
            }

            for (int y = 0; y < image1.Height; y++)
            {
                for (int x = 0; x < image1.Width; x++)
                {
                    matrix1[x, y] = image1.GetPixel(x, y).R;
                }
            }

            for (int y = 0; y < image2.Height; y++)
            {
                for (int x = 0; x < image2.Width; x++)
                {
                    matrix2[x, y] = image2.GetPixel(x, y).R;
                }
            }

            int[,] returnMatrix = MatrixOps.MatrixElementwiseOperations(matrix1, matrix2, operation);

            Color newColor;
            Bitmap newImage = new Bitmap(returnMatrix.GetLength(0), returnMatrix.GetLength(1));

            for (int y = 0; y < newImage.Height; y++)
            {
                for (int x = 0; x < newImage.Width; x++)
                {
                    returnMatrix[x, y] = returnMatrix[x, y] < 256 ? returnMatrix[x, y] : 255;
                    returnMatrix[x, y] = returnMatrix[x, y] >= 0 ? returnMatrix[x, y] : 0;

                    newColor = Color.FromArgb(returnMatrix[x, y], returnMatrix[x, y], returnMatrix[x, y]);
                    newImage.SetPixel(x, y, newColor);
                }
            }

            return newImage;
        }


    }
}
