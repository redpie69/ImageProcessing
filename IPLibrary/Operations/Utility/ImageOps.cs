using System.Drawing;

namespace IPLibrary
{
    internal static partial class ImageOps
    {
        /// <summary>
        /// Creates a bitmap with a border around the original image.
        /// </summary>
        /// <param name="image">The original image.</param>
        /// <returns>The bitmap with a border.</returns>
        internal static Bitmap CevresiniDoldurma(Bitmap image,int sidesLineCount,int topBotLineCount)
        {
            Bitmap CerceveliBitmap = new Bitmap(image.Width + sidesLineCount*2, image.Height + topBotLineCount*2);

            for (int y = 0; y < CerceveliBitmap.Height; y++)
            {
                for (int x = 0; x < CerceveliBitmap.Width; x++)
                {
                    if (x<sidesLineCount||y<topBotLineCount||x>=image.Width+sidesLineCount||y>=image.Height+topBotLineCount)
                        CerceveliBitmap.SetPixel(x, y, Color.Black);
                    else
                        CerceveliBitmap.SetPixel(x, y, image.GetPixel(x - sidesLineCount, y - topBotLineCount));
                }
            }
            return CerceveliBitmap;
        }

        /// <summary>
        /// Converts the given image to a matrix representation.
        /// </summary>
        /// <param name="image">The image to convert.</param>
        /// <returns>The matrix representation of the image.</returns>
        public static int[,] TurnItToMatrix(Bitmap image)
        {
            IP.RGB2GrayScale(image);// ???

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
