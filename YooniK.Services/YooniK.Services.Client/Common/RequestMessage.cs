using System;
using System.Collections.Generic;
using System.Net.Http;

namespace YooniK.Services.Client.Common
{
    /// <summary>
    ///     RequestMessage implements IRequestMessage.
    ///     <see cref="IRequestMessage"/>
    /// </summary>
    public class RequestMessage : IRequestMessage
    {
        private HttpMethod _httpMethod;
        public Dictionary<string, string> Header { get; set; } = null;
        public Dictionary<string, string> QueryString { get; set; } = null;
        public string UrlRelativePath { get; set; } = null;
        public IRequest Request { get; set; } = null;

        /// <summary>
        ///     Constructor.
        ///     Only required parameter is HttpMethod, all other can be null.
        /// </summary>
        /// <param name="httpMethod"> Identifies the Method intended for the HTTP Request. </param>
        /// <param name="headers"> Additional parameters, not included in the default configuration of the HttpClient instance. </param>
        /// <param name="queryString"> Custom query parameters to be inserted into the URL. </param>
        /// <param name="request"> The API Model Request. </param>
        /// <param name="urlRelativePath"> Identifies the Relative Path of the URL. </param>
        public RequestMessage(HttpMethod httpMethod, Dictionary<string, string> header = null, Dictionary<string, string> queryString = null, IRequest request = null, string urlRelativePath = null)
        {
            @HttpMethod = httpMethod;
            Header = header;
            QueryString = queryString;
            Request = request;
            UrlRelativePath = urlRelativePath;
        }

        // Custom @HttpMethod Properties
        public HttpMethod @HttpMethod
        {
            get
            {
                return _httpMethod;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(@HttpMethod));
                _httpMethod = value;
            }
        }
    }
}
