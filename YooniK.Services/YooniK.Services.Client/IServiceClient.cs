using System.Threading.Tasks;
using YooniK.Services.Client.Common;

namespace YooniK.Services.Client
{
    public interface IServiceClient
    {
        public Task<T> SendRequestAsync<T>(IRequestMessage Message);
        public Task<string> SendRequestAsync(IRequestMessage Message);
    }
}
