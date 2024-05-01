using System.Drawing;

namespace IPLibrary.Border
{
    public partial class BorderOps
    {
        /// <summary>
        /// Creates a bitmap with a border around the original image.
        /// </summary>
        /// <param name="image">The original image.</param>
        /// <returns>The bitmap with a border.</returns>
        public static Bitmap CevresiniDoldurma(Bitmap image)
        {
            Bitmap CerceveliBitmap = new Bitmap(image.Width + 1, image.Height + 1);

            for (int y = 0; y < CerceveliBitmap.Height; y++)
            {
                for (int x = 0; x < CerceveliBitmap.Width; x++)
                {
                    if (x == 0 || y == 0 || y == CerceveliBitmap.Height - 1 || x == CerceveliBitmap.Width - 1)
                        CerceveliBitmap.SetPixel(x, y, Color.Black);
                    else
                        CerceveliBitmap.SetPixel(x, y, image.GetPixel(x - 1, y - 1));
                }
            }

            return CerceveliBitmap;
        }

    }
}
