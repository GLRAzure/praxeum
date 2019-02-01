using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.LeaderBoards
{
    public class LeaderBoardDeleter : IHandler<LeaderBoardDelete, LeaderBoardDeleted>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly ILearnerRepository _learnerRepository;
        private readonly ILearnerLeaderBoardRepository _learnerLeaderBoardRepository;

        public LeaderBoardDeleter(
            ILeaderBoardRepository leaderBoardRepository,
            ILearnerRepository learnerRepository,
            ILearnerLeaderBoardRepository learnerLeaderBoardRepository)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
            _learnerRepository =
                learnerRepository;
            _learnerLeaderBoardRepository =
                learnerLeaderBoardRepository;
        }

        public async Task<LeaderBoardDeleted> ExecuteAsync(
            LeaderBoardDelete leaderBoardDelete)
        {
            var leaderBoard =
                await _leaderBoardRepository.DeleteByIdAsync(
                    leaderBoardDelete.Id);

            var leaderBoardDeleted =
                Mapper.Map(leaderBoard, new LeaderBoardDeleted());

            var learners =
                await _learnerRepository.FetchListByLeaderBoardIdAsync(
                    leaderBoardDelete.Id);

            foreach(var learner in learners)
            {
                await _learnerLeaderBoardRepository.DeleteAsync(
                    learner.Id,
                    leaderBoardDelete.Id);
            }

            return leaderBoardDeleted;
        }
    }
}