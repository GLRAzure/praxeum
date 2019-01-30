using Newtonsoft.Json;
using System;

namespace Praxeum.Data
{
    public class LearnerLeaderBoard
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public LearnerLeaderBoard()
        {
        }

        public LearnerLeaderBoard(
            LeaderBoard leaderBoard)
        {
            this.Id = leaderBoard.Id;
            this.Name = leaderBoard.Name;
        }
    }
}
