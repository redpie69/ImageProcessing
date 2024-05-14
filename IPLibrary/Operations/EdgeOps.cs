using System;
using System.Collections.Generic;
using System.Drawing;

namespace IPLibrary
{
    public static partial class IP
    {
        public static Bitmap CannyEdgeDetection(Bitmap image, double sigma, double lowThreshold = 0.05, double highThreshold = 0.09)
        {
            // Implement Sobel edge detection (calculate gradient magnitude and direction)
            (Bitmap gradientMagnitude, Bitmap gradientDirection) = SobelEdgeDetection(image, sigma);

            // Apply non-maximum suppression
            Bitmap suppressedImage = NonMaximumSuppression(gradientMagnitude, gradientDirection);

            // Apply hysteresis thresholding
            Bitmap edgeImage = HysteresisThresholding(suppressedImage, lowThreshold, highThreshold);

            return edgeImage;
        }

        public static (Bitmap gradientMagnitude, Bitmap gradientDirection) SobelEdgeDetection(Bitmap image, double sigma)
        {
            RGB2GrayScale(image); // Assuming a conversion function exists

            GaussianBlur(image, sigma);
            int[,] blurredMatrix = ImageOps.TurnItToMatrix(image);

            // Sobel kernels
            double[,] sobelX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            double[,] sobelY = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int kernelSize = 3;
            int center = kernelSize / 2;

            Bitmap gradientX = new Bitmap(image.Width, image.Height);
            Bitmap gradientY = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    double sumX = 0;
                    double sumY = 0;

                    for (int i = 0; i < kernelSize; i++)
                    {
                        for (int j = 0; j < kernelSize; j++)
                        {
                            int clampedX = Clamp(x + i - center, 0, image.Width - 1);
                            int clampedY = Clamp(y + j - center, 0, image.Height - 1);
                            sumX += blurredMatrix[clampedY, clampedX] * sobelX[i, j];
                            sumY += blurredMatrix[clampedY, clampedX] * sobelY[i, j];
                        }
                    }

                    // Apply clamping using a ternary operator
                    int gradientXValue = sumX < 0 ? 0 : (sumX > 255 ? 255 : (int)sumX);
                    int gradientYValue = sumY < 0 ? 0 : (sumY > 255 ? 255 : (int)sumY);

                    gradientX.SetPixel(x, y, Color.FromArgb(gradientXValue, gradientXValue, gradientXValue));
                    gradientY.SetPixel(x, y, Color.FromArgb(gradientYValue, gradientYValue, gradientYValue));
                }
            }

            // Calculate gradient magnitude and direction (optional)
            Bitmap gradientMagnitude = new Bitmap(image.Width, image.Height);
            Bitmap gradientDirection = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int magnitudeX = gradientX.GetPixel(x, y).R;
                    int magnitudeY = gradientY.GetPixel(x, y).R;

                    double magnitude = Math.Sqrt(magnitudeX * magnitudeX + magnitudeY * magnitudeY);
                    double direction = Math.Atan2(magnitudeY, magnitudeX) * 180 / Math.PI; // Convert radians to degrees

                    // Apply clamping using Math.Min and Math.Max (alternative)
                    int magnitudeValue = Clamp(magnitude, 0, 255);
                    int directionValue = Clamp(direction, 0, 255); // May need adjustments for specific use case

                    gradientMagnitude.SetPixel(x, y, Color.FromArgb(magnitudeValue, magnitudeValue, magnitudeValue));
                    gradientDirection.SetPixel(x, y, Color.FromArgb(directionValue, directionValue, directionValue));
                }
            }

            return (gradientMagnitude, gradientDirection);
        }
        public static Bitmap NonMaximumSuppression(Bitmap gradientMagnitude, Bitmap gradientDirection)
        {
            int height = gradientMagnitude.Height;
            int width = gradientMagnitude.Width;
            Bitmap suppressedImage = new Bitmap(width, height);

            // Convert gradient direction to degrees (assuming it's in radians)
            int[,] directionMatrix = ImageOps.TurnItToMatrix(gradientDirection);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double angle = directionMatrix[y, x] * 180 / Math.PI;
                    angle = (angle + 360) % 180; // Ensure angle is within 0-180 degrees
                    directionMatrix[y, x] = (int)angle;
                }
            }

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int pixelValue = gradientMagnitude.GetPixel(x, y).R;
                    double angle = directionMatrix[y, x];

                    int q = 0;
                    int r = 0;

                    // Determine neighbors based on angle
                    if ((0 <= angle && angle < 22.5) || (157.5 <= angle && angle <= 180))
                    {
                        r = gradientMagnitude.GetPixel(x - 1, y).R;
                        q = gradientMagnitude.GetPixel(x + 1, y).R;
                    }
                    else if (22.5 <= angle && angle < 67.5)
                    {
                        r = gradientMagnitude.GetPixel(x - 1, y + 1).R;
                        q = gradientMagnitude.GetPixel(x + 1, y - 1).R;
                    }
                    else if (67.5 <= angle && angle < 112.5)
                    {
                        r = gradientMagnitude.GetPixel(x - 1, y).R;
                        q = gradientMagnitude.GetPixel(x + 1, y).R;
                    }
                    else if (112.5 <= angle && angle <= 157.5)
                    {
                        r = gradientMagnitude.GetPixel(x + 1, y + 1).R;
                        q = gradientMagnitude.GetPixel(x - 1, y - 1).R;
                    }

                    if (pixelValue >= q && pixelValue >= r)
                    {
                        suppressedImage.SetPixel(x, y, Color.FromArgb(pixelValue, pixelValue, pixelValue));
                    }
                    else
                    {
                        suppressedImage.SetPixel(x, y, Color.FromArgb(0, 0, 0)); // Set to black for suppression
                    }
                }
            }

            return suppressedImage;
        }
        public static Bitmap HysteresisThresholding(Bitmap image, double lowThresholdRatio = 0.05, double highThresholdRatio = 0.09, byte weakPixelValue = 25)
        {
            // Calculate thresholds
            int highThreshold = (int)(image.GetPixel(0, 0).R * highThresholdRatio); // Assuming all pixel values are within 0-255 range
            int lowThreshold = (int)(highThreshold * lowThresholdRatio);

            int height = image.Height;
            int width = image.Width;
            Bitmap resultImage = new Bitmap(width, height);

            const byte strongPixelValue = 255;

            // Find strong and weak edges using thresholds
            List<Point> strongEdges = new List<Point>();
            List<Point> weakEdges = new List<Point>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pixelValue = image.GetPixel(x, y).R;
                    if (pixelValue >= highThreshold)
                    {
                        strongEdges.Add(new Point(x, y));
                    }
                    else if (pixelValue >= lowThreshold)
                    {
                        weakEdges.Add(new Point(x, y));
                    }
                }
            }

            // Apply hysteresis
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (image.GetPixel(x, y).R == weakPixelValue)
                    {
                        bool isStrong = false;
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            for (int dx = -1; dx <= 1; dx++)
                            {
                                int neighborX = x + dx;
                                int neighborY = y + dy;
                                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                                {
                                    if (image.GetPixel(neighborX, neighborY).R == strongPixelValue)
                                    {
                                        isStrong = true;
                                        break; // Early exit if a strong neighbor is found
                                    }
                                }
                            }
                        }

                        resultImage.SetPixel(x, y, isStrong ? Color.FromArgb(strongPixelValue, strongPixelValue, strongPixelValue) : Color.FromArgb(0, 0, 0));
                    }
                }
            }

            return resultImage;
        }
        // Helper function for clamping (optional, uncomment if using)
        public static int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
        public static int Clamp(double value, int min, int max)
        {
            return (int)Math.Min(Math.Max(value, min), max);
        }
    }
}
