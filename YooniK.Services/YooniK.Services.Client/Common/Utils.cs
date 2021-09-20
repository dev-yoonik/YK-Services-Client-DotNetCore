using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;

namespace YooniK.Services.Client.Common
{
    public class Utils
    {
        public static Random RandomSeed = new Random();

        /// <summary>
        ///     Validates if the given string isn't null, empty or whiteSpace.
        /// </summary>
        /// <param name="stringToValidate">String to analyze.</param>
        public static void StringValidation(string stringToValidate)
        {
            if (String.IsNullOrWhiteSpace(stringToValidate))
            {
                throw new ArgumentException("Invalid argument. String is null, empty or white spaces.");
            }
        }


        /// <summary>
        ///     Builds an HttpRequestMessage object with a provided IRequestMessage.
        ///     Since the RequestMessage only stores the relative path, its required to pass the baseUrl, so we can join them both.
        /// </summary>
        /// <param name="message"> RequestMessage. </param>
        /// <param name="baseUrl"> Base URL. </param>
        /// <returns> HttpRequestMessage with all the custom parameters built-in. </returns>
        public static HttpRequestMessage GetHttpRequestMessage(IRequestMessage message, Uri baseUrl)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(message.HttpMethod, message.UrlRelativePath);

            if (message.Header != null)
                foreach (KeyValuePair<string, string> header in message.Header)
                    httpRequest.Headers.Add(header.Key, header.Value);

            httpRequest.RequestUri = GetRequestUri(baseUrl, httpRequest.RequestUri, message.QueryString);

            if (message.Request != null)
                httpRequest.Content = new StringContent(message.Request.ToJson(), Encoding.UTF8, "application/json");

            return httpRequest;
        }

        /// <summary>
        ///     Concatenates the base URL, with the relative path and the query.
        /// </summary>
        /// <param name="baseUri"> Uri with the BaseUrl. </param>
        /// <param name="requestUri"> Uri with the custom RequestUrl. </param>
        /// <param name="queryString"> Custom query string to added to Url. </param>
        /// <returns> Uri concatenated. </returns>
        public static Uri GetRequestUri(Uri baseUri, Uri requestUri, Dictionary<string, string> queryString)
        {
            try
            {
                UriBuilder builder = new UriBuilder(baseUri);
                builder.Path += requestUri.OriginalString;

                if (queryString != null)
                {
                    var queryCollection = HttpUtility.ParseQueryString(builder.Query);

                    foreach (KeyValuePair<string, string> query in queryString)
                        queryCollection.Add(query.Key, query.Value);

                    builder.Query = queryCollection.ToString();
                }
                return builder.Uri;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
