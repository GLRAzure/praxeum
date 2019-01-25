using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardUpdater : IHandler<LeaderBoardUpdate, LeaderBoardUpdated>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardUpdater(
            ILeaderBoardRepository leaderBoardRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
        }

        public async Task<LeaderBoardUpdated> ExecuteAsync(
            LeaderBoardUpdate leaderBoardUpdate)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardUpdate.Id);

            Mapper.Map(leaderBoardUpdate, leaderBoard);

            leaderBoard =
                await _leaderBoardRepository.UpdateByIdAsync(
                    leaderBoardUpdate.Id,
                    leaderBoard);

            var leaderBoardUpdated =
                Mapper.Map(leaderBoard, new LeaderBoardUpdated());

            return leaderBoardUpdated;
        }
    }
}