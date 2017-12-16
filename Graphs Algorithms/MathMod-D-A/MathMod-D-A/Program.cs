using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathMod_D_A;

namespace MathMod_D_A
{
    class Program
    {

        static void Main(string[] args)
        {
            var matrix = CreateMatrixFormFile.ReadFile();
            GlobalFunctions.ShowMatrix<int>(matrix);
            DijkstraAlgorithm.FindShortestWayByMatrixDesirialize(matrix, 1, 4);
            Console.ReadKey();
        }
    }
}
