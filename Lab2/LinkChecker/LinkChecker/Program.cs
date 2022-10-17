using LinkChecker;

if ( args.Length != 3 )
{
    Console.WriteLine(
        "Expected 3 arguments: <url> <valid_links output> <invalid_links output>" );
    return;
}

try
{
    LinkStatusChecker linkChecker = new();
    LinkCheckingResult result = await linkChecker.CheckLinks( args[ 0 ] );
    LinkStatusResultWriter.WriteToFile( result, args[ 1 ], args[ 2 ] );
}
catch ( Exception e )
{
    Console.Error.WriteLine( e.Message );
}
