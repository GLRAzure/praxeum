using System;

namespace Praxeum.WebApp.Models
{
    public class LeaderBoardIndexModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfLearners { get; set; }
    }
}
