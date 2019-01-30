using System.Linq;
using System.Threading.Tasks;
using Praxeum.Data;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.LeaderBoards.Learners
{
    public class LeaderBoardLearnerCountUpdater : IHandler<LeaderBoardLearnerCountUpdate, LeaderBoardLearnerCountUpdated>
    {
        private readonly ILearnerRepository _learnerRepository;
        private readonly ILeaderBoardRepository _leaderBoardRepository;

        public LeaderBoardLearnerCountUpdater(
            ILearnerRepository learnerRepository,
            ILeaderBoardRepository leaderBoardRepository)
        {
            _learnerRepository =
                learnerRepository;
            _leaderBoardRepository =
                leaderBoardRepository;
        }

        public async Task<LeaderBoardLearnerCountUpdated> ExecuteAsync(
            LeaderBoardLearnerCountUpdate leaderBoardLearnerCountUpdate)
        {
            var leaderBoardLearnerCountUpdated =
                new LeaderBoardLearnerCountUpdated();

            var leaderBoards =
                await _leaderBoardRepository.FetchListAsync();

            foreach (var leaderBoard in leaderBoards)
            {
                var learners =
                   await _learnerRepository.FetchListByLeaderBoardIdAsync(
                       leaderBoard.Id);

                var numberOfLearners = 
                    learners.Count();

                if (leaderBoard.NumberOfLearners != numberOfLearners)
                {
                    leaderBoard.NumberOfLearners =
                        numberOfLearners;

                    await _leaderBoardRepository.UpdateByIdAsync(
                        leaderBoard.Id,
                        leaderBoard);

                    leaderBoardLearnerCountUpdated.NumberOfLeaderBoardsUpdated++;
                }
            }

            return leaderBoardLearnerCountUpdated;
        }
    }
}