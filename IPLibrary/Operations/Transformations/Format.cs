using System.Drawing;

namespace IPLibrary.Transformations
{
    public partial class FormatTransformation
    {
        public static int[,] TurnItToMatrix(Bitmap image)
        {
            ColorSpaceTransformation.RGB2GrayScale(image);// ???

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
    }
}
