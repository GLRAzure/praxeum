using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Praxeum.WebApp.Helpers
{
    public class ProfileService
    {
        private IConfiguration _configuration;

        public ProfileService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Profile> AddOrUpdateAsync(
            ClaimsPrincipal principal)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(
               _configuration.GetValue<string>("AzureStorageOptions:ConnectionString"));

            var cloudTableClient =
                cloudStorageAccount.CreateCloudTableClient();

            var cloudTable =
                cloudTableClient.GetTableReference("profiles");

            await cloudTable.CreateIfNotExistsAsync();

            var nameIdentifierClaim =
                principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim == null)
            {
                throw new NullReferenceException($"Missing '{ClaimTypes.NameIdentifier}' claim.");
            }

            var nameClaim =
                principal.Claims.SingleOrDefault(x => x.Type == "name");

            if (nameClaim == null)
            {
                throw new NullReferenceException($"Missing 'name' claim.");
            }

            var emailsClaim =
                principal.Claims.SingleOrDefault(x => x.Type == "emails");

            if (emailsClaim == null)
            {
                throw new NullReferenceException($"Missing 'emails' claim.");
            }

            var profile =
                new Profile(nameIdentifierClaim.Value);

            profile.Name =
                nameClaim.Value;
            profile.Email =
                emailsClaim.Value;

            var insertOrReplaceOperation =
                TableOperation.InsertOrReplace(profile);

            await cloudTable.ExecuteAsync(insertOrReplaceOperation);

            return profile;
        }

        public async Task<Profile> GetAsync(
            ClaimsPrincipal principal)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(
               _configuration.GetValue<string>("AzureStorageOptions:ConnectionString"));

            var cloudTableClient =
                cloudStorageAccount.CreateCloudTableClient();

            var cloudTable =
                cloudTableClient.GetTableReference("profiles");

            await cloudTable.CreateIfNotExistsAsync();

            var nameIdentifierClaim =
                principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim == null)
            {
                throw new NullReferenceException($"Missing '{ClaimTypes.NameIdentifier}' claim.");
            }

            var retrieveOperation =
                TableOperation.Retrieve<Profile>(
                    nameIdentifierClaim.Value,
                    nameIdentifierClaim.Value);

            var retrievedResult =
                await cloudTable.ExecuteAsync(retrieveOperation);

            return (Profile)retrievedResult.Result;
        }
    }
}
