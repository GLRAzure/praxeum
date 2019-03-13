using Newtonsoft.Json;
using System.Collections.Generic;

namespace Praxeum.Data
{
    public class MicrosoftProfileGameStatus
    {
        [JsonProperty(PropertyName = "totalPoints")]
        public int TotalPoints { get; set; }

        [JsonProperty(PropertyName = "level")]
        public MicrosoftProfileGameStatusLevel Level { get; set; }

        [JsonProperty(PropertyName = "levelAchievedDateUTC")]
        public string LevelAchievedDate { get; set; }

        [JsonProperty(PropertyName = "nextLevelNumber")]
        public int NextLevelNumber { get; set; }

        [JsonProperty(PropertyName = "currentLevelPointsEarned")]
        public int CurrentLevelPointsEarned { get; set; }

        [JsonProperty(PropertyName = "pointsToNextLevel")]
        public int PointsToNextLevel { get; set; }

        [JsonProperty(PropertyName = "achievements")]
        public IList<MicrosoftProfileGameStatusAchievement> Achievements { get; set; }

        public MicrosoftProfileGameStatus()
        {
            this.Level = new MicrosoftProfileGameStatusLevel();
            this.Achievements = new List<MicrosoftProfileGameStatusAchievement>();
        }
    }
}
