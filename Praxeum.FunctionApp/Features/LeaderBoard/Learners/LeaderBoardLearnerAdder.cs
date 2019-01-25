using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Praxeum.Data;

namespace Praxeum.FunctionApp.Features.LeaderBoards.Learners
{
    public class LeaderBoardLearnerAdder : IHandler<LeaderBoardLearnerAdd, LeaderBoardLearnerAdded>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LeaderBoardLearnerAdder(
            ILogger logger,
            IMapper mapper,
            ILeaderBoardRepository leaderBoardRepository,
            ILearnerRepository learnerRepository)
        {
            _logger =
               logger;
            _mapper =
               mapper;
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

            if (leaderBoardLearner == null)
            {
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
            }

            var learner =
                await _learnerRepository.FetchByIdAsync(
                    leaderBoardLearnerAdd.LearnerId);

            var leaderBoardLearnerAdded =
                _mapper.Map(learner, new LeaderBoardLearnerAdded());

            return leaderBoardLearnerAdded;
        }
    }
}