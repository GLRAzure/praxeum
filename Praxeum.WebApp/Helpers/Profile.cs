using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace Praxeum.WebApp.Helpers
{
    public class Profile : TableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }

        public Profile()
        {
        }

        public Profile(
            string id)
        {
            this.PartitionKey = id;
            this.RowKey = id;
            this.Timestamp = DateTime.UtcNow;
            this.Roles = "Learner";
        }
    }
}
