using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Praxeum.Data
{
    public class User : TableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }

        public User()
        {
        }

        public User(
            string id)
        {
            this.PartitionKey = id;
            this.RowKey = id;
            this.Timestamp = DateTime.UtcNow;
            this.Roles = "Learner";
        }
    }
}
