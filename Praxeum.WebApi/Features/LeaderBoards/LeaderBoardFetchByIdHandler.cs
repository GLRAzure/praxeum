using System.Threading.Tasks;
using AutoMapper;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardFetchByIdHandler : ILeaderBoardHandler<LeaderBoardFetchById, LeaderBoardFetchedById>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardFetchByIdHandler(
            ILeaderBoardRepository leaderBoardRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
        }

        public async Task<LeaderBoardFetchedById> ExecuteAsync(
            LeaderBoardFetchById leaderBoardFetchById)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardFetchById.Id);

            var leaderBoardFetchedById =
                Mapper.Map(leaderBoard, new LeaderBoardFetchedById());

            return leaderBoardFetchedById;
        }
    }
}