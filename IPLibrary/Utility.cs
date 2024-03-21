using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLibrary
{
    public static partial class IP
    {
        private static double Interpolation(double[,] points,int destX,int destY,InterpolationMode interpolationMode)
        {
            //Points[,3] {x,y,f(x,y)}
            
            if (points.GetLength(1) != 3)
                throw new ArgumentException("hatali input dizisi formati");
            int loopCount = 0;
            switch (interpolationMode)
            {
                case InterpolationMode.NearestNeighbor:
                    return points[0, 2];
                case InterpolationMode.Bilinear:
                    if (points.GetLength(0) != 4)
                        throw new ArgumentException("enterpolasyon icin yeterli sayida deger yok");
                    loopCount = 4;
                    break;
                case InterpolationMode.Bicubic:
                    if (points.GetLength(0) != 16)
                        throw new ArgumentException("enterpolasyon icin yeterli sayida deger yok");
                    loopCount = 16;
                    break;
                default:
                    throw new ArgumentException("hatali enterpolasyon secimi");
            }

            double[,] A = new double[loopCount, loopCount];
            double[] B = new double[loopCount];
            double[] X = new double[loopCount];

            for(int b=0;b < loopCount;b++)
            {
                B[b]= points[b, 2];

                for (int i=0;i<Math.Sqrt(loopCount);i++)
                {
                    for(int j=0;j<Math.Sqrt(loopCount);j++)
                    {
                        double x = points[i, 0];
                        double y = points[i, 1];


                        A[b, i*j+j] = Math.Pow(x, i) * Math.Pow(y, j);
                    }
                }
            }

            X = CramerMethod(A, B);

            double result = 0;
            for(int i =0;i<Math.Sqrt(loopCount);i++)
            {
                for(int j=0;j<Math.Sqrt(loopCount);j++)
                {
                    result += Math.Pow(destX, i) * Math.Pow(destY, j)*X[i * j + j];
                }
            }
            return result;
        }
        
    }
}
