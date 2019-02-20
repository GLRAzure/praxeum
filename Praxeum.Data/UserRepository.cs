using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Table;
using Praxeum.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praxeum.Data
{
    public class UserRepository : AzureTableStorageRepository, IUserRepository
    {
        public UserRepository(
            IOptions<AzureTableStorageOptions> azureTableStorageOptions) : base(azureTableStorageOptions)
        {
            _cloudTable =
                _cloudTableClient.GetTableReference("profiles");
        }

        public async Task<User> AddAsync(
            User user)
        {
            await _cloudTable.CreateIfNotExistsAsync();

            var insertOperation =
                TableOperation.Insert(user);

            await _cloudTable.ExecuteAsync(insertOperation);

            return user;
        }

        public async Task<User> FetchByIdAsync(
            string id)
        {
            await _cloudTable.CreateIfNotExistsAsync();

            var retrieveOperation =
                TableOperation.Retrieve<User>(
                    id,
                    id);

            var retrievedResult =
                await _cloudTable.ExecuteAsync(retrieveOperation);

            return (User)retrievedResult.Result;
        }

        public async Task<IEnumerable<User>> FetchListAsync()
        {
            await _cloudTable.CreateIfNotExistsAsync();

            var tableQuery = new TableQuery<User>();

            TableContinuationToken continuationToken = null;

            var userList = new List<User>();

            do
            {
                var tableQueryResult =
                    await _cloudTable.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);

                userList.AddRange(
                    tableQueryResult.Results);

                continuationToken = tableQueryResult.ContinuationToken;

            } while (continuationToken != null);

            return userList;
        }

        public async Task<User> UpdateByIdAsync(
            string id,
            User user)
        {
            var retrieveOperation =
                 TableOperation.Retrieve<User>(
                     id,
                     id);

            var retrievedResult =
                await _cloudTable.ExecuteAsync(retrieveOperation);

            var userToUpdate = 
                (User)retrievedResult.Result;

            userToUpdate.Name = user.Name;
            userToUpdate.Roles = user.Roles;

            var insertOperation =
                TableOperation.Replace(userToUpdate);

            await _cloudTable.ExecuteAsync(insertOperation);

            return user;
        }
    }
}
