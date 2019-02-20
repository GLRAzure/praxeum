using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Praxeum.ConsoleApp.DatabaseMigrator.Data;
using Praxeum.Data;
using Praxeum.Domain;

namespace Praxeum.ConsoleApp.DatabaseMigrator
{
    class Program
    {
        static void Main()
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", true, true)
                .Build();

            var destinationCosmosClient =
                new CosmosClient(
                    configuration["AzureCosmosDbOptions:DestinationConnectionString"]);
            var destinationCosmosDatabase =
                destinationCosmosClient.Databases["praxeum"];

            await DeleteContestsAsync(destinationCosmosDatabase);
            await DeleteContestLearnersAsync(destinationCosmosDatabase);

            var sourceCosmosClient =
                new CosmosClient(
                    configuration["AzureCosmosDbOptions:SourceConnectionString"]);
            var sourceCosmosDatabase =
                sourceCosmosClient.Databases["praxeum"];

            var leaderBoards =
                await GetLeaderBoardsAsync(sourceCosmosDatabase);

            var learners =
                await GetLearnersAsync(sourceCosmosDatabase);

            foreach (var leaderBoard in leaderBoards)
            {
                Console.WriteLine($"Adding leaderboard {leaderBoard.Name}...");

                var contest = new Contest();

                contest.Id = leaderBoard.Id;
                contest.Name = leaderBoard.Name;
                contest.Type = ContestType.Leaderboard;
                contest.Status = ContestStatus.InProgress;
                contest.StartDate = DateTime.UtcNow.AddDays(-14);
                contest.ProgressUpdateInterval = 1440;
                contest.LastProgressUpdateOn = DateTime.UtcNow;
                contest.NextProgressUpdateOn = contest.LastProgressUpdateOn.Value.AddMinutes(contest.ProgressUpdateInterval);
                contest.CreatedOn = leaderBoard.CreatedOn;

                contest = await AddContestAsync(
                    contest, destinationCosmosDatabase);

                Console.WriteLine(JsonConvert.SerializeObject(contest, Formatting.Indented));

                var experiencePointsCalculator =
                    new ExperiencePointsCalculator();

                foreach (var learner in learners.Where(x => x.LeaderBoards.Any(y => y.Id == contest.Id)))
                {
                    Console.WriteLine($"Adding learner {learner.UserName} to contest {contest.Name}...");

                    var contestLearner =
                        new ContestLearner();
                    
                    if (learner.ProgressStatus.CurrentLevel == 0)
                    {
                        learner.ProgressStatus.CurrentLevel = 1;
                    }

                    contestLearner.ContestId = contest.Id;
                    contestLearner.Id = Guid.NewGuid();
                    contestLearner.UserName = learner.UserName;
                    contestLearner.DisplayName = learner.DisplayName;
                    contestLearner.CreatedOn = learner.CreatedOn;
                    contestLearner.StartValue = 0;
                    contestLearner.CurrentValue = experiencePointsCalculator.Calculate(learner.ProgressStatus.CurrentLevel, learner.ProgressStatus.CurrentLevelPointsEarned);

                    contestLearner = await AddContestLearnerAsync(
                        contestLearner, destinationCosmosDatabase);

                    Console.WriteLine(JsonConvert.SerializeObject(contestLearner, Formatting.Indented));

                    Console.WriteLine($"Learner added.");
                }

                Console.WriteLine($"Leaderboard added.");
            }

            //var sourceCosmosClient =
            //    new CosmosClient(
            //        configuration["AzureCosmosDbOptions:SourceConnectionString"]);
            //var sourceCosmosDatabase =
            //    sourceCosmosClient.Databases["praxeum"];

            //var sourceLeaderBoardsCosmosContainer =
            //    sourceCosmosDatabase.Containers["leaderboards"];

            //var sourceLeaderBoardsQuery =
            //    $"SELECT * FROM lb";

            //var sourceLeaderBoardsQueryDefinition =
            //    new CosmosSqlQueryDefinition(sourceLeaderBoardsQuery);

            //var sourceLeaderBoards =
            //    sourceLeaderBoardsCosmosContainer.Items.CreateItemQuery<LeaderBoard>(
            //        sourceLeaderBoardsQueryDefinition,
            //        4);

            //var sourceLeaderBoardList = new List<LeaderBoard>();

            //while (sourceLeaderBoards.HasMoreResults)
            //{
            //    sourceLeaderBoardList.AddRange(
            //        await sourceLeaderBoards.FetchNextSetAsync());
            //};

            //foreach(var sourceLeaderBoard in sourceLeaderBoardList)
            //{
            //    Console.WriteLine($"Migrating leaderboard {sourceLeaderBoard.Name}");
            //}

