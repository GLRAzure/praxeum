using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.Data.Helpers;
using Praxeum.Domain.Contests.Learners;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class ContestLearnerAdderQueueTrigger
    {
        [FunctionName("ContestLearnerAdderQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestlearner-add", Connection = "AzureStorageOptions:ConnectionString")]ContestLearnerAdd contestLearnerAdd,
            ILogger log,
            [Queue("contestlearner-added", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestLearnerAdded> contestLearnerAddedCollector)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(contestLearnerAdd, Formatting.Indented)}");

            // NOTE: Doing it this way as there because there is no startup file support to just run the mapping configuration once
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

            var contestLearnerAdder =
                new ContestLearnerAdder(
                    mapper,
                    new ContestRepository(Options.Create(azureCosmosDbOptions)),
                    new ContestLearnerRepository(Options.Create(azureCosmosDbOptions)),
                    new MicrosoftProfileRepository());

            var contestLearnerAdded =
                await contestLearnerAdder.ExecuteAsync(
                    contestLearnerAdd);

            contestLearnerAddedCollector.Add(
                contestLearnerAdded);

            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerAdded, Formatting.Indented));
        }
    }
}
