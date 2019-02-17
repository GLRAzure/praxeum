using AutoMapper;
using Microsoft.Extensions.Options;
using Praxeum.Data;
using Praxeum.Data.Helpers;
using Praxeum.Domain;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;
using Praxeum.Domain.Users;

namespace Praxeum.FunctionApp.Helpers
{
    public class ObjectFactory
    {
        private static IOptions<AzureCosmosDbOptions> _azureCosmosDbOptions;

        public static IOptions<AzureCosmosDbOptions> CreateAzureCosmosDbOptions()
        {
            if (_azureCosmosDbOptions == null)
            {
                _azureCosmosDbOptions =
                    Options.Create(new AzureCosmosDbOptions());
            }

            return _azureCosmosDbOptions;
        }

        private static IOptions<AzureQueueStorageEventPublisherOptions> _azureQueueStorageEventPublisherOptions;

        public static IOptions<AzureQueueStorageEventPublisherOptions> CreateAzureQueueStorageEventPublisherOptions()
        {
            if (_azureQueueStorageEventPublisherOptions == null)
            {
                _azureQueueStorageEventPublisherOptions =
                    Options.Create(new AzureQueueStorageEventPublisherOptions());
            }

            return _azureQueueStorageEventPublisherOptions;
        }

        public static ContestRepository CreateContestRepository()
        {
            return new ContestRepository(
                ObjectFactory.CreateAzureCosmosDbOptions());
        }

        public static ContestLearnerRepository CreateContestLearnerRepository()
        {
            return new ContestLearnerRepository(
                ObjectFactory.CreateAzureCosmosDbOptions());
        }
       
        public static AzureQueueStorageEventPublisher CreateAzureQueueStorageEventPublisher()
        {
            return new AzureQueueStorageEventPublisher(
                ObjectFactory.CreateAzureQueueStorageEventPublisherOptions());
        }

        public static MicrosoftProfileRepository CreateMicrosoftProfileRepository()
        {
            return new MicrosoftProfileRepository();
        }

        public static ContestLearnerCurrentValueUpdater CreateContestLearnerCurrentValueUpdater()
        {
            return new ContestLearnerCurrentValueUpdater(
                new ExperiencePointsCalculator());
        }

        public static ContestLearnerTargetValueUpdater CreateContestLearnerTargetValueUpdater()
        {
            return new ContestLearnerTargetValueUpdater();
        }

        public static ContestLearnerStartValueUpdater CreateContestLearnerStartValueUpdater()
        {
            return new ContestLearnerStartValueUpdater(
                new ExperiencePointsCalculator());
        }

      
        public static IMapper CreateMapper()
        {
            var mapperConfiguration =
                new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<ContestProfile>();
                    cfg.AddProfile<ContestLearnerProfile>();
                    cfg.AddProfile<UserProfile>();
                });

            var mapper =
                mapperConfiguration.CreateMapper();

            return mapper;
        }
    } 
}
