using TriangleTestLib;
using TriangleTestLib.Exceptions;
using static TriangleTestLib.ArgsParser;

try
{
    ProgramArgs programArgs = ParseArgs( args );
    TriangleTester tester = new TriangleTester( 
        programArgs.PathToExe, 
        programArgs.InputFile, 
        programArgs.OutputFile );
    tester.Run();
}
catch ( ArgsParseException )
{
    Console.WriteLine( "Ошибка парсинга аргументов программы\n" +
        "Использование аргументов: " +
        "<тестируемый файл> " +
        "<входной файл с тестовыми данными> " +
        "<выходной файл с результатами>(опц.)" );
}
catch ( Exception e )
{
    Console.WriteLine( e.Message );
}
