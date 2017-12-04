using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace MathMod_D_A
{
    static class CreateMatrixFormFile
    {
        public static int[,] ReadFile(string fileName = "INPUT.txt")
        {
            StreamReader Reader = DeclareStreamReader(fileName);
            int[,] matrix = FileReadLogic(Reader);
            return matrix;
        }
        private static int[,] FileReadLogic(StreamReader Reader) {

            string WholeFile = Reader.ReadToEnd();
            string BeforeEndLine = SelectFirstLine(WholeFile);
            var ElementsInFile = FindAllDigitalInString(WholeFile);
            var ElementsInFirstFileLine = FindAllDigitalInString(BeforeEndLine);
            int CountOfElementsInFile = ElementsInFile.Count;
            int CountOfElementsInFirstFileLine = ElementsInFirstFileLine.Count;
            if ((CountOfElementsInFile / CountOfElementsInFirstFileLine) != CountOfElementsInFirstFileLine)
            {
                Console.WriteLine("Matrix is not valid");
            }
            else
                Console.WriteLine("Matrix is valid");
            Console.ReadKey();
            return null;
        }
        private static StreamReader DeclareStreamReader(string fileName)
        {
            StreamReader Reader = null;
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                Reader = new StreamReader(file);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(1);
            }
            return Reader;
        }
        private static string SelectFirstLine(string WholeString)
        {
            string BeforeEndOfLine = "";
            for (int i = 0; i < WholeString.Length; i++)
            {
                if (WholeString[i] == '\n')
                    break;
                BeforeEndOfLine += WholeString[i];
            }
            return BeforeEndOfLine;
        }
        private static MatchCollection FindAllDigitalInString(string WholeString)
        {
            Regex reg = new Regex(@"\d{2}|\d");
            var Elements = reg.Matches(WholeString);
            return Elements;
        }
    }
}