            //var sourceLearnersSourceCosmosContainer =
            //    sourceCosmosDatabase.Containers["learners"];

            //var sourceLearnersQuery =
            //    $"SELECT * FROM l";

            //var sourceLearnersQueryDefinition =
            //    new CosmosSqlQueryDefinition(sourceLearnersQuery);

            //var sourceLearners =
            //    sourceLearnersSourceCosmosContainer.Items.CreateItemQuery<LeaderBoard>(
            //        sourceLearnersQueryDefinition,
            //        4);

            //var sourceLearnerList = new List<LeaderBoard>();

            //while (sourceLearners.HasMoreResults)
            //{
            //    sourceLearnerList.AddRange(
            //        await sourceLearners.FetchNextSetAsync());
            //};

            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }

        public static async Task<Contest> AddContestAsync(
            Contest contest,
            CosmosDatabase cosmosDatabase)
        {
            var cosmosContainer =
                cosmosDatabase.Containers["contests"];

            var document =
                await cosmosContainer.Items.CreateItemAsync<Contest>(
                    contest.Id.ToString(),
                    contest);

            return document.Resource;
        }

        public static async Task<ContestLearner> AddContestLearnerAsync(
            ContestLearner contestLearner,
            CosmosDatabase cosmosDatabase)
        {
            var cosmosContainer =
                cosmosDatabase.Containers["contestlearners"];

            var document =
                await cosmosContainer.Items.CreateItemAsync<ContestLearner>(
                    contestLearner.ContestId.ToString(),
                    contestLearner);

            return document.Resource;
        }

        public static async Task DeleteContestsAsync(
            CosmosDatabase cosmosDatabase)
        {
            var cosmosContainer =
                cosmosDatabase.Containers["contests"];

            var query =
                $"SELECT * FROM i";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var items =
               cosmosContainer.Items.CreateItemQuery<Contest>(
                    queryDefinition,
                    4);

            var itemList = new List<Contest>();

            while (items.HasMoreResults)
            {
                itemList.AddRange(
                    await items.FetchNextSetAsync());
            };

            Console.WriteLine($"Found {itemList.Count} contest(s).");

            foreach (var item in itemList)
            {
                Console.WriteLine($"Deleting contest {item.Name}...");

                await cosmosContainer.Items.DeleteItemAsync<Contest>(
                    item.Id.ToString(),
                    item.Id.ToString());

                Console.WriteLine($"Contest deleted.");
            }
        }

        public static async Task DeleteContestLearnersAsync(
            CosmosDatabase cosmosDatabase)
        {
            var cosmosContainer =
                cosmosDatabase.Containers["contestlearners"];

            var query =
                $"SELECT * FROM i";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var items =
               cosmosContainer.Items.CreateItemQuery<ContestLearner>(
                    queryDefinition,
                    4);

            var itemList = new List<ContestLearner>();

            while (items.HasMoreResults)
            {
                itemList.AddRange(
                    await items.FetchNextSetAsync());
            };

            Console.WriteLine($"Found {itemList.Count} contest learner(s).");

            foreach (var item in itemList)
            {
                Console.WriteLine($"Deleting contest learner {item.UserName}...");

                await cosmosContainer.Items.DeleteItemAsync<Contest>(
                    item.ContestId.ToString(),
                    item.Id.ToString());

                Console.WriteLine($"Contest learner deleted.");
            }

        }

        public static async Task<IList<LeaderBoard>> GetLeaderBoardsAsync(
            CosmosDatabase cosmosDatabase)
        {
            var cosmosContainer =
                cosmosDatabase.Containers["leaderboards"];

            var query =
                $"SELECT * FROM i";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var contests =
               cosmosContainer.Items.CreateItemQuery<LeaderBoard>(
                    queryDefinition,
                    4);

            var itemList = new List<LeaderBoard>();

            while (contests.HasMoreResults)
            {
                itemList.AddRange(
                    await contests.FetchNextSetAsync());
            };

            Console.WriteLine($"Found {itemList.Count} leader board(s).");

            return itemList;
        }

        public static async Task<IList<Learner>> GetLearnersAsync(
            CosmosDatabase cosmosDatabase)
        {
            var cosmosContainer =
                cosmosDatabase.Containers["learners"];

            var query =
                $"SELECT * FROM i";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var contests =
               cosmosContainer.Items.CreateItemQuery<Learner>(
                    queryDefinition,
                    4);

            var itemList = new List<Learner>();

            while (contests.HasMoreResults)
            {
                itemList.AddRange(
                    await contests.FetchNextSetAsync());
            };

            Console.WriteLine($"Found {itemList.Count} learner(s).");

            return itemList;
        }
    }
}
