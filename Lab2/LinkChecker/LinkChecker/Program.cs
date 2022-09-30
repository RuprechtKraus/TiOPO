using LinkChecker;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

if ( args.Length != 3 )
{
    Console.WriteLine(
        "Expected 3 arguments: <url> <valid_links output> <invalid_links output>" );
    return;
}

IWebDriver webDriver = new FirefoxDriver();

try
{
    LinkStatusChecker linkChecker = new( args[0], webDriver );
    LinkCheckingResult result = await linkChecker.CheckLinks();
    LinkStatusResultWriter.WriteToFile( result, args[1], args[2] );
}
catch ( Exception e )
{
    Console.Error.WriteLine( e.Message );
}
finally
{
    webDriver.Quit();
}
