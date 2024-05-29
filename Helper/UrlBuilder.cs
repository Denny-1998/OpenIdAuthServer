using System.Web;

namespace OpenIdAuthServer.Helper
{
    public class UrlBuilder
    {
        string _url, _parameters;
        public string UrlWithParams {  get; private set; }

        public UrlBuilder(string url, Dictionary<string,string> parameters) 
        { 
            _url = url;
            _parameters = BuildQueryString(parameters);

            if (parameters.Count != 0)
                UrlWithParams = _url + "?" + _parameters;
            else
                UrlWithParams = _url;

        }


        string BuildQueryString(Dictionary<string, string> parameters)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (var parameter in parameters)
            {
                query[parameter.Key] = parameter.Value;
            }
            return query.ToString();
        }


    }
}
