using System.Drawing;

namespace IPLibrary
{
    public static partial class IP
    {
        public static int[] HistogramCreator(Bitmap image)
        {
            RGB2GrayScale(image); // ???

            int[] histogram = new int[256];

            for (int i = 0; i < histogram.Length; i++)
            {
                histogram[i] = 0;
            }

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int gray = image.GetPixel(x, y).R;
                    histogram[gray]++;
                }
            }

            return histogram;
        }

        public static int[] CDFunctionCreator(Bitmap image)
        {
            int[] histogram = HistogramCreator(image);
            int[] cDF = new int[256];
            cDF[0] = histogram[0];
            for (int i = 1; i < histogram.Length; i++)
            {
                cDF[i] = cDF[i - 1] + histogram[i];
            }
            return cDF;
        }

        public static void HistogramGerme(Bitmap image, int max, int min)
        {
            RGB2GrayScale(image);

            int imgMin = 255;
            int imgMax = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (image.GetPixel(x, y).R < imgMin)
                        imgMin = image.GetPixel(x, y).R;
                    if (image.GetPixel(x, y).R > imgMax)
                        imgMax = image.GetPixel(x, y).R;
                }
            }

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    double oldPixel = image.GetPixel(x, y).R;
                    double newPixel = (oldPixel - imgMin) * (max - min) / (imgMax - imgMin) + min;
                    Color newColor = Color.FromArgb((int)newPixel, (int)newPixel, (int)newPixel);
                    image.SetPixel(x, y, newColor);
                }
            }
        }

        public static void HistogramGenisletme(Bitmap image)
        {
            RGB2GrayScale(image);

            double imgMin = 255;
            double imgMax = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (image.GetPixel(x, y).R < imgMin)
                        imgMin = image.GetPixel(x, y).R;
                    if (image.GetPixel(x, y).R > imgMax)
                        imgMax = image.GetPixel(x, y).R;
                }
            }

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    double oldPixel = image.GetPixel(x, y).R;
                    double newPixel = (oldPixel - imgMin) / (imgMax - imgMin) * 255;
                    Color newColor = Color.FromArgb((int)newPixel, (int)newPixel, (int)newPixel);
                    image.SetPixel(x, y, newColor);
                }
            }
        }
    }
}
