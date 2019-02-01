using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.LeaderBoards
{
    public class LeaderBoardAdder : IHandler<LeaderBoardAdd, LeaderBoardAdded>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardAdder(
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