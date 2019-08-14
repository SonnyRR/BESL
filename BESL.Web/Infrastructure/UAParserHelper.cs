namespace BESL.Web.Infrastructure
{
    using UAParser;

    public class UAParserHelper
    {
        public static string GetBrowserFamily(string userAgent)
        {
            var userAgentParser = Parser.GetDefault();
            ClientInfo clientInfo = userAgentParser.Parse(userAgent);

            return clientInfo.UA.Family;
        }

        public enum BrowserFamily
        {
            Chrome = 1,
            Opera = 2,
            Firefox = 3,
            Edge = 4,
            IE = 5
        }
    }
}
