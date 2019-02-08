using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Praxeum.Data;
using Praxeum.Data.Helpers;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.Learners
{
    public static class ContestUpdatedQueueTrigger
    {
        [FunctionName("ContestUpdatedQueueTrigger")]
        public static async Task Run(
            [QueueTrigger("contest-updated", Connection = "AzureStorageOptions:ConnectionString")]ContestUpdated contestUpdated,
            ILogger log,
            [Queue("contestlearnerprogress-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestLearnerProgressUpdate> contestLearnerProgressUpdateCollector)
        {
            log.LogInformation($"C# Queue trigger function processed: {JsonConvert.SerializeObject(contestUpdated, Formatting.Indented)}");

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

            var contestLearnerLister =
                 new ContestLearnerLister(
                     mapper,
                     new ContestLearnerRepository(Options.Create(azureCosmosDbOptions)));

            var contestLearners =
                await contestLearnerLister.ExecuteAsync(
                    new ContestLearnerList
                    {
                        ContestId = contestUpdated.Id
                    });

            foreach (var contestLearner in contestLearners)
            {
                var contestLearnerProgressUpdate =
                    new ContestLearnerProgressUpdate
                    {
                        Id = contestLearner.Id,
                        ContestId = contestLearner.ContestId
                    };

                contestLearnerProgressUpdateCollector.Add(
                    contestLearnerProgressUpdate);

                log.LogInformation(
                    JsonConvert.SerializeObject(contestLearnerProgressUpdate, Formatting.Indented));
            }
        }
    }
}
