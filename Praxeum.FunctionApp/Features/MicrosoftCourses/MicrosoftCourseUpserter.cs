using Praxeum.FunctionApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Praxeum.FunctionApp.Features.MicrosoftCourses
{
    public class MicrosoftCourseUpserter
    {
        IHandler<MicrosoftCourseList, IEnumerable<MicrosoftCourseListed>> _microsoftCourseLister;

        public MicrosoftCourseUpserter(
           IHandler<MicrosoftCourseList, IEnumerable<MicrosoftCourseListed>> microsoftCourseLister)
        {
            _microsoftCourseLister =
                microsoftCourseLister;
        }

        public async Task ExecuteAsync()
        {
            var microsoftCourseListedList =
                new List<MicrosoftCourseListed>();

            var microsoftCourseList =
                new MicrosoftCourseList
                    {
                       Page = 1,
                       PageSize = 30
                    };

            var currentMicrosoftCourseListed =
                await _microsoftCourseLister.ExecuteAsync(
                    microsoftCourseList);

            while (currentMicrosoftCourseListed.Count() != 0)
            {
                microsoftCourseListedList.AddRange(
                    currentMicrosoftCourseListed);

                microsoftCourseList.Page++;

                currentMicrosoftCourseListed =
                    await _microsoftCourseLister.ExecuteAsync(
                        microsoftCourseList);
            }

            foreach(var microsoftCourse in microsoftCourseListedList)
            {

            }
        }
    }
}