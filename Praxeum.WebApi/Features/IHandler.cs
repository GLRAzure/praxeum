using System.Threading.Tasks;

namespace Praxeum.WebApi.Features
{
    public interface IHandler<TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
}