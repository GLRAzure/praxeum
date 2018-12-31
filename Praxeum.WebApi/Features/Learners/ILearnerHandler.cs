using System.Threading.Tasks;

namespace Praxeum.WebApi.Features.Learners
{
    public interface ILearnerHandler<TRequest,TResponse>
    {
        Task<TResponse> ExecuteAsync(
            TRequest request);
    }
}
