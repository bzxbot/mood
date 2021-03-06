﻿using System;
using Mood.GeoCalc;

namespace Mood
{
    public class Point3d
    {
        #region fields

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        #endregion Fields

        #region Constructors

        public Point3d()
            : this(0, 0, 0)
        {
        }

        public Point3d(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public Point3d(Point3d v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
        }

        #endregion Constructors

        #region Public Methods

        public double[,] ToArray()
        {
            return new double[,] { { this.X, this.Y, this.Z, 1 } };
        }

        public Matrix toMatrix()
        {
            return new Matrix(this.ToArray());
        }

        public override string ToString()
        {
            return String.Format("X: {0}, Y: {1}, Z: {2}", this.X, this.Y, this.Z);
        }

        public double DotProduct(Point3d v)
        {
            return (X * Y * Z) + (v.X * v.Y * v.Z);
        }

        #endregion Public Methods

        #region Public Static Methods

        public static Point3d operator -(Point3d v1, Point3d v2)
        {
            return new Point3d(v1.X - v2.X,
                    v1.Y - v2.Y,
                    v1.Z - v2.Z);

            //v1.X -= v2.X;
            //v1.Y -= v2.Y;
            //v1.Z -= v2.Z;

            //return v1;
        }

        public static Point3d operator +(Point3d v1, Point3d v2)
        {
            return new Point3d(v1.X + v2.X,
                                v1.Y + v2.Y,
                                v1.Z + v2.Z);

            //v1.X += v2.X;
            //v1.Y += v2.Y;
            //v1.Z += v2.Z;

            //return v1;
        }

        public static Point3d operator *(Point3d v1, Matrix m1)
        {
            Matrix mResult = v1.toMatrix() * m1;
            double[,] result = mResult.GetMatrix();

            if (result.GetLength(1) < 3)
            {
                throw new MatrixSizeException();
            }

            return new Point3d((float)result[0, 0], (float)result[0, 1], (float)result[0, 2]);       
        }

        public static Point3d Convert(Matrix m)
        {
            double[,] matrix = m.GetMatrix();
            if (matrix.GetLength(0) != 1 || matrix.GetLength(1) < 3)
            {
                throw new MatrixSizeException();
            }

            return new Point3d((float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2]);
        }

        #endregion Public Static Methods
    }
}
