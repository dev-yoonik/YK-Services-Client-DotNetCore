using System.Threading.Tasks;
using YooniK.Services.Client.Common;

namespace YooniK.Services.Client
{
    public interface IServiceClient
    {
        public Task<T> RequestAsync<T>(IRequestMessage Message);
        public Task<string> RequestAsync(IRequestMessage Message);
    }
}
