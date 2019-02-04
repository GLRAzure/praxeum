using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Learners.LeaderBoards
{
    public class LearnerLeaderBoardDeleter : IHandler<LearnerLeaderBoardDelete, LearnerLeaderBoardDeleted>
    {
        private readonly ILearnerLeaderBoardRepository _learnerLeaderBoardRepository;

        public LearnerLeaderBoardDeleter(
            ILearnerLeaderBoardRepository learnerLeaderBoardRepository)
        {
            _learnerLeaderBoardRepository =
                learnerLeaderBoardRepository;
        }

        public async Task<LearnerLeaderBoardDeleted> ExecuteAsync(
            LearnerLeaderBoardDelete learnerLeaderBoardDelete)
        {
            var learnerLeaderBoard =
                await _learnerLeaderBoardRepository.DeleteAsync(
                    learnerLeaderBoardDelete.LearnerId,
                    learnerLeaderBoardDelete.LeaderBoardId);

            var learnerLeaderBoardDeleted =
                Mapper.Map(learnerLeaderBoard, new LearnerLeaderBoardDeleted());

            return learnerLeaderBoardDeleted;

        }
    }
}