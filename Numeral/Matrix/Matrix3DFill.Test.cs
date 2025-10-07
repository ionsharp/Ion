using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ion.Numeral;

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ion.Numeral
{
    public class Program
    {
        public enum Matrix3DFillSide 
        {
            Default,
            Back,
            Bottom,
            Front,
            Left,
            Right,
            Top
        }

        public static T[][][] Format<T>(T[][] i, int slices, Matrix3DFillSide side)
        {
            (int Y, int X) length = (i.Length, i[0].Length);

            T[][][] result = null;

            int zCount = slices, yCount = length.Y, xCount = length.X;

            switch (side)
            {
                case Matrix3DFillSide.Back:
                case Matrix3DFillSide.Front:
                    result = new T[slices][][];
                    break;
                case Matrix3DFillSide.Bottom:
                case Matrix3DFillSide.Top:
                    result = new T[length.Y][][];

                    yCount = slices;
                    zCount = length.Y;
                    break;
                case Matrix3DFillSide.Left:
                case Matrix3DFillSide.Right:
                    result = new T[length.X][][];

                    xCount = slices;
                    zCount = length.X;
                    break;
            }

            for (var z = 0; z < zCount; z++)
            {
                switch (side)
                {
                    case Matrix3DFillSide.Back:
                    case Matrix3DFillSide.Front:
                    case Matrix3DFillSide.Left:
                    case Matrix3DFillSide.Right:
                        result[z] = new T[length.Y][];
                        break;
                    case Matrix3DFillSide.Bottom:
                    case Matrix3DFillSide.Top:
                        result[z] = new T[slices][];
                        break;
                }
                for (var y = 0; y < yCount; y++)
                {
                    switch (side)
                    {
                        case Matrix3DFillSide.Back:
                        case Matrix3DFillSide.Front:
                        case Matrix3DFillSide.Bottom:
                        case Matrix3DFillSide.Top:
                            result[z][y] = new T[length.X];
                            break;
                        case Matrix3DFillSide.Left:
                        case Matrix3DFillSide.Right:
                            result[z][y] = new T[slices];
                            break;
                    }
                    for (var x = 0; x < xCount; x++)
                    {
                        switch (side)
                        {
                            case Matrix3DFillSide.Back:
                                result[z][y][x] = i[y][xCount - 1 - x];
                                break;
                            case Matrix3DFillSide.Bottom:
                                result[z][y][x] = i[z][x];
                                break;
                            case Matrix3DFillSide.Front:
                                result[z][y][x] = i[y][x];
                                break;
                            case Matrix3DFillSide.Left:
                                result[z][y][x] = i[y][zCount - 1 - z];
                                break;
                            case Matrix3DFillSide.Right:
                                result[z][y][x] = i[y][z];
                                break;
                            case Matrix3DFillSide.Top:
                                result[z][y][x] = i[zCount - 1 - z][x];
                                break;
                        }
                    }
                }
            }

                    ToString(result, side);
            return result;
        }

        public static void ToString<T>(T[][][] i, Matrix3DFillSide side) 
        {
            (int Z, int Y, int X) length = (i.Length, i[0].Length, i[0][0].Length);
            Console.Write($"\nSide = {side}\n");
            for (var z = 0; z < length.Z; z++)
            {
                Console.Write($"\nZ = {z}\n");
                for (var y = 0; y < length.Y; y++)
                {
                    for (var x = 0; x < length.X; x++)
                    {
                      Console.Write($"{i[z][y][x]}, ");
                    }
                    Console.Write("\n");
                }
            }
        }

        public static void Main(string[] args)
        {
            var a = new double[][] 
            {
                new double[] { 1, 2, 3},
                new double[] { 4, 5, 6},
                new double[] { 7, 8, 9},
            };

            var b1 = Format(a, 3, Matrix3DFillSide.Back);
            var b2 = Format(a, 3, Matrix3DFillSide.Bottom);
            var b3 = Format(a, 3, Matrix3DFillSide.Front);
            var b4 = Format(a, 3, Matrix3DFillSide.Left);
            var b5 = Format(a, 3, Matrix3DFillSide.Right);
            var b6 = Format(a, 3, Matrix3DFillSide.Top);
        }
    }
}
*/