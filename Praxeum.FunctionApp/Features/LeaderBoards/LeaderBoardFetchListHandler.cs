using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardFetchListHandler
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardFetchListHandler()
        {
            _leaderBoardRepository =
                new LeaderBoardRepository();
        }

        public LeaderBoardFetchListHandler(
            ILeaderBoardRepository leaderBoardRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
        }

        public async Task<IEnumerable<LeaderBoardFetchedList>> ExecuteAsync(
            LeaderBoardFetchList leaderBoardFetchList)
        {
            var leaderBoardList =
                await _leaderBoardRepository.FetchListAsync();

            var leaderBoardFetchedList =
                leaderBoardList.Select(x => new LeaderBoardFetchedList(x));

            return leaderBoardFetchedList;
        }
    }
}