
namespace YooniK.Services.Client.Common
{
    // Each different API Request Object must implement the IRequest and guarantee the parse to JSON format of its data.
    public interface IRequest
    {
        public string ToJson();
    }
}
