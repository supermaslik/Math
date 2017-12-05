using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMod_D_A
{
    public static class GlobalFunctions
    {
        public static void Error(string errorText)
        {
            Console.WriteLine(errorText);
            Console.ReadKey();
            Environment.Exit(1);
        }
        public static void ShowMatrix<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write(matrix[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
