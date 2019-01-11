using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.LeaderBoards.Learners
{
    public class LeaderBoardLearnerDeleter : IHandler<LeaderBoardLearnerDelete, LeaderBoardLearnerDeleted>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LeaderBoardLearnerDeleter(
            ILeaderBoardRepository leaderBoardRepository,
            ILearnerRepository learnerRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LeaderBoardLearnerDeleted> ExecuteAsync(
            LeaderBoardLearnerDelete leaderBoardLearnerDelete)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardLearnerDelete.LeaderBoardId);

            if (leaderBoard == null)
            {
                throw new NullReferenceException($"Leader Board {leaderBoardLearnerDelete.LeaderBoardId} does not exist.");
            }

            var leaderBoardLearner =
                leaderBoard.Learners.SingleOrDefault(x => x.LearnerId == leaderBoardLearnerDelete.LearnerId);

            if (leaderBoardLearner == null)
            {
                throw new ArgumentOutOfRangeException($"Learner {leaderBoardLearnerDelete.LearnerId} does not exist.");
            }

            leaderBoard.Learners.Remove(
                leaderBoardLearner);

            leaderBoard =
                await _leaderBoardRepository.UpdateByIdAsync(
                    leaderBoardLearnerDelete.LeaderBoardId,
                    leaderBoard);

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    leaderBoardLearnerDelete.LearnerId);

            var leaderBoardLearnerDeleted =
                 new LeaderBoardLearnerDeleted();

            return leaderBoardLearnerDeleted;
        }
    }
}