using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardLister : IHandler<LeaderBoardList, IEnumerable<LeaderBoardListed>>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardLister(
            ILeaderBoardRepository leaderBoardFetchListRepository)
        {
            _leaderBoardRepository =
                leaderBoardFetchListRepository;
        }

        public async Task<IEnumerable<LeaderBoardListed>> ExecuteAsync(
            LeaderBoardList leaderBoardFetchList)
        {
            var leaderBoardList =
                await _leaderBoardRepository.FetchListAsync();

            var leaderBoardFetchedList =
                leaderBoardList.Select(x => Mapper.Map(x, new LeaderBoardListed()));

            return leaderBoardFetchedList;
        }
    }
}