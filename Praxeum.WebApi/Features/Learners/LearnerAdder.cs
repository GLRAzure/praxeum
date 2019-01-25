using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.WebApi.Data;
using Praxeum.WebApi.Features.LeaderBoards.Learners;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerAdder : IHandler<LearnerAdd, LearnerAdded>
    {
        private readonly IHandler<LeaderBoardLearnerAdd, LeaderBoardLearnerAdded> _leaderBoardLearnerAdder;
        private readonly IMicrosoftProfileFetcher _microsoftProfileFetcher;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerAdder(
            IHandler<LeaderBoardLearnerAdd, LeaderBoardLearnerAdded> leaderBoardLearnerAdder,
            IMicrosoftProfileFetcher microsoftProfileFetcher,
            ILearnerRepository learnerRepository)
        {
            _leaderBoardLearnerAdder =
                leaderBoardLearnerAdder;
            _microsoftProfileFetcher =
                microsoftProfileFetcher;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerAdded> ExecuteAsync(
            LearnerAdd learnerAdd)
        {
            var learner =
                await _learnerRepository.FetchByUserNameAsync(learnerAdd.Name);

            if (learner == null)
            {
                learner = new Learner
                {
                    Id = learnerAdd.Id
                };
            }

            var microsoftProfile =
                await _microsoftProfileFetcher.FetchProfileAsync(learnerAdd.Name);

            learner =
                Mapper.Map(microsoftProfile, learner);

            learner.LastModifiedOn =
                learner.CreatedOn;

            learner =
                await _learnerRepository.AddOrUpdateAsync(
                    learner);

            var learnerAdded =
                Mapper.Map(learner, new LearnerAdded());

            if (learnerAdd.LeaderBoardId.HasValue)
            {
                await _leaderBoardLearnerAdder.ExecuteAsync(
                    new LeaderBoardLearnerAdd 
                    {
                        LeaderBoardId = learnerAdd.LeaderBoardId.Value,
                        LearnerId = learnerAdd.Id
                    });
            }

            learnerAdded.IsCached = false;

            return learnerAdded;
        }
    }
}