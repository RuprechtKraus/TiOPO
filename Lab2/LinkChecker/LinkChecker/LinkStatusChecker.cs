using System.Net;
using System.Text.RegularExpressions;
using LinkChecker.Utilities;
using OpenQA.Selenium;

namespace LinkChecker
{
    public struct LinkCheckingResult
    {
        public int ValidLinksCount;
        public int InvalidLinksCount;
        public IDictionary<string, HttpResponseMessage> Responses;

        public LinkCheckingResult()
        {
            ValidLinksCount = 0;
            InvalidLinksCount = 0;
            Responses = new Dictionary<string, HttpResponseMessage>();
        }
    }

    public class LinkStatusChecker
    {
        private readonly Regex _linkPattern;
        public string Url { get; }
        public IWebDriver WebDriver { get; }

        public LinkStatusChecker( string url, IWebDriver webDriver )
        {
            Url = url;
            WebDriver = webDriver;

            if ( !UrlValidator.IsValidUrl( Url ) )
            {
                throw new ArgumentException( "Url is not valid" );
            }

            if ( Url.Last() != '/' )
            {
                Url += '/';
            }

            _linkPattern = new Regex( Url + "(?:[^#][-a-zA-Z0-9()@:%_\\+.#~?&\\/=]*)" );
        }

        

        public async Task<LinkCheckingResult> CheckLinks()
        {
            WebDriver.Url = Url;

            List<IWebElement> links = FindValidDistinctLinks();
            LinkCheckingResult result = await ProcessLinks( links );

            return result;
        }

        private List<IWebElement> FindValidDistinctLinks()
        {
            List<IWebElement> links = WebDriver.FindElements( By.TagName( "a" ) ).ToList();
            links = RemoveEmptyAndDuplicateLinks( links );

            return links;
        }

        private List<IWebElement> RemoveEmptyAndDuplicateLinks( List<IWebElement> links )
        {
            List<IWebElement> copy = links;

            copy.RemoveAll( x =>
            {
                if ( !x.IsLink() )
                {
                    throw new ArgumentException( "WebElement is not a link" );
                }

                string href = x.GetAttribute( "href" );
                if ( href == null || !_linkPattern.Match( href ).Success )
                {
                    return true;
                }
                return false;
            } );

            return copy.DistinctBy( x => x.GetAttribute( "href" ) ).ToList();
        }

        private static async Task<LinkCheckingResult> ProcessLinks( List<IWebElement> links )
        {
            HttpClient httpClient = new();
            LinkCheckingResult result = new();

            foreach ( IWebElement link in links )
            {
                string href = link.GetAttribute( "href" );
                HttpResponseMessage response = await httpClient.GetAsync( href );

                if ( !link.IsLink() )
                {
                    throw new ArgumentException( "WebElement is not a link" );
                }

                if ( response.IsSuccessStatusCode )
                {
                    result.ValidLinksCount++;
                }
                else
                {
                    result.InvalidLinksCount++;
                }

                result.Responses[ href ] = response;
            }

            return result;
        }
    }
}
