using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.WebApp.Pages.Contests.Learners
{
    public class DetailsModel : PageModel
    {
        private readonly IHandler<ContestLearnerFetch, ContestLearnerFetched> _contestLearnerFetcher;

        [BindProperty]
        public ContestLearnerFetched Learner { get; set; }

        public DetailsModel(
            IHandler<ContestLearnerFetch, ContestLearnerFetched> contestLearnerFetcher)
        {
            _contestLearnerFetcher = contestLearnerFetcher;
        }

        public async Task<IActionResult> OnGetAsync(
            Guid? contestId,
            Guid? id)
        {
            if (contestId == null)
            {
                return NotFound();
            }
            
            if (id == null)
            {
                return NotFound();
            }

            this.Learner =
                await _contestLearnerFetcher.ExecuteAsync(
                    new ContestLearnerFetch
                    {
                        Id = id.Value
                    });


            return Page();
        }
    }
}