using System;
using System.Text;

namespace ConsoleApplication14
{
    public class sweepMethod
    {
        private struct coefficients
        {
            public double a;
            public double b;
            public double c;
            public double d;
            public double u;
            public double v;
        };

        private static void printRoots(double[] roots)
        {
            for (var i = 0; i < roots.Length; i++)
            {
                Console.Write(new StringBuilder("x").Append(i).Append(": "));
                Console.WriteLine(roots[i]);
            }
        }
        
        private static double[] getRoots(coefficients[] coefficientMatrix)
        {
            var roots = new double[coefficientMatrix.Length];
            roots[roots.Length - 1] = coefficientMatrix[coefficientMatrix.Length - 1].v; 
            
            for (var i = coefficientMatrix.Length-2; i >= 0; i--)
            {
                roots[i] = coefficientMatrix[i].u * roots[i + 1] + coefficientMatrix[i].v;
            }

            return roots;
        }

        private static void printCoefficients(coefficients[] coefficientMatrix)
        {
            foreach (var value in coefficientMatrix)
            {
                Console.Write(value.a);
                Console.Write('\t');
                Console.Write(value.b);
                Console.Write('\t');
                Console.Write(value.c);
                Console.Write('\t');
                Console.Write(value.d);
                Console.Write('\t');
                Console.Write(Math.Round(value.u, 4));
                Console.Write('\t');
                Console.Write(Math.Round(value.v, 4));
                Console.Write('\t');
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        
        private static coefficients[] getCoefficients(double[,] matrix)
        {
            var lines = matrix.GetLength(0);
            var columns = matrix.GetLength(1);
            var coefficientMatrix = new coefficients[lines];

            for (var i = 0; i < lines; i++)
            {
                coefficientMatrix[i].a = i == 0 ? 0 : matrix[i, i - 1];
                coefficientMatrix[i].b = matrix[i, i];
                coefficientMatrix[i].c = i == lines - 1 ? 0 : matrix[i, i + 1];
                coefficientMatrix[i].d = matrix[i, columns - 1];
            }

            coefficientMatrix[0].u = (-1) * coefficientMatrix[0].c / coefficientMatrix[0].b;
            coefficientMatrix[0].v = coefficientMatrix[0].d / coefficientMatrix[0].b;

            for (var i = 1; i < lines; i++)
            {
                coefficientMatrix[i].u = (-1) * coefficientMatrix[i].c /
                                         (coefficientMatrix[i].a * coefficientMatrix[i - 1].u + coefficientMatrix[i].b);
                coefficientMatrix[i].v =
                    (coefficientMatrix[i].d - coefficientMatrix[i].a * coefficientMatrix[i - 1].v) /
                    (coefficientMatrix[i].a * coefficientMatrix[i - 1].u + coefficientMatrix[i].b);
            }

            return coefficientMatrix;
        }
        
        public static void SolveSystem(double[,] matrix)
        {
            Console.WriteLine("Исходная матрица:");
            Printing.printMatrix(matrix);
            var coefficientsMatrix = getCoefficients(matrix);
            Console.WriteLine("Матрица коэффициентов (a, b, c, d, u, v):");
            printCoefficients(coefficientsMatrix);
            var rootsMatrix = getRoots(coefficientsMatrix);
            printRoots(rootsMatrix);
        }
    }
}