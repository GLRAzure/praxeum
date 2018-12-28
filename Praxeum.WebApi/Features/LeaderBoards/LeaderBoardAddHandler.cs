using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardAddHandler : ILeaderBoardHandler<LeaderBoardAdd, LeaderBoardAdded>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardAddHandler(
            ILeaderBoardRepository leaderBoardRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
        }

        public async Task<LeaderBoardAdded> ExecuteAsync(
            LeaderBoardAdd leaderBoardAdd)
        {
            var leaderBoard =
                Mapper.Map(leaderBoardAdd, new LeaderBoard());

            leaderBoard = 
                await _leaderBoardRepository.AddAsync(
                    leaderBoard);

            var leaderBoardAdded =
                Mapper.Map(leaderBoard, new LeaderBoardAdded());

            return leaderBoardAdded;
        }
    }
}