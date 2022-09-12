using TriangleTestLib.Exceptions;

namespace TriangleTestLib
{
    public static class ArgsParser
    {
        public struct ProgramArgs
        {
            public string PathToExe { get; }
            public string InputFile { get; }
            public string OutputFile { get; }

            public ProgramArgs(
                string pathToExe,
                string inputFile,
                string outputFile )
            {
                PathToExe = pathToExe;
                InputFile = inputFile;
                OutputFile = outputFile;
            }
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgsParseException"></exception>
        public static ProgramArgs ParseArgs( string[] args )
        {
            if ( args == null)
            {
                throw new ArgumentNullException( nameof( args ) );
            }

            if ( args.Length != 2 && args.Length != 3 )
            {
                throw new ArgsParseException( "Неверное количество аргументов" );
            }

            string pathToExe = args[ 0 ];
            string inputFile = args[ 1 ];
            string outputFile = args.Length == 3 ? args[ 2 ] : "test_result.txt";
            ProgramArgs programArgs = new ProgramArgs( pathToExe, inputFile, outputFile );

            return programArgs;
        }
    }
}
