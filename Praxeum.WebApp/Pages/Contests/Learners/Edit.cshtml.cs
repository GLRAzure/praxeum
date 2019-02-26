using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.WebApp.Pages.Contests.Learners
{
    public class EditModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IHandler<ContestLearnerFetch, ContestLearnerFetched> _contestLearnerFetcher;
        private readonly IHandler<ContestLearnerUpdate, ContestLearnerUpdated> _contestLearnerUpdater;

        [BindProperty]
        public ContestLearnerFetched Learner { get; set; }

        public EditModel(
            IMapper mapper,
            IHandler<ContestLearnerFetch, ContestLearnerFetched> contestLearnerFetcher,
            IHandler<ContestLearnerUpdate, ContestLearnerUpdated> contestLearnerUpdater)
        {
            _mapper = mapper;
            _contestLearnerFetcher = contestLearnerFetcher;
            _contestLearnerUpdater = contestLearnerUpdater;
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
                        ContestId = contestId.Value,
                        Id = id.Value
                    });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contestLearnerUpdate =
                _mapper.Map(
                    this.Learner, new ContestLearnerUpdate());
            
            contestLearnerUpdate.Id = id.Value;
            contestLearnerUpdate.ContestId = contestId.Value;

            await _contestLearnerUpdater.ExecuteAsync(
                contestLearnerUpdate);

            return RedirectToPage("/Contests/Details", new { id = contestId });
        }
    }
}