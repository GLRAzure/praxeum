using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Praxeum.WebApp.Pages.LeaderBoards.Learners
{
    [Authorize(Roles = "Administrator")]
    public class AddModel : PageModel
    {
        public IActionResult OnGet(
            Guid leaderBoardId)
        {
            return RedirectToPage("/Learners/Create", new { leaderBoardId });
        }
    }
}