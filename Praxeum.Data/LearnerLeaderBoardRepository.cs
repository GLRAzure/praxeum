using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data.Helpers;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace Praxeum.Data
{
    public class LearnerLeaderBoardRepository : AzureCosmosDbRepository, ILearnerLeaderBoardRepository
    {
        public LearnerLeaderBoardRepository(
            IOptions<AzureCosmosDbOptions> azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<LearnerLeaderBoard> AddAsync(
       LearnerLeaderBoard leaderBoard)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["leaderboards"];

            var leaderBoardDocument =
                await learnerContainer.Items.CreateItemAsync<LearnerLeaderBoard>(
                    leaderBoard.Id.ToString(),
                    leaderBoard);

            return leaderBoardDocument.Resource;
        }

        public async Task<LearnerLeaderBoard> AddAsync(
            Guid learnerId,
            Guid leaderBoardId)
        {
            var leaderBoardContainer =
               _cosmosDatabase.Containers["leaderboards"];

            var leaderBoardDocument =
                await leaderBoardContainer.Items.ReadItemAsync<LeaderBoard>(
                    leaderBoardId.ToString(),
                    leaderBoardId.ToString());

            if (leaderBoardDocument == null)
            {
                throw new NullReferenceException("Leader Board does not exist.");
            }

            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.ReadItemAsync<Learner>(
                    learnerId.ToString(),
                    learnerId.ToString());

            if (learnerDocument == null)
            {
                throw new NullReferenceException("Learner does not exist.");
            }

            var learner =
                learnerDocument.Resource;

            var learnerLeaderBoard =
               new LearnerLeaderBoard(
                   leaderBoardDocument.Resource);

            if (learner.LeaderBoards.All(x => x.Id != leaderBoardId))
            {

                learner.LeaderBoards.Add(
                    learnerLeaderBoard);

                await learnerContainer.Items.ReplaceItemAsync(
                    learnerId.ToString(),
                    learnerId.ToString(),
                    learner);
            }

            return learnerLeaderBoard;
        }

        public async Task<LearnerLeaderBoard> DeleteAsync(
            Guid learnerId,
            Guid leaderBoardId)
        {
            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.ReadItemAsync<Learner>(
                    learnerId.ToString(),
                    learnerId.ToString());

            if (learnerDocument == null)
            {
                throw new NullReferenceException("Learner does not exist.");
            }

            var learner =
                learnerDocument.Resource;

            var learnerLeaderBoard =
                learner.LeaderBoards.SingleOrDefault(x => x.Id == leaderBoardId);

            if (learnerLeaderBoard != null)
            {
                learner.LeaderBoards.Remove(
                    learner.LeaderBoards.Single(x => x.Id == leaderBoardId));

                await learnerContainer.Items.ReplaceItemAsync(
                    learnerId.ToString(),
                    learnerId.ToString(),
                    learner);
            }

            return learnerLeaderBoard;
        }

        public async Task<LearnerLeaderBoard> UpdateAsync(
            Guid learnerId,
            Guid leaderBoardId)
        {
            var leaderBoardContainer =
               _cosmosDatabase.Containers["leaderboards"];

            var leaderBoardDocument =
                await leaderBoardContainer.Items.ReadItemAsync<LeaderBoard>(
                    leaderBoardId.ToString(),
                    leaderBoardId.ToString());

            if (leaderBoardDocument == null)
            {
                throw new NullReferenceException("Leader Board does not exist.");
            }

            var learnerContainer =
               _cosmosDatabase.Containers["learners"];

            var learnerDocument =
                await learnerContainer.Items.ReadItemAsync<Learner>(
                    learnerId.ToString(),
                    learnerId.ToString());

            if (learnerDocument == null)
            {
                throw new NullReferenceException("Learner does not exist.");
            }

            var learner =
                learnerDocument.Resource;

            var learnerLeaderBoard =
                learner.LeaderBoards.SingleOrDefault(x => x.Id == leaderBoardId);

            if (learnerLeaderBoard == null)
            {
                throw new NullReferenceException("Leader Board does not exist for the learner.");
            }
            else
            {
                learnerLeaderBoard.Name =
                    leaderBoardDocument.Resource.Name;

                await learnerContainer.Items.ReplaceItemAsync(
                    learnerId.ToString(),
                    learnerId.ToString(),
                    learner);
            }

            return learnerLeaderBoard;
        }
    }
}
