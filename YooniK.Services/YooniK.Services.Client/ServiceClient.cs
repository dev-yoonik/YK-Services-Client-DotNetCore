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
        ///     Policy in charge of blocking HTTP Requests when they repeatedly fail.
        ///     
        /// 
        ///     Circuit-Breaker tracks the number of time the HTTP Request response fails.
        ///     Using the Advanced configuration we don't rely on successive response fails to Open the Circuit.
        ///     We can define a time period in which the responses are analyzed: the percentage of failed responses, the minimum number
        ///     of responses and the time period in which the circuit remains open.
        ///     
        ///     Once the time period runs out the circuit-breaker will let one request through to "test" the API and see if it succeeds. 
        ///     If it fails, it goes back to just failing immediately. 
        ///     If it succeeds then the circuit is closed again, and it will go back to calling the API for every request.
        ///     
        ///     Terminology of Circuit Status:
        ///         Open: 
        ///             Requests are blocked.
        ///         Half Closed:
        ///             Lets one request through and validates the response status.
        ///         Closed:
        ///             Requests are sent.
        ///             
        /// </summary>
        private readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy = Policy
            .HandleResult<HttpResponseMessage>(
                resultPredicate: message => (int)message.StatusCode == 503
            )
            .AdvancedCircuitBreakerAsync(
                failureThreshold: 0.6,      // Percentage of failed responses in sample
                samplingDuration: TimeSpan.FromMinutes(2),  // Sample minimum time duration
                minimumThroughput: 150, // Sample minimum number of responses
                durationOfBreak: TimeSpan.FromMinutes(1.5)  // Duration in which the circuit is held open
            );

        
        /// <summary>
        ///     Policy responsible to retry sending HTTP Requests when failing.
        ///     Retries when: 
        ///         The HTTP Response has a 429 or 5xx Status Code.
        ///     
        ///     Max number of retries: 4
        ///     
        ///     Time between retries: 
        ///         Current number of retry to the power of 2 plus random milliseconds ranging 0 to a 1000.
        ///         
        /// </summary>
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy = Policy
            .HandleResult<HttpResponseMessage>(
                resultPredicate: message => (int)message.StatusCode == 429 || (int)message.StatusCode >= 500
            )
            .WaitAndRetryAsync(
                retryCount: 4,
                sleepDurationProvider:
                    retryAttempt => { return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(Utils.RandomSeed.Next(0, 1000)); }
            );


        /// <summary>
        ///     Handles sending the HTTPRequestMessage and returning the Response Content in string format.
        /// </summary>
        /// <param name="message"> Request Message Information. </param>
        /// <returns> HTTP Response Message Content in string. </returns>
        private async Task<string> RequestHandlerAsync(IRequestMessage message)
        {
            try
            {
                using HttpRequestMessage httpRequest = Utils.GetHttpRequestMessage(message, _httpClient.BaseAddress);

                using HttpResponseMessage httpResponse = await SendWithPoliciesAsync(_circuitBreakerPolicy, _retryPolicy, httpRequest);

                httpResponse.EnsureSuccessStatusCode();
                return await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        ///     Sends the HTTP Request Message to the Server/API.
        ///     The Response is passed to the Polly Methods, in order, to validate its predefined policies.
        /// </summary>
        /// <param name="httpRequest"> HTTP Request to be sent. </param>
        /// <returns> The HTTP Response Message. </returns>
        private async Task<HttpResponseMessage> SendWithPoliciesAsync(AsyncCircuitBreakerPolicy<HttpResponseMessage> circuitBreaker, AsyncRetryPolicy<HttpResponseMessage> retry, HttpRequestMessage httpRequest)
        {
            return await circuitBreaker.ExecuteAsync( () => retry.ExecuteAsync( () => _httpClient.SendAsync(httpRequest)));
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
        public async Task<T> RequestAsync<T>(IRequestMessage requestMessage)
        {
            return JsonConvert.DeserializeObject<T>(await RequestHandlerAsync(requestMessage));
        }

        
        /// <summary>
        ///     Sends the HTTP Request Message with the custom RequestMessage information.
        /// </summary>
        /// <param name="requestMessage"> Request Message Information. </param>
        /// <returns> JSON string of the HTTP Response Content </returns>
        public async Task<string> RequestAsync(IRequestMessage requestMessage)
        {
            return await RequestHandlerAsync(requestMessage);
        }
    }
}
