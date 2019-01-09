using Praxeum.FunctionApp.Data;
using System;

namespace Praxeum.FunctionApp.Features.MicrosoftCourses
{
    public class MicrosoftCourseFetched : MicrosoftCourse
    {
        public MicrosoftCourseFetched()
        {

        }

        public MicrosoftCourseFetched(
            dynamic microsoftCourse)
        {
            this.Id = microsoftCourse.uid.ToString().Split('.')[1];
            this.Title = microsoftCourse.title;
            this.ResourceType = microsoftCourse.resource_type;
            this.Abstract = microsoftCourse.@abstract;
            this.Summary = microsoftCourse.summary;
            this.Locale = microsoftCourse.locale;
            this.IconUrl = $"https://docs.microsoft.com/en-us{microsoftCourse.icon_url}";
            this.Url = $"https://docs.microsoft.com/en-us{microsoftCourse.url}";
            this.LastModifiedDate = DateTime.Parse(microsoftCourse.last_modified.ToString());
            this.DurationInMinutes = int.Parse(microsoftCourse.duration_in_minutes.ToString());

            foreach (var product in microsoftCourse.products)
            {
                this.Products.Add(product.ToString());
            }

            foreach (var role in microsoftCourse.roles)
            {
                this.Roles.Add(role.ToString());
            }

            foreach (var level in microsoftCourse.levels)
            {
                this.Levels.Add(level.ToString());
            }
        }
    }
}
