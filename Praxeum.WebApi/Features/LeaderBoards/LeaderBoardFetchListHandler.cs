using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardFetchListHandler : ILeaderBoardHandler<LeaderBoardFetchList, IEnumerable<LeaderBoardFetchedList>>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardFetchListHandler(
            ILeaderBoardRepository leaderBoardFetchListRepository)
        {
            _leaderBoardRepository =
                leaderBoardFetchListRepository;
        }

        public async Task<IEnumerable<LeaderBoardFetchedList>> ExecuteAsync(
            LeaderBoardFetchList leaderBoardFetchList)
        {
            var leaderBoardList =
                await _leaderBoardRepository.FetchListAsync();

            var leaderBoardFetchedList =
                leaderBoardList.Select(x => Mapper.Map(x, new LeaderBoardFetchedList()));

            return leaderBoardFetchedList;
        }
    }
}