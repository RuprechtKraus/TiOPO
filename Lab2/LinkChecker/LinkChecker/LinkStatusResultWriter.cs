
namespace LinkChecker
{
    public static class LinkStatusResultWriter
    {
        public static void WriteToFile(
            LinkCheckingResult result,
            string validLinksPath,
            string invalidLinksPath )
        {
            using StreamWriter validLinks = File.CreateText( validLinksPath );
            using StreamWriter invalidLinks = File.CreateText( invalidLinksPath );

            foreach ( var urlResponse in result.Responses )
            {
                if ( urlResponse.Value.IsSuccessStatusCode )
                {
                    validLinks.WriteLine( @"Link: {0}, Status: {1}",
                        urlResponse.Key, urlResponse.Value.StatusCode );
                }
                else
                {
                    invalidLinks.WriteLine( @"Link: {0}, Status: {1}",
                        urlResponse.Key, urlResponse.Value.StatusCode );
                }
            }

            validLinks.WriteLine( @"Links in total: {0}", result.ValidLinksCount );
            validLinks.WriteLine( @"Check date: {0}", DateTime.Now );

            invalidLinks.WriteLine( @"Links in total: {0}", result.InvalidLinksCount );
            invalidLinks.WriteLine( @"Check date: {0}", DateTime.Now );
        }
    }
}
