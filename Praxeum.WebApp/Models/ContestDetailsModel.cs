using System.Collections.Generic;

namespace Praxeum.WebApp.Models
{
    public class ContestDetailsModel : ContestIndexModel
    {
        public ICollection<LearnerDetailsModel> Learners { get; set; }
    }
}
