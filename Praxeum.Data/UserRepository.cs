using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Table;
using Praxeum.Data.Helpers;
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
    }
}
