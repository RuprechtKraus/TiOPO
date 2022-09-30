using System.Net;
using OpenQA.Selenium;

namespace LinkChecker.Utilities
{
    public static class Extensions
    {
        public static bool IsLink( this IWebElement webElement )
        {
            return webElement.TagName == "a";
        }
    }
}
