using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.Data.Helpers;
using Praxeum.Domain;
using Praxeum.Domain.Contests.Learners;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class ContestLearnerAddQueueTrigger
    {
        [FunctionName("ContestLearnerAddQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestlearner-add", Connection = "AzureStorageOptions:ConnectionString")] ContestLearnerAdd contestLearnerAdd,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(contestLearnerAdd, Formatting.Indented)}");

            var mapperConfiguration =
                new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<ContestLearnerProfile>();
                });

            var mapper =
                mapperConfiguration.CreateMapper();

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var azureQueueStorageEventPublisherOptions =
                new AzureQueueStorageEventPublisherOptions();

            var contestLearnerAdder =
                new ContestLearnerAdder(
                    mapper,
                    new AzureQueueStorageEventPublisher(Options.Create(azureQueueStorageEventPublisherOptions)),
                    new ContestRepository(Options.Create(azureCosmosDbOptions)),
                    new ContestLearnerRepository(Options.Create(azureCosmosDbOptions)),
                    new MicrosoftProfileRepository(),
                    new ContestLearnerStartValueUpdater(
                        new ExperiencePointsCalculator()),
                    new ContestLearnerTargetValueUpdater());

            var contestLearnerAdded =
                await contestLearnerAdder.ExecuteAsync(
                    contestLearnerAdd);

            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerAdded, Formatting.Indented));
        }
    }
}
