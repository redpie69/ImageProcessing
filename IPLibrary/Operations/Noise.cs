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
        public static void AddSaltAndPepper(Bitmap image,double saltChance,double pepperChance)
        {
            if (saltChance < 0 || saltChance > 1)
                throw new ArgumentOutOfRangeException(nameof(saltChance), "chances can be between 0 and 1");
            if(pepperChance < 0 || pepperChance > 1)
                throw new ArgumentOutOfRangeException(nameof(pepperChance), "chances can be between 0 and 1");

            Random random = new Random();
            
            for(int y=0; y < image.Height; y++)
            {
                for(int x=0; x < image.Width; x++)
                {
                    if(random.NextDouble() <= saltChance)
                    {
                        image.SetPixel(x, y, Color.White);
                    }
                    if(random.NextDouble() <=pepperChance)
                    {
                        image.SetPixel(x, y, Color.Black);
                    }

                }
            }
        }
    }
}
