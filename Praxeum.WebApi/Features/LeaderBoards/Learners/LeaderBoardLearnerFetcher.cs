using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.LeaderBoards.Learners
{
    public class LeaderBoardLearnerFetcher : IHandler<LeaderBoardLearnerFetch, LeaderBoardLearnerFetched>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LeaderBoardLearnerFetcher(
            ILeaderBoardRepository leaderBoardRepository,
            ILearnerRepository learnerRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LeaderBoardLearnerFetched> ExecuteAsync(
            LeaderBoardLearnerFetch leaderBoardLearnerFetch)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardLearnerFetch.LeaderBoardId);

            if (leaderBoard == null)
            {
                throw new NullReferenceException($"Leader Board {leaderBoardLearnerFetch.LeaderBoardId} does not exist.");
            }

            var leaderBoardLearner =
                leaderBoard.Learners.SingleOrDefault(
                    x => x.LearnerId == leaderBoardLearnerFetch.LearnerId);

            if (leaderBoardLearner == null)
            {
                throw new NullReferenceException($"Learner {leaderBoardLearnerFetch.LearnerId} does not exist.");
            }

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    leaderBoardLearnerFetch.LearnerId);

            var leaderBoardLearnerFetched =
                Mapper.Map(learner, new LeaderBoardLearnerFetched());

            return leaderBoardLearnerFetched;
        }
    }
}