using System.Threading.Tasks;
using AutoMapper;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardUpdateByIdHandler : ILeaderBoardHandler<LeaderBoardUpdateById, LeaderBoardUpdatedById>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardUpdateByIdHandler(
            ILeaderBoardRepository leaderBoardRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
        }

        public async Task<LeaderBoardUpdatedById> ExecuteAsync(
            LeaderBoardUpdateById leaderBoardUpdateById)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardUpdateById.Id);

            Mapper.Map(leaderBoardUpdateById, leaderBoard);

            leaderBoard = await _leaderBoardRepository.UpdateByIdAsync(
                leaderBoardUpdateById.Id, 
                leaderBoard);

            var leaderBoardUpdatedById =
                Mapper.Map(leaderBoard, new LeaderBoardUpdatedById());

            return leaderBoardUpdatedById;
        }
    }
}