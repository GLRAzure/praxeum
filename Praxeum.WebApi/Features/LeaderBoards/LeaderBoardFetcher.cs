using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardFetcher : IHandler<LeaderBoardFetch, LeaderBoardFetched>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardFetcher(
            ILeaderBoardRepository leaderBoardRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
        }

        public async Task<LeaderBoardFetched> ExecuteAsync(
            LeaderBoardFetch leaderBoardFetch)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardFetch.Id);

            var leaderBoardFetched =
                Mapper.Map(leaderBoard, new LeaderBoardFetched());

            return leaderBoardFetched;
        }
    }
}