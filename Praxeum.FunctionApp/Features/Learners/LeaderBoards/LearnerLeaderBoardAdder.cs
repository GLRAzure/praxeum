using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Praxeum.Data;

namespace Praxeum.FunctionApp.Features.LeaderBoards.Learners
{
    public class LearnerLeaderBoardAdder : IHandler<LearnerLeaderBoardAdd, LearnerLeaderBoardAdded>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ILearnerLeaderBoardRepository _learnerLeaderBoardRepository;

        public LearnerLeaderBoardAdder(
            ILogger logger,
            IMapper mapper,
            ILearnerLeaderBoardRepository learnerLeaderBoardRepository)
        {
            _logger =
               logger;
            _mapper =
               mapper;
            _learnerLeaderBoardRepository =
                learnerLeaderBoardRepository;
        }

        public async Task<LearnerLeaderBoardAdded> ExecuteAsync(
            LearnerLeaderBoardAdd learnerLeaderBoardAdd)
        {
            await _learnerLeaderBoardRepository.AddAsync(
                learnerLeaderBoardAdd.LearnerId,
                learnerLeaderBoardAdd.LeaderBoardId);

            _logger.LogInformation("Leader board added to learner.");

            var learnerLeaderBoardAdded =
                _mapper.Map(learnerLeaderBoardAdd, new LearnerLeaderBoardAdded());

            return learnerLeaderBoardAdded;
        }
    }
}