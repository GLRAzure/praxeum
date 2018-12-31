using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardDeleteByIdHandler : ILeaderBoardHandler<LeaderBoardDeleteById, LeaderBoardDeletedById>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardDeleteByIdHandler(
            ILeaderBoardRepository leaderBoardRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
        }

        public async Task<LeaderBoardDeletedById> ExecuteAsync(
            LeaderBoardDeleteById leaderBoardDeleteById)
        {
            var leaderBoard =
                await _leaderBoardRepository.DeleteByIdAsync(
                    leaderBoardDeleteById.Id);

            var leaderBoardDeletedById =
                Mapper.Map(leaderBoard, new LeaderBoardDeletedById());

            return leaderBoardDeletedById;
        }
    }
}