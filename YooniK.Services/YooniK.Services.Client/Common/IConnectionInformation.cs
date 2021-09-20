namespace YooniK.Services.Client.Common
{
    public interface IConnectionInformation
    {
        public string BaseUrl { get; set; }
        public string SubscriptionKey { get; set; }
    }
}