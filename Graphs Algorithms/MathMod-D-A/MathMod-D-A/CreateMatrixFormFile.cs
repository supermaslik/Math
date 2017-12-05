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

            string WholeFile = ReadWholeFileAndCloseIt(Reader);

            if(IsCurrentMatrixNotValid(WholeFile))
            {
                GlobalFunctions.Error("Matrix is not valid, problem in size of matrix");
            }



            return CreateMatrixAfterPreparation(WholeFile);
        }

        private static int[,] CreateMatrixAfterPreparation(string wholeFile)
        {
            string beforeEndLine = SelectFirstLine(wholeFile);
            var ElementsInFirstFileLine = FindAllDigitalInString(beforeEndLine);
            int MatrixDemention = ElementsInFirstFileLine.Count;

            var WholeElements = FindAllDigitalInString(wholeFile);

            int[,] matrixForReturn = new int[MatrixDemention, MatrixDemention];
            int fcounter; int scounter;
            fcounter = scounter = 0;
            foreach(var diget in WholeElements)
            {
                matrixForReturn[fcounter, scounter++] = int.Parse(diget.ToString());
                if (scounter == MatrixDemention)
                {
                    fcounter++;
                    scounter = 0;
                }
            }
            return matrixForReturn;
        }

        private static bool IsCurrentMatrixNotValid(string wholeFile)
        {
            string beforeEndLine = SelectFirstLine(wholeFile);
            var ElementsInFile = FindAllDigitalInString(wholeFile);
            var ElementsInFirstFileLine = FindAllDigitalInString(beforeEndLine);

            int CountOfElementsInFile = ElementsInFile.Count;
            int CountOfElementsInFirstFileLine = ElementsInFirstFileLine.Count;
            float Rest = CountOfElementsInFile % CountOfElementsInFirstFileLine;
            if (((CountOfElementsInFile / CountOfElementsInFirstFileLine) != CountOfElementsInFirstFileLine) && Rest != 0)
                return true;
            return false;
        }
        private static string ReadWholeFileAndCloseIt(StreamReader reader)
        {
            string toReturn = reader.ReadToEnd();
            reader.Close();
            return toReturn;
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
