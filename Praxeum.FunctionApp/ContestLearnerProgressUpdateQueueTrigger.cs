using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.Data.Helpers;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.FunctionApp
{
    public static class ContestLearnerProgressUpdateQueueTrigger
    {
        [FunctionName("ContestLearnerProgressUpdateQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestlearnerprogress-update", Connection = "AzureStorageOptions:ConnectionString")]ContestLearnerProgressUpdate contestLearnerProgressUpdate,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(contestLearnerProgressUpdate, Formatting.Indented)}");

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

            var contestLearnerProgressUpdater =
                new ContestLearnerProgressUpdater(
                    mapper,
                    new ContestRepository(Options.Create(azureCosmosDbOptions)),
                    new ContestLearnerRepository(Options.Create(azureCosmosDbOptions)),
                    new MicrosoftProfileRepository());

            var contestLearnerProgressUpdated =
                await contestLearnerProgressUpdater.ExecuteAsync(
                    contestLearnerProgressUpdate);

            log.LogInformation(
                JsonConvert.SerializeObject(contestLearnerProgressUpdated, Formatting.Indented));
        }
    }
}
