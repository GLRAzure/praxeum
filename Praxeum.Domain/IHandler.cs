using System.Threading.Tasks;

namespace Praxeum.Domain
{
    public interface IHandler<TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
}