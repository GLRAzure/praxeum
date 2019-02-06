using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Praxeum.Domain.Contests;
using Praxeum.Data.Helpers;
using Praxeum.Data;
using Microsoft.Extensions.Options;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.FunctionApp
{
    public static class ContestProgressUpdaterHttpTrigger
    {
        [FunctionName("ContestProgressUpdaterHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "contests/progress")] HttpRequest req,
            ILogger log,
            [Queue("contestlearnerprogress-update", Connection = "AzureStorageOptions:ConnectionString")] ICollector<ContestLearnerProgressUpdate> contestLearnerProgressUpdateCollector)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // NOTE: Doing it this way as there because there is no startup file support to just run the mapping configuration once
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

            var contestLister =
                new ContestLister(
                    mapper,
                    new ContestRepository(Options.Create(azureCosmosDbOptions)));

            var contestLearnerLister =
                new ContestLearnerLister(
                    mapper,
                    new ContestLearnerRepository(Options.Create(azureCosmosDbOptions)));

            var contests =
                await contestLister.ExecuteAsync(
                    new ContestList
                    {
                        Status = ContestStatus.InProgress
                    });

            foreach (var contest in contests)
            {
                var contestLearners =
                   await contestLearnerLister.ExecuteAsync(
                       new ContestLearnerList
                       {
                           ContestId = contest.Id
                       });

                foreach (var contestLearner in contestLearners)
                {
                    contestLearnerProgressUpdateCollector.Add(
                        new ContestLearnerProgressUpdate
                        {
                            Id = contestLearner.Id,
                            ContestId = contestLearner.ContestId
                        });
                }
            }

            return new OkResult();
        }
    }
}
