using System;
using System.Text;

namespace ConsoleApplication14
{
    public class Gauss
    {
        private struct roots
        {
            public double x1;
            public double x2;
            public double x3;
            public double x4;
        }

        private static double[,] sumLines(double[,] matrix, int firstLine, int secondLine)
        {
            var columns = matrix.GetLength(1);

            for (var i = 0; i < columns; i++)
            {
                matrix[firstLine, i] += matrix[secondLine, i];
            }

            return matrix;
        }

        private static double[,] multiplyLine(double[,] matrix, int line, double value)
        {
            var columns = matrix.GetLength(1);

            for (var i = 0; i < columns; i++)
            {
                matrix[line, i] *= value;
            }

            return matrix;
        }

        private static double[,] resetColumn(double[,] matrix, int column)
        {
            var columns = matrix.GetLength(1);
            var startValue = matrix[column, column];

            for (var i = column + 1; i < columns - 1; i++)
            {
                var value = startValue * (-1) / matrix[i, column];
                matrix = multiplyLine(matrix, i, value);
                matrix = sumLines(matrix, i, column);
            }

            return matrix;
        }

        private static double[,] createDiagonalView(double[,] matrix)
        {
            var lines = matrix.GetLength(0);

            for (var i = 0; i < lines; i++)
            {
                matrix = resetColumn(matrix, i);
            }

            return matrix;
        }

        private static roots getRoots(double[,] matrix)
        {
            var fourthRoot = matrix[3, 4] / matrix[3, 3];
            var thirdRoot = (matrix[2, 4] - matrix[2, 3] * fourthRoot) / matrix[2, 2];
            var secondRoot = (matrix[1, 4] - matrix[1, 3] * fourthRoot - matrix[1, 2] * thirdRoot) / matrix[1, 2];
            var firstRoot =
                (matrix[0, 4] - matrix[0, 3] * fourthRoot - matrix[0, 2] * thirdRoot - matrix[0, 1] * secondRoot) /
                matrix[0, 0];

            return new roots {x1 = firstRoot, x2 = secondRoot, x3 = thirdRoot, x4 = fourthRoot};
        }

        private static void printMatrix(double[,] matrix)
        {
            var lines = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            for (var i = 0; i < lines; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    Console.Write(Math.Round(matrix[i, j], 2));
                    Console.Write('\t');
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void printRoots(roots values)
        {
            Console.WriteLine(new StringBuilder("x1 = ").Append(values.x1));
            Console.WriteLine(new StringBuilder("x2 = ").Append(values.x2));
            Console.WriteLine(new StringBuilder("x3 = ").Append(values.x3));
            Console.WriteLine(new StringBuilder("x4 = ").Append(values.x4));
        }

        public static void SolveSystem(double[,] matrix)
        {
            Console.WriteLine("Исходная матрица:");
            printMatrix(matrix);
            Console.WriteLine("Матрица, приведенная к диагональному виду:");
            matrix = createDiagonalView(matrix);
            printMatrix(matrix);
            Console.WriteLine("Корни:");
            var roots = getRoots(matrix);
            printRoots(roots);
        }
    }
}