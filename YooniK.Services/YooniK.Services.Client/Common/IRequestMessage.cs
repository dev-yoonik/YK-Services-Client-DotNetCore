using System.Collections.Generic;
using System.Net.Http;

namespace YooniK.Services.Client.Common
{
    /// <summary>
    ///     This interface allows the instantiation of classes that store needed information to customize the HttpRequestMessage.
    ///     
    ///     Customizable information:
    ///         (REQUIRED)  HTTP Method - identifies the Method intended for the HTTP Request
    ///                     HTTP Header - additional parameters, not included in the default configuration of the HttpClient instance
    ///                     URL Query String - custom query parameters to be inserted into the URL
    ///                     Relative Path - identifies the Relative Path of the URL
    ///                     Request - the API Model Request
    /// </summary>
    public interface IRequestMessage
    {
        public HttpMethod @HttpMethod { get; }
        
        public Dictionary<string, string> Header { get; }
        
        public Dictionary<string, string> QueryString { get; }
        public string UrlRelativePath { get; }
        public IRequest Request { get; }

    }
}
