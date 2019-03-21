using Praxeum.Domain.Contests.Learners;
using System.ComponentModel.DataAnnotations;

namespace Praxeum.WebApp.Areas.Public.Models
{
    public class ContestLearnerRegistrationAdd : ContestLearnerAdd
    {
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must acccept the terms of use, to do this, check the terms accepted box.")]
        public bool HasAcceptedTerms { get; set; }

        public ContestLearnerRegistrationAdd()
        {
            this.HasAcceptedTerms = false;
        }
    }
}
