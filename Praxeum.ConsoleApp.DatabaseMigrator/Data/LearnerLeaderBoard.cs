using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Praxeum.ConsoleApp.DatabaseMigrator.Data
{
 public class LearnerLeaderBoard
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
