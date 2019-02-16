using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.Data.Helpers;
using Praxeum.Domain;
using Praxeum.Domain.Contests;

namespace Praxeum.FunctionApp
{
    public static class ContestProgressUpdateQueueTrigger
    {
        [FunctionName("ContestProgressUpdateQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contestprogress-update", Connection = "AzureStorageOptions:ConnectionString")] ContestProgressUpdate contestProgressUpdate,
            ILogger log)
        {
            log.LogInformation(
                JsonConvert.SerializeObject(contestProgressUpdate, Formatting.Indented));

            var mapperConfiguration =
                new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                    cfg.AddProfile<ContestProfile>();
                });

            var mapper =
                mapperConfiguration.CreateMapper();

            var azureCosmosDbOptions =
                new AzureCosmosDbOptions();

            var azureQueueStorageEventPublisherOptions =
                new AzureQueueStorageEventPublisherOptions();

            var contestProgressUpdater =
                new ContestProgressUpdater(
                    mapper,
                    new AzureQueueStorageEventPublisher(Options.Create(azureQueueStorageEventPublisherOptions)),
                    new ContestRepository(Options.Create(azureCosmosDbOptions)),
                    new ContestLearnerRepository(Options.Create(azureCosmosDbOptions)));

            var contestProgressUpdated =
                await contestProgressUpdater.ExecuteAsync(
                    contestProgressUpdate);

            log.LogInformation(
                JsonConvert.SerializeObject(contestProgressUpdated, Formatting.Indented));
        }
    }
}
