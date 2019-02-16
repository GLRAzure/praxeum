using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.Data
{
    public class ContestLearner
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "contestId")]
        public Guid ContestId { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "userPrincipalName")]
        [Display(Name = "User Principal Name")]
        public string UserPrincipalName { get; set; }

        [JsonProperty(PropertyName = "userName")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "hasSeenMicrosoftPrivacyNotice")]
        [Display(Name = "Has Seen Microsoft Privacy Notice")]
        public bool HasSeenMicrosoftPrivacyNotice { get; set; }

        [JsonProperty(PropertyName = "startValue")]
        [Display(Name = "Start Value")]
        public int? StartValue { get; set; }

        [JsonProperty(PropertyName = "targetValue")]
        [Display(Name = "Target Value")]
        public int? TargetValue { get; set; }

        [JsonProperty(PropertyName = "currentValue")]
        [Display(Name = "Current Value")]
        public int? CurrentValue { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        [Display(Name = "Created")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "lastModifiedOn")]
        [Display(Name = "Last Modified")]
        public DateTime LastModifiedOn { get; set; }

        public ContestLearner()
        {
            this.Id =
                Guid.NewGuid();
            this.StartValue = 0;
            this.CurrentValue = 0;
            this.CreatedOn = DateTime.UtcNow;
            this.LastModifiedOn = this.CreatedOn;
        }
    }
}
