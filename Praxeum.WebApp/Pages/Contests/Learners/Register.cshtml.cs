﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.WebApp.Pages.Contests.Learners
{
    public class RegisterModel : PageModel
    {
        private readonly IHandler<ContestFetch, ContestFetched> _contestFetcher;
        private readonly IHandler<ContestLearnerAdd, ContestLearnerAdded> _contestLearnerAdder;

        [BindProperty]
        public ContestFetched Contest { get; set; }

        [BindProperty]
        public ContestLearnerAdd Learner { get; set; }

        public RegisterModel(
           IHandler<ContestFetch, ContestFetched> contestFetcher,
           IHandler<ContestLearnerAdd, ContestLearnerAdded> contestLearnerAdder)
        {
            _contestFetcher =
                contestFetcher;
            _contestLearnerAdder =
                contestLearnerAdder;
        }

        public async Task<IActionResult> OnGet(
            Guid? contestId)
        {
            if (contestId == null)
            {
                return NotFound();
            }

            await this.GetContestAsync(
                contestId.Value);

            this.Learner =
                new ContestLearnerAdd
                {
                    ContestId = contestId.Value
                };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            Guid? contestId)
        {
            if (contestId == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await this.GetContestAsync(
                    contestId.Value);

                return Page();
            }

            await _contestLearnerAdder.ExecuteAsync(
                this.Learner);

            return RedirectToPage(
                "/Contests/Details", new { id = this.Learner.ContestId });
        }

        private async Task GetContestAsync(
            Guid id)
        {
            var contest =
                await _contestFetcher.ExecuteAsync(
                    new ContestFetch
                    {
                        Id = id
                    });

            this.Contest = contest;
        }
    }
}