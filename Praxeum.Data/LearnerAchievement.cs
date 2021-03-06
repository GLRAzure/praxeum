﻿using Newtonsoft.Json;
using System;

namespace Praxeum.Data
{
    public class LearnerAchievement
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "achievedDate")]
        public DateTime AchievedDate { get; set; }
    }
}
