using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features
{
    public interface IHandler<TRequest, TResponse>
    {
        Task<TResponse> ExecuteAsync(TRequest request);
    }
}