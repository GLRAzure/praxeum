using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Learners;

namespace Praxeum.WebApp.Pages.Learners
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IHandler<LearnerList, IEnumerable<LearnerListed>> _learnerLister;

        public IEnumerable<LearnerListed> Learners { get; private set; }

        public IndexModel(
            IHandler<LearnerList, IEnumerable<LearnerListed>> learnerLister)
        {
            _learnerLister =
                learnerLister;
        }

        public async Task OnGetAsync()
        {
            this.Learners =
                await _learnerLister.ExecuteAsync(
                    new LearnerList
                    {
                        OrderBy = "l.userName ASC",
                        MaximumRecords = null
                    });
        }
    }
}