using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests;

namespace Praxeum.WebApp.Pages.Contests
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IHandler<ContestAdd, ContestAdded> _contestAdder;

        [BindProperty]
        public ContestAdd Contest { get; set; }

        public CreateModel(
           IHandler<ContestAdd, ContestAdded> contestAdder)
        {
            _contestAdder = contestAdder;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contestAdded =
                await _contestAdder.ExecuteAsync(this.Contest);

            return RedirectToPage("Details", new { id = contestAdded.Id });
        }
    }
}