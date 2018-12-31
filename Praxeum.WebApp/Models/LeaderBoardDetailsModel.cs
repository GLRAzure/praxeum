using System.Collections.Generic;

namespace Praxeum.WebApp.Models
{
    public class LeaderBoardDetailsModel : LeaderBoardIndexModel
    {
        public ICollection<LearnerDetailsModel> Learners { get; set; }
    }
}
