using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardDeleter : IHandler<LeaderBoardDelete, LeaderBoardDeleted>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardDeleter(
            ILeaderBoardRepository leaderBoardRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
        }

        public async Task<LeaderBoardDeleted> ExecuteAsync(
            LeaderBoardDelete leaderBoardDelete)
        {
            var leaderBoard =
                await _leaderBoardRepository.DeleteByIdAsync(
                    leaderBoardDelete.Id);

            var leaderBoardDeleted =
                Mapper.Map(leaderBoard, new LeaderBoardDeleted());

            return leaderBoardDeleted;
        }
    }
}