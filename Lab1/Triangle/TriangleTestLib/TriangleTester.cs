using System.Diagnostics;
using System.Text.RegularExpressions;
using TriangleTestLib.Exceptions;

namespace TriangleTestLib
{
    struct TestArgs
    {
        public string Sides { get; }
        public string ExpectedResult { get; }

        public TestArgs( string sides, string expectedResult )
        {
            Sides = sides;
            ExpectedResult = expectedResult;
        }
    }

    public class TriangleTester
    {
        public string PathToExe { get; set; }
        public string InputFile { get; set; }
        public string OutputFile { get; set; }

        public TriangleTester( string pathToExe, string inputFile, string outputFile )
        {
            PathToExe = pathToExe;
            InputFile = inputFile;
            OutputFile = outputFile;
        }

        /// <summary>
        /// Запускает тест и сохраняет результат в файл вывода
        /// </summary>
        /// <exception cref="TestArgsParseException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.ComponentModel.Win32Exception"></exception>
        public void Run()
        {
            StreamWriter fs = File.CreateText( OutputFile );
            fs.Close();

            foreach ( string line in File.ReadLines( InputFile ) )
            {
                TestArgs testArgs = ParseTestArgs( line );

                using Process process = new Process();
                process.StartInfo.FileName = PathToExe;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.Arguments = testArgs.Sides;
                process.Start();

                StreamReader reader = process.StandardOutput;
                string output = reader.ReadToEnd().RemoveNonPrintableCharacters();
                string testResult = CompareAndGetTestResult( testArgs.ExpectedResult, output );

                using ( StreamWriter sw = File.AppendText( OutputFile ) )
                {
                    sw.WriteLine( testResult );
                }

                process.WaitForExit();
            }
        }

        private static TestArgs ParseTestArgs( string line )
        {
            if ( line == null )
            {
                throw new ArgumentNullException( nameof( line ) );
            }

            Match match = Regex.Match( line, "^(([-\\,\\.0-9a-zA-Z]+\\s)+)?((\\\".*\\\")|(\\S*))\\r?$" );

            if ( !match.Success )
            {
                throw new TestArgsParseException( "Ошибка парсинга файла" );
            }

            string sides = match.Groups[ 1 ].Value;
            string expectedResult = match.Groups[ 3 ].Value.Replace("\"", "");
            TestArgs triangleArgs = new( sides, expectedResult );

            return triangleArgs;
        }

        private static string CompareAndGetTestResult( string expectedOutput, string actualOutput )
        {
            return expectedOutput.ToLower() == actualOutput.ToLower() ? "success" : "error";
        }
    }
}
