using System.Threading.Tasks;
using Praxeum.FunctionApp.Data;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardAddHandler
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardAddHandler()
        {
            _leaderBoardRepository =
                new LeaderBoardRepository();
        }

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
                new LeaderBoard
                {
                    Name = leaderBoardAdd.Name,
                    Description = leaderBoardAdd.Description
                };

            leaderBoard =
                await _leaderBoardRepository.AddAsync(
                    leaderBoard);

            var leaderBoardAdded =
                new LeaderBoardAdded(leaderBoard);

            return leaderBoardAdded;
        }
    }
}