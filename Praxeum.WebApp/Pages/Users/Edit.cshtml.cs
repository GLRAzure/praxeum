using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Users;

namespace Praxeum.WebApp.Pages.Users
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IHandler<UserFetch, UserFetched> _userFetcher;
        private readonly IHandler<UserUpdate, UserUpdated> _userUpdater;

        [BindProperty]
        public UserFetched UserProfile { get; set; }

        public EditModel(
            IMapper mapper,
            IHandler<UserFetch, UserFetched> userFetcher,
            IHandler<UserUpdate, UserUpdated> userUpdater)
        {
            _mapper = mapper;
            _userFetcher = userFetcher;
            _userUpdater = userUpdater;
        }

        public async Task<IActionResult> OnGetAsync(
            Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            this.UserProfile =
                await _userFetcher.ExecuteAsync(
                    new UserFetch
                    {
                        Id = id.ToString()
                    });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
           Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userUpdate =
                _mapper.Map(
                    this.UserProfile, new UserUpdate());

            userUpdate.Id = id.ToString();

            await _userUpdater.ExecuteAsync(
                userUpdate);

            return RedirectToPage("Index");
        }
    }
}