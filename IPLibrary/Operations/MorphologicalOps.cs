using System.Drawing;
using System;

namespace IPLibrary
{
    public enum MorphologicalOps
    {
        Erosion,
        Dilation,
        Opening,
        Closing
    }

    public enum MorphologicalStructuringElements
    {
        Square,
        Disc,
        LineSegment,
    }

    public static partial class IP
    {
        public static Bitmap ApplyMorphologicalOperation(Bitmap image, bool[,] structuringElement, MorphologicalOps operation = MorphologicalOps.Erosion)
        {
            if (image == null || structuringElement == null)
            {
                throw new ArgumentNullException();
            }

            int height = image.Height;
            int width = image.Width;
            Bitmap resultImage = new Bitmap(width, height);

            // Pad the image with zeros for border handling
            int padX = structuringElement.GetLength(0) / 2;
            int padY = structuringElement.GetLength(1) / 2;
            Bitmap paddedImage = new Bitmap(width + 2 * padX, height + 2 * padY);
            using (Graphics g = Graphics.FromImage(paddedImage))
            {
                g.DrawImage(image, new Rectangle(padX, padY, width, height));
            }

            for (int y = padY; y < height + padY; y++)
            {
                for (int x = padX; x < width + padX; x++)
                {
                    int newPixelValue = 255; // Assuming image uses 8-bit grayscale (0-255)
                    for (int dy = -padY; dy <= padY; dy++)
                    {
                        for (int dx = -padX; dx <= padX; dx++)
                        {
                            int neighborX = x + dx;
                            int neighborY = y + dy;
                            bool isWithinImage = neighborX >= 0 && neighborX < width + 2 * padX && neighborY >= 0 && neighborY < height + 2 * padY;

                            // Apply structuring element and operation logic
                            if (isWithinImage && structuringElement[dx + padX, dy + padY])
                            {
                                int neighborValue = paddedImage.GetPixel(neighborX, neighborY).R;
                                switch (operation)
                                {
                                    case MorphologicalOps.Erosion:
                                        if (neighborValue < newPixelValue)
                                        {
                                            newPixelValue = neighborValue;
                                        }
                                        break;
                                    case MorphologicalOps.Dilation:
                                        if (neighborValue > newPixelValue)
                                        {
                                            newPixelValue = neighborValue;
                                        }
                                        break;
                                    case MorphologicalOps.Opening:
                                        if (neighborValue < newPixelValue)
                                        {
                                            newPixelValue = neighborValue;
                                        }
                                        break;
                                    case MorphologicalOps.Closing:
                                        if (neighborValue > newPixelValue)
                                        {
                                            newPixelValue = neighborValue;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    resultImage.SetPixel(x - padX, y - padY, Color.FromArgb(newPixelValue, newPixelValue, newPixelValue));
                }
            }
            paddedImage.Dispose();
            return resultImage;
        }

        public static bool[,] GenerateStructuringElement(MorphologicalStructuringElements elementType, int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException("Size must be a positive integer.");
            }

            bool[,] structuringElement;
            switch (elementType)
            {
                case MorphologicalStructuringElements.Square:
                    structuringElement = CreateSquareStructuringElement(size);
                    break;
                case MorphologicalStructuringElements.Disc:
                    structuringElement = CreateDiscStructuringElement(size);
                    break;
                case MorphologicalStructuringElements.LineSegment:
                    structuringElement = CreateLineSegmentStructuringElement(size);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("elementType", "Invalid structuring element type.");
            }

            return structuringElement;
        }

        private static bool[,] CreateSquareStructuringElement(int size)
        {
            bool[,] square = new bool[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    square[y, x] = true;
                }
            }
            return square;
        }

        private static bool[,] CreateDiscStructuringElement(int radius)
        {
            int diameter = radius * 2 + 1;
            bool[,] disc = new bool[diameter, diameter];
            int centerX = radius;
            int centerY = radius;

            for (int y = 0; y < diameter; y++)
            {
                for (int x = 0; x < diameter; x++)
                {
                    int distanceSquared = (x - centerX) * (x - centerX) + (y - centerY) * (y - centerY);
                    disc[y, x] = distanceSquared <= radius * radius;
                }
            }
            return disc;
        }

        private static bool[,] CreateLineSegmentStructuringElement(int length)
        {
            bool[,] line = new bool[length, 1];
            for (int y = 0; y < length; y++)
            {
                line[y, 0] = true;
            }
            return line;
        }
    }
}
