using System.Threading.Tasks;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public interface ILeaderBoardHandler<TRequest,TResponse>
    {
        Task<TResponse> ExecuteAsync(
            TRequest request);
    }
}
