﻿using Newtonsoft.Json;
using System;

namespace Praxeum.Data
{
    public class LeaderBoard
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "numberOfLearners")]
        public int NumberOfLearners { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; }

        public LeaderBoard()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsActive = true;
        }
    }
}
