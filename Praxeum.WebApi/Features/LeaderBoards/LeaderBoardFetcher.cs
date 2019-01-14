using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardFetcher : IHandler<LeaderBoardFetch, LeaderBoardFetched>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LeaderBoardFetcher(
            ILeaderBoardRepository leaderBoardRepository,
            ILearnerRepository learnerRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LeaderBoardFetched> ExecuteAsync(
            LeaderBoardFetch leaderBoardFetch)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardFetch.Id);

            var leaderBoardFetched =
                Mapper.Map(leaderBoard, new LeaderBoardFetched());

            var learners =
                await _learnerRepository.FetchListAsync(
                    leaderBoard.Learners.Select(x => x.LearnerId).ToArray());

            foreach (var learner in learners)
            {
                leaderBoardFetched.Learners.Add(
                    Mapper.Map(learner, new Learner()));
            }

            return leaderBoardFetched;
        }
    }
}