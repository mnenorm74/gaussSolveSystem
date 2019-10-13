using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication14
{
    internal class Program
    {
        private static string getCoefficient(double value)
        {
            return Math.Abs(value - 1) < 1e-5 ? "" : value.ToString();
        }

        private static List<string> getEquations(double [,] matrix)
        {
            var equations = new List<string>();
            var lines = matrix.GetLength(0);
            var columns = matrix.GetLength(1);
            for (var i = 0; i < lines; i++)
            {
                var xIndex = 1;
                var equation = new StringBuilder();
                
                for (var j = 0; j < columns; j++)
                {
                    var coefficient = matrix[i, j];
                    if (j == 0 && Math.Abs(coefficient) > 1e-5)
                    {
                        equation.Append(getCoefficient(coefficient)).Append("x").Append(xIndex);
                    }
                    else if (j == columns - 1)
                    {
                        equation.Append(" = ").Append(coefficient);
                    }
                    else if (Math.Abs(coefficient) > 1e-5)
                    {
                        equation.Append(coefficient > 0 ? " + " : " - ").Append(getCoefficient(Math.Abs(coefficient)))
                            .Append("x").Append(xIndex);
                    }

                    xIndex++;
                }
                equations.Add(equation.ToString());
            }
            return equations;
        }
        private static void printInputData(double [,] matrix)
        {
            var equations = getEquations(matrix);
            Console.WriteLine("СЛУ:");
            foreach (var equation in equations)
            {
                Console.WriteLine(equation);
            }
            Console.WriteLine();
        }
        
        public static void Main(string[] args)
        {
            var matrix = new[,]
            {
                {4.0, 1, 1, 2, 2},
                {1, 3, 2, -1, 2},
                {2, -1, 5, 3, -1},
                {4, 5, 4, -4, 8}
            };
            printInputData(matrix);
            Gauss.SolveSystem(matrix);
        }
    }
}