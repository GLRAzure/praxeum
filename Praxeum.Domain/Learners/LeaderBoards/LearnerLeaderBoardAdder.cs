using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Learners.LeaderBoards
{
    public class LearnerLeaderBoardAdder : IHandler<LearnerLeaderBoardAdd, LearnerLeaderBoardAdded>
    {
        private readonly ILearnerLeaderBoardRepository _learnerLeaderBoardRepository;

        public LearnerLeaderBoardAdder(
            ILearnerLeaderBoardRepository learnerLeaderBoardRepository)
        {
            _learnerLeaderBoardRepository =
                learnerLeaderBoardRepository;
        }

        public async Task<LearnerLeaderBoardAdded> ExecuteAsync(
            LearnerLeaderBoardAdd learnerLeaderBoardAdd)
        {
            var learnerLeaderBoard =
                await _learnerLeaderBoardRepository.AddAsync(
                    learnerLeaderBoardAdd.LearnerId,
                    learnerLeaderBoardAdd.LeaderBoardId);

            var learnerLeaderBoardAdded =
                Mapper.Map(learnerLeaderBoard, new LearnerLeaderBoardAdded());

            return learnerLeaderBoardAdded;
        }
    }
}