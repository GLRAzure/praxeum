using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Praxeum.Domain;
using Praxeum.Domain.Contests;
using Praxeum.Domain.Contests.Learners;

namespace Praxeum.WebApp.Pages.Contests
{
    [Authorize(Roles = "Administrator")]
    public class ExportModel : PageModel
    {
        private readonly IHandler<ContestFetch, ContestFetched> _contestFetcher;

        public ExportModel(
            IHandler<ContestFetch, ContestFetched> contestFetcher)
        {
            _contestFetcher = contestFetcher;
        }

        public async Task<IActionResult> OnGetAsync(
            Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contest =
                await _contestFetcher.ExecuteAsync(
                    new ContestFetch
                    {
                        Id = id.Value
                    });

            var contestLearners =
                contest.Learners
                    .OrderByDescending(x => x.ProgressValue)
                    .ThenBy(x => x.UserName)
                        .ToList();

            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            using (var csv = new CsvWriter(sw))
            {
                csv.WriteRecords(contestLearners);

                var fileContents =
                    ASCIIEncoding.ASCII.GetBytes(sb.ToString());

                return File(fileContents, "application/octet-stream", $"{contest.Name.Replace(" ", "_")}_{DateTime.UtcNow.ToString("yyyyMMdd_HHmmss")}.csv");
            }
        }
    }
}