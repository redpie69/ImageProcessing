﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace IPLibrary
{
    public static partial class IP
    {
        public static void FlipHorizontal(Bitmap image)
        {
            Size size = image.Size;

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width / 2; x++)
                {
                    Color temp = image.GetPixel(x, y);
                    image.SetPixel(x, y, image.GetPixel(size.Width - 1 - x, y));
                    image.SetPixel(size.Width - 1 - x, y, temp);
                }
            }
        }
        public static void Constrast(Bitmap image, double a)
        {
            Size size = image.Size;

            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    double red = color.R * a;
                    double green = color.G * a;
                    double blue = color.B * a;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;

                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;

                    image.SetPixel(x, y, Color.FromArgb(color.A, (int)red, (int)green, (int)blue));
                }
            }

        }

        public static void Translate(Bitmap image, double x, double y)
        {
            Bitmap oldImage = (Bitmap)image.Clone();
            double[,] TransformMatrix = TransformMatrisCreator(Transforms.translation, x, y);
            double[,] reversed = MatrixInverse(TransformMatrix);
            double[,] V = new double[3, 1];
            V[2, 0] = 1;
            double[,] F;
            for (int lx = 0; lx < image.Width; lx++)
            {
                for (int ly = 0; ly < image.Height; ly++)
                {
                    V[0, 0] = lx;
                    V[1, 0] = ly;
                    F = MatrixMultiplication(reversed, V);
                    if (F[0, 0] < image.Width && F[0, 0] > 0 && F[1, 0] < image.Height && F[1, 0] > 0)
                    {
                        image.SetPixel(lx, ly, oldImage.GetPixel((int)Math.Round(F[0, 0]), (int)Math.Round(F[1, 0])));
                    }
                    else
                    {
                        image.SetPixel(lx, ly, Color.Black);
                    }
                }
            }

            oldImage.Dispose();
        }

        public static void Rotate(Bitmap image, double angle, InterpolationMode mode)
        {
            Bitmap oldImage = (Bitmap)image.Clone();
            int centreX = image.Width / 2;
            int centreY = image.Height / 2;

            double[,] translateToOrigin = TransformMatrisCreator(Transforms.translation, -centreX, -centreY);
            double[,] rotate = TransformMatrisCreator(Transforms.rotate, angle);
            double[,] translateBack = TransformMatrisCreator(Transforms.translation, centreX, centreY);
            double[,] intermediate = MatrixMultiplication(translateBack, rotate);
            double[,] transformMatrix = MatrixMultiplication(intermediate, translateToOrigin);
            double[,] reversed = MatrixInverse(transformMatrix);

            double[,] V = new double[3, 1];
            double[,] F = new double[3, 1];
            switch (mode)
            {
                case InterpolationMode.NearestNeighbor:
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            V[0, 0] = x;
                            V[1, 0] = y;
                            V[2, 0] = 1;
                            F = MatrixMultiplication(reversed, V);
                            F[0, 0] = Math.Round(F[0, 0]);
                            F[1, 0] = Math.Round(F[1, 0]);
                            if (F[0, 0] >= 0 && F[0, 0] < oldImage.Width && F[1, 0] >= 0 && F[1, 0] < oldImage.Height)
                            {
                                Color colorToAssign = oldImage.GetPixel((int)F[0, 0], (int)F[1, 0]);
                                image.SetPixel(x, y, colorToAssign);
                            }
                            else
                                image.SetPixel(x, y, Color.Black);
                        }
                    }

                    break;
                case InterpolationMode.Bilinear:
                    for (int x = 0; x < oldImage.Width; x++)
                    {
                        for (int y = 0; y < oldImage.Height; y++)
                        {
                            V[0, 0] = x;
                            V[1, 0] = y;
                            V[2, 0] = 1;
                            F = MatrixMultiplication(reversed, V);
                            int[,] points = NeighbourhoodCreator(F, 4);
                            int[,] pointsR = (int[,])points.Clone();
                            int[,] pointsG = (int[,])points.Clone();
                            int[,] pointsB = (int[,])points.Clone();

                            for (int i = 0; i < points.GetLength(0); i++)
                            {
                                if (points[i, 0] >= 0 && points[i, 1] >= 0 && points[i, 0] < image.Width && points[i, 1] < image.Height)
                                {
                                    pointsR[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).R;
                                    pointsG[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).G;
                                    pointsB[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).B;
                                }
                                else
                                {
                                    pointsR[i, 2] = 0;
                                    pointsG[i, 2] = 0;
                                    pointsB[i, 2] = 0;
                                }
                            }

                            int red;
                            int green;
                            int blue;

                            red = (int)Interpolation(pointsR, F[0, 0], F[1, 0], InterpolationMode.Bilinear);
                            green = (int)Interpolation(pointsG, F[0, 0], F[1, 0], InterpolationMode.Bilinear);
                            blue = (int)Interpolation(pointsB, F[0, 0], F[1, 0], InterpolationMode.Bilinear);

                            Color colorToAssign = Color.FromArgb(red, green, blue);
                            image.SetPixel(x, y, colorToAssign);

                        }
                    }
                    break;
                case InterpolationMode.Bicubic:
                    for (int x = 0; x < oldImage.Width; x++)
                    {
                        for (int y = 0; y < oldImage.Height; y++)
                        {
                            V[0, 0] = x;
                            V[1, 0] = y;
                            V[2, 0] = 1;
                            F = MatrixMultiplication(reversed, V);
                            int[,] points = NeighbourhoodCreator(F, 16);
                            int[,] pointsR = (int[,])points.Clone();
                            int[,] pointsG = (int[,])points.Clone();
                            int[,] pointsB = (int[,])points.Clone();

                            for (int i = 0; i < points.GetLength(0); i++)
                            {
                                if (points[i, 0] >= 0 && points[i, 1] >= 0 && points[i, 0] < image.Width && points[i, 1] < image.Height)
                                {
                                    pointsR[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).R;
                                    pointsG[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).G;
                                    pointsB[i, 2] = oldImage.GetPixel(points[i, 0], points[i, 1]).B;
                                }
                                else
                                {
                                    pointsR[i, 2] = 0;
                                    pointsG[i, 2] = 0;
                                    pointsB[i, 2] = 0;
                                }
                            }
                            int red = (int)Interpolation(pointsR, F[0, 0], F[1, 0], InterpolationMode.Bicubic);
                            int green = (int)Interpolation(pointsG, F[0, 0], F[1, 0], InterpolationMode.Bicubic);
                            int blue = (int)Interpolation(pointsB, F[0, 0], F[1, 0], InterpolationMode.Bicubic);

                            Color colorToAssign = Color.FromArgb(red, green, blue);
                            image.SetPixel(x, y, colorToAssign);

                        }
                    }
                    break;
                default:
                    throw new ArgumentException("gecersiz enterpolasyon modu");
            }
            oldImage.Dispose();
        }

        public static void Brightness(Bitmap image, int a)
        {
            Size size = image.Size;

            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    double red = color.R + a;
                    double green = color.G + a;
                    double blue = color.B + a;
                    if (red > 255)
                        red = 255;
                    else if (red < 0)
                        red = 0;

                    if (green > 255)
                        green = 255;
                    else if (green < 0)
                        green = 0;

                    if (blue > 255)
                        blue = 255;
                    else if (blue < 0)
                        blue = 0;

                    image.SetPixel(x, y, Color.FromArgb(color.A, (int)red, (int)green, (int)blue));
                }
            }

        }



    }
}
