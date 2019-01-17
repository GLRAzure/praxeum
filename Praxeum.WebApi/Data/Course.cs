using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Praxeum.WebApi.Data
{
    public class Course
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty(PropertyName = "abstract")]
        public string Abstract { get; set; }

        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        [JsonProperty(PropertyName = "lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }

        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        [JsonProperty(PropertyName = "products")]
        public List<string> Products { get; set; }

        [JsonProperty(PropertyName = "roles")]
        public List<string> Roles { get; set; }

        [JsonProperty(PropertyName = "levels")]
        public List<string> Levels { get; set; }

        [JsonProperty(PropertyName = "durationInMinutes")]
        public int DurationInMinutes { get; set; }

        [JsonProperty(PropertyName = "iconUrl")]
        public string IconUrl { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        public Course()
        {
            this.Products = 
                new List<string>();
            this.Roles = 
                new List<string>();
            this.Levels = 
                new List<string>();
        }
    }
}
