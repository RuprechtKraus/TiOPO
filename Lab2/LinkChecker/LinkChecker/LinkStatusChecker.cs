using HtmlAgilityPack;
using LinkChecker.Utilities;

namespace LinkChecker
{
    public struct LinkCheckingResult
    {
        public int ValidLinksCount;
        public int InvalidLinksCount;
        public Dictionary<string, HttpResponseMessage> Responses;

        public LinkCheckingResult()
        {
            ValidLinksCount = 0;
            InvalidLinksCount = 0;
            Responses = new Dictionary<string, HttpResponseMessage>();
        }
    }

    public class LinkStatusChecker
    {
        private string _url = string.Empty;
        private readonly HashSet<string> _visitedLinks = new();

        public async Task<LinkCheckingResult> CheckLinks( string url )
        {
            if ( !UrlValidator.IsValidUrl( url ) )
            {
                throw new ArgumentException( "Url is not valid" );
            }

            _url = url;
            AppendSlashToUrl( ref _url );

            HtmlWeb web = new();
            HttpClient httpClient = new();

            HashSet<string> links = GetAllUrlsFromPage( web, _url ).ToHashSet();
            HashSet<string> newLinks = new();

            LinkCheckingResult result = new();

            while ( links.Any() )
            {
                foreach ( var link in links )
                {
                    string absoluteUrl = string.Empty;

                    try
                    {
                        absoluteUrl = TidyAndAbsolutizeUrl( _url, link );
                    }
                    catch ( UriFormatException )
                    {
                        continue;
                    }

                    if ( !absoluteUrl.StartsWith( _url ) || _visitedLinks.Contains( absoluteUrl ) )
                    {
                        continue;
                    }

                    _visitedLinks.Add( absoluteUrl );
                    HttpResponseMessage response = await httpClient.GetAsync( absoluteUrl );

                    AddResponseToResult( ref result, absoluteUrl, response );
                    newLinks.UnionWith( GetAllUrlsFromPage( web, absoluteUrl ) );

                    Console.WriteLine( @"{0} {1}", absoluteUrl, response.StatusCode );
                }

                links = newLinks.ToHashSet();
                newLinks.Clear();
            }

            _url = string.Empty;
            _visitedLinks.Clear();

            return result;
        }

        static void AppendSlashToUrl( ref string url )
        {
            url = url.Last() != '/' ? url + '/' : url;
        }

        static string TidyAndAbsolutizeUrl( string baseUrl, string url )
        {
            string href = RemoveAnchor( url );
            href = RemoveQueryParams( href );

            return GetAbsoluteUrlString( baseUrl, href );
        }

        static string GetAbsoluteUrlString( string baseUrl, string url )
        {
            var uri = new Uri( url, UriKind.RelativeOrAbsolute );

            if ( !uri.IsAbsoluteUri )
            {
                uri = new Uri( new Uri( baseUrl ), uri );
            }

            return uri.ToString();
        }

        static string RemoveAnchor( string url )
        {
            int index = url.IndexOf( '#' );
            return index > -1 ? url.Remove( index ) : url;
        }

        static string RemoveQueryParams( string url )
        {
            int index = url.IndexOf( '?' );
            return index > -1 ? url.Remove( index ) : url;
        }

        static void AddResponseToResult(
            ref LinkCheckingResult result, string url, HttpResponseMessage response )
        {
            if ( response.IsSuccessStatusCode )
            {
                result.ValidLinksCount++;
            }
            else
            {
                result.InvalidLinksCount++;
            }

            result.Responses[ url ] = response;
        }

        static IEnumerable<string> GetAllUrlsFromPage( HtmlWeb web, string url )
        {
            HtmlDocument htmlDoc = web.Load( url );
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes( "//a[@href]" );

            return nodes != null ?
                nodes.Select( x => x.Attributes[ "href" ].Value ) :
                new List<string>();
        }
    }
}
