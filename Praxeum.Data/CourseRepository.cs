using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Praxeum.Data.Helpers;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace Praxeum.Data
{
    public class CourseRepository : AzureCosmosDbRepository, ICourseRepository
    {
        public CourseRepository(
            IOptions<AzureCosmosDbOptions> azureCosmosDbOptions) : base(azureCosmosDbOptions)
        {
        }

        public async Task<Course> AddAsync(
            Course course)
        {
            var courseContainer =
               _cosmosDatabase.Containers["courses"];

            var courseDocument =
                await courseContainer.Items.CreateItemAsync<Course>(
                    course.Id.ToString(),
                    course);

            return courseDocument.Resource;
        }

        public async Task<Course> AddOrUpdateAsync(
            Course course)
        {
            var courseContainer =
               _cosmosDatabase.Containers["courses"];

            var courseDocument =
                await courseContainer.Items.UpsertItemAsync<Course>(
                    course.Id.ToString(),
                    course);

            return courseDocument.Resource;
        }

        public async Task<Course> DeleteByIdAsync(
            Guid id)
        {
            var courseContainer =
               _cosmosDatabase.Containers["courses"];

            var courseDocument =
                await courseContainer.Items.DeleteItemAsync<Course>(
                    id.ToString(),
                    id.ToString());

            return courseDocument.Resource;
        }

        public async Task<Course> FetchByIdAsync(
            Guid id)
        {
            var courseContainer =
               _cosmosDatabase.Containers["courses"];

            var courseDocument =
                await courseContainer.Items.ReadItemAsync<Course>(
                    id.ToString(),
                    id.ToString());

            return courseDocument.Resource;
        }

        public async Task<Course> FetchByUserNameAsync(
            string userName)
        {
            var courseContainer =
                _cosmosDatabase.Containers["courses"];

            var query =
                $"SELECT * FROM l where l.userName = @userName";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            queryDefinition.UseParameter(
                "@userName", userName);

            var courses =
                courseContainer.Items.CreateItemQuery<Course>(
                    queryDefinition, 
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var courseList = new List<Course>();

            while (courses.HasMoreResults)
            {
                courseList.AddRange(
                    await courses.FetchNextSetAsync());
            };

            var course =
                courseList.FirstOrDefault();

            return course;
        }

        public async Task<IEnumerable<Course>> FetchListAsync()
        {
            var courseContainer =
                _cosmosDatabase.Containers["courses"];

            var query =
                $"SELECT * FROM l";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            var courses =
                courseContainer.Items.CreateItemQuery<Course>(
                    queryDefinition, 
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var courseList = new List<Course>();

            while (courses.HasMoreResults)
            {
                courseList.AddRange(
                    await courses.FetchNextSetAsync());
            };

            return courseList;
        }

        public async Task<IEnumerable<Course>> FetchListAsync(
            Guid[] ids)
        {
            var courseContainer =
                _cosmosDatabase.Containers["courses"];

            var query =
                $"SELECT * FROM l";

            query += " WHERE ARRAY_CONTAINS(@ids, l.id)";

            var queryDefinition =
                new CosmosSqlQueryDefinition(query);

            queryDefinition.UseParameter("@ids", ids);

            var courses =
                courseContainer.Items.CreateItemQuery<Course>(
                    queryDefinition, 
                    _azureCosmosDbOptions.Value.MaxConcurrency);

            var courseList = new List<Course>();

            while (courses.HasMoreResults)
            {
                courseList.AddRange(
                    await courses.FetchNextSetAsync());
            };

            return courseList;
        }

        public async Task<Course> UpdateByIdAsync(
            Guid id,
            Course course)
        {
            var courseContainer =
               _cosmosDatabase.Containers["courses"];

            var courseDocument =
                await courseContainer.Items.ReplaceItemAsync<Course>(
                    id.ToString(),
                    id.ToString(),
                    course);

            return courseDocument.Resource;
        }
    }
}
