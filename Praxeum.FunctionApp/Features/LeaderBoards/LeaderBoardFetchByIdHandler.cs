using System.Threading.Tasks;
using AutoMapper;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardFetchByIdHandler : IRequestHandler<LeaderBoardFetchById, LeaderBoardFetchedById>
    {
        private readonly ILeaderBoardRepository _opponentRepository;

        public LeaderBoardFetchByIdHandler(
            ILeaderBoardRepository opponentRepository)
        {
            _opponentRepository =
                opponentRepository;
        }

        public async Task<LeaderBoardFetchedById> ExecuteAsync(
            LeaderBoardFetchById opponentFetchById)
        {
            var opponent =
                await _opponentRepository.FetchByIdAsync(
                    opponentFetchById.Id);

            var opponentFetchedById =
                Mapper.Map(opponent, new LeaderBoardFetchedById());

            return opponentFetchedById;
        }
    }
}