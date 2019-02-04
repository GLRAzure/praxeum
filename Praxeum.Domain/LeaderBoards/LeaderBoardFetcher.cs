using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;
using Praxeum.Domain.Learners;

namespace Praxeum.Domain.LeaderBoards
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
                await _learnerRepository.FetchListByLeaderBoardIdAsync(
                    leaderBoardFetch.Id);

            foreach (var learner in learners)
            {
                leaderBoardFetched.Learners.Add(
                    Mapper.Map(learner, new LearnerFetched()));
            }

            return leaderBoardFetched;
        }
    }
}