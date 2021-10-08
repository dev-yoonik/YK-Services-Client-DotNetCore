using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using YooniK.Services.Client.Common;

namespace YooniK.Services.Client
{
    /// <summary>
    ///     Centralizes the handling of the HTTP communication of the DotNet YooniK SDKs to its APIs.
    /// </summary>
    public class ServiceClient : IServiceClient
    {
        // HTTP Client used in all the HTTP transactions hereby called.
        private HttpClient _httpClient;


        /// <summary>
        ///     Handles sending the HTTPRequestMessage and returning the Response Content in string format.
        /// </summary>
        /// <param name="message"> Request Message Information. </param>
        /// <returns> HTTP Response Message Content in string. </returns>
        private async Task<string> SendRequestHandlerAsync(IRequestMessage message)
        {
            try
            {
                using HttpRequestMessage httpRequest = Utils.GetHttpRequestMessage(message, _httpClient.BaseAddress);

                using HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

                httpResponse.EnsureSuccessStatusCode();
                return await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="clientInformation"></param>
        public ServiceClient(IConnectionInformation connectionInformation)
        {
            if (connectionInformation is null)
            {
                throw new ArgumentNullException(nameof(connectionInformation));
            }

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(connectionInformation.BaseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("x-api-key", connectionInformation.SubscriptionKey);
            _httpClient.Timeout = TimeSpan.FromSeconds(70);
        }

        /// <summary>
        ///     Sends the HTTP Request Message with the custom RequestMessage information.
        ///     Deserializes the JSON HTTP Response Content onto chosen Type.
        ///     Warning: 
        ///         In case of `T` (Type Parameter) is of type string, an exception will be thrown.
        ///         Use the alternative RequestAsync method.
        /// </summary>
        /// <typeparam name="T"> Identifies object type of the JSON deserialization. </typeparam>
        /// <param name="requestMessage"> Request Message Information. </param>
        /// <returns> HTTP Response Content in T (type) object. </returns>
        public async Task<T> SendRequestAsync<T>(IRequestMessage requestMessage)
        {
            return JsonConvert.DeserializeObject<T>(await SendRequestHandlerAsync(requestMessage));
        }

        
        /// <summary>
        ///     Sends the HTTP Request Message with the custom RequestMessage information.
        /// </summary>
        /// <param name="requestMessage"> Request Message Information. </param>
        /// <returns> JSON string of the HTTP Response Content </returns>
        public async Task<string> SendRequestAsync(IRequestMessage requestMessage)
        {
            return await SendRequestHandlerAsync(requestMessage);
        }
    }
}
