using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YooniK.Services.Client.Common
{
    /// <summary>
    ///     Class that stores the vital connection information.
    ///     Meaning, the base URL and its authentication subscription key.
    /// </summary>
    public class ConnectionInformation : IConnectionInformation
    {
        private string _baseUrl;
        private string _subscriptionKey;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUrl">Api endpoint</param>
        /// <param name="subscriptionKey">Plan subscription key</param>
        public ConnectionInformation(string baseUrl, string subscriptionKey)
        {
            BaseUrl = baseUrl;
            SubscriptionKey = subscriptionKey;
        }

        public string BaseUrl
        {
            get
            {
                return _baseUrl;
            }
            set
            {
                Utils.StringValidation(value);
                _baseUrl = value;
            }
        }

        public string SubscriptionKey
        {
            get
            {
                return _subscriptionKey;
            }
            set
            {
                Utils.StringValidation(value);
                _subscriptionKey = value;
            }
        }
    }
}
