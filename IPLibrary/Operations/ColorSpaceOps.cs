using System.Drawing;
using System.Drawing.Imaging;

namespace IPLibrary
{
    public static partial class IP
    {
        public static void RGB2GrayScale(Bitmap image)
        {
            Color newColor;
            Color oldColor;
            int grayValue;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    oldColor = image.GetPixel(x, y);
                    grayValue = (int)(oldColor.R * 0.2989 + oldColor.G * 0.5870 + oldColor.B * 0.1140);
                    newColor = Color.FromArgb(grayValue, grayValue, grayValue);

                    image.SetPixel(x, y, newColor);
                }
            }
        }

        public static void GrayScale2Binary(Bitmap image)
        {
            RGB2GrayScale(image);
            Color oldColor;
            Color newColor;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    oldColor = image.GetPixel(x, y);
                    if (oldColor.R >= 128)
                        newColor = Color.White;
                    else
                        newColor = Color.Black;

                    image.SetPixel(x, y, newColor);
                }
            }
        }

        public static void CiftEsikleme(Bitmap image,int altEsik,int ustEsik)
        {
            RGB2GrayScale(image);

            for(int y=0; y<image.Height; y++)
            {
                for(int x=0; x<image.Width; x++)
                {
                    int intensity = image.GetPixel(x, y).R;

                    if(intensity>=altEsik && intensity <=ustEsik)
                    {
                        image.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        image.SetPixel(x,y,Color.Black);
                    }
                }
            }
        }

    }
}
