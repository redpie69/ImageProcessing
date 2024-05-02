using System;

namespace IPLibrary.Utility
{
    internal partial class MatrixOps
    {
        /// <summary>
        /// Creates a transformation matrix for a given transformation type and parameters.
        /// </summary>
        /// <param name="type">The transformation type.</param>
        /// <param name="parameters">The transformation parameters.</param>
        /// <returns>The transformation matrix.</returns>
        public static double[,] TransformMatrisCreator(Transforms type, params double[] parameters)
        {
            double[,] transformMatrix = new double[3, 3];

            switch (type)
            {
                case Transforms.Rotate:
                    double cos = Math.Cos(parameters[0]);
                    double sin = Math.Sin(parameters[0]);
                    transformMatrix[0, 0] = cos;
                    transformMatrix[0, 1] = -sin;
                    transformMatrix[1, 0] = sin;
                    transformMatrix[1, 1] = cos;
                    transformMatrix[2, 2] = 1;
                    break;
                case Transforms.Translation:
                    transformMatrix[0, 0] = 1;
                    transformMatrix[0, 2] = parameters[0];
                    transformMatrix[1, 1] = 1;
                    transformMatrix[1, 2] = parameters[1];
                    transformMatrix[2, 2] = 1;
                    break;
                case Transforms.Scaling:
                    transformMatrix[0, 0] = parameters[0];
                    transformMatrix[1, 1] = parameters[1];
                    transformMatrix[2, 2] = 1;
                    break;
                case Transforms.ShearVertical:
                    transformMatrix[0, 0] = 1;
                    transformMatrix[0, 1] = parameters[0];
                    transformMatrix[1, 1] = 1;
                    transformMatrix[2, 2] = 1;
                    break;
                case Transforms.ShearHorizantal:
                    transformMatrix[0, 0] = 1;
                    transformMatrix[1, 0] = parameters[0];
                    transformMatrix[1, 1] = 1;
                    transformMatrix[2, 2] = 1;
                    break;
                default:
                    throw new ArgumentException("Invalid argument");
            }

            return transformMatrix;
        }
    }
}
