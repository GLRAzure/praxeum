using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.LeaderBoards.Learners
{
    public class LeaderBoardLearnerAdder : IHandler<LeaderBoardLearnerAdd, LeaderBoardLearnerAdded>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LeaderBoardLearnerAdder(
            ILeaderBoardRepository leaderBoardRepository,
            ILearnerRepository learnerRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LeaderBoardLearnerAdded> ExecuteAsync(
            LeaderBoardLearnerAdd leaderBoardLearnerAdd)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardLearnerAdd.LeaderBoardId);

            if (leaderBoard == null)
            {
                throw new NullReferenceException($"Leader Board {leaderBoardLearnerAdd.LeaderBoardId} does not exist.");
            }

            var leaderBoardLearner =
                leaderBoard.Learners.SingleOrDefault(x => x.LearnerId == leaderBoardLearnerAdd.LearnerId);

            if (leaderBoardLearner != null)
            {
                throw new ArgumentOutOfRangeException($"Learner {leaderBoardLearnerAdd.LearnerId} already exists.");
            }

            leaderBoard.Learners.Add(
                new LeaderBoardLearner
                {
                    Id = Guid.NewGuid(),
                    LearnerId = leaderBoardLearnerAdd.LearnerId
                });

            leaderBoard =
                await _leaderBoardRepository.UpdateByIdAsync(
                    leaderBoardLearnerAdd.LeaderBoardId,
                    leaderBoard);

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    leaderBoardLearnerAdd.LearnerId);

            var leaderBoardLearnerAdded =
                Mapper.Map(learner, new LeaderBoardLearnerAdded());

            return leaderBoardLearnerAdded;
        }
    }
}