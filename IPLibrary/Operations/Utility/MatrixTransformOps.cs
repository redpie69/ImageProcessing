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
            double[,] transformMatrix;
            switch (type)
            {
                case Transforms.Rotate:
                    transformMatrix = new double[3, 3] { { Math.Cos(parameters[0]), -Math.Sin(parameters[0]), 0 }, { Math.Sin(parameters[0]), Math.Cos(parameters[0]), 0 }, { 0, 0, 1 } };
                    break;
                case Transforms.Translation:
                    transformMatrix = new double[3, 3] { { 1, 0, parameters[0] }, { 0, 1, parameters[1] }, { 0, 0, 1 } };
                    break;
                case Transforms.Scaling:
                    transformMatrix = new double[3, 3] { { parameters[0], 0, 0 }, { 0, parameters[1], 0 }, { 0, 0, 1 } };
                    break;
                case Transforms.ShearVertical:
                    transformMatrix = new double[3, 3] { { 1, parameters[0], 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
                    break;
                case Transforms.ShearHorizantal:
                    transformMatrix = new double[3, 3] { { 1, 0, 0 }, { parameters[0], 1, 0 }, { 0, 0, 1 } };
                    break;
                default:
                    throw new ArgumentException("Invalid argument");
            }

            return transformMatrix;
        }

    }
}
