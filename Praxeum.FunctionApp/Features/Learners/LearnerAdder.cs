﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Praxeum.Data;
using Praxeum.FunctionApp.Features.LeaderBoards.Learners;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.Learners
{
    public class LearnerAdder : IHandler<LearnerAdd, LearnerAdded>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMicrosoftProfileRepository _microsoftProfileRepository;
        private readonly ILearnerRepository _learnerRepository;

        public LearnerAdder(
            ILogger logger,
            IMapper mapper,
            IMicrosoftProfileRepository microsoftProfileRepository,
            ILearnerRepository learnerRepository)
        {
            _logger =
                logger;
            _mapper =
               mapper;
            _microsoftProfileRepository =
                microsoftProfileRepository;
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerAdded> ExecuteAsync(
            LearnerAdd learnerAdd)
        {
            learnerAdd.Name = 
                learnerAdd.Name.Trim().ToLower();

            var learner =
                await _learnerRepository.FetchByUserNameAsync(
                    learnerAdd.Name);

            if (learner == null)
            {
                learner = new Learner
                {
                    Id = learnerAdd.Id,
                    UserName = learnerAdd.Name,
                    Status = LearnerStatus.Pending
                };

                _logger.LogInformation(
                    $"Adding '{learner.UserName}'...");
            }
            else
            {
                learner.Status =
                    LearnerStatus.Importing;

                _logger.LogInformation(
                    $"Importing '{learner.UserName}'...");
            }

            learner =
                await _learnerRepository.AddOrUpdateAsync(
                    learner);

            try
            {
                var microsoftProfile =
                    await _microsoftProfileRepository.FetchProfileAsync(
                        learnerAdd.Name);

                learner =
                    _mapper.Map(microsoftProfile, learner);

                learner.Status =
                    LearnerStatus.Imported;

                _logger.LogInformation(
                    $"Imported!");
            }
            catch (Exception ex)
            {
                learner.Status = 
                    LearnerStatus.Failed;
                learner.StatusMessage = 
                    ex.Message;
 
                _logger.LogInformation(
                    $"Failed to import '{learner.UserName}': {ex.Message}.");
           }

            learner.LastModifiedOn =
                learner.CreatedOn;

            learner =
                await _learnerRepository.AddOrUpdateAsync(
                    learner);

            var learnerAdded =
                _mapper.Map(
                    learner, 
                    new LearnerAdded());

            return learnerAdded;
        }
    }
}