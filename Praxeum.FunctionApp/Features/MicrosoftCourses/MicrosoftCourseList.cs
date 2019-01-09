namespace Praxeum.FunctionApp.Features.MicrosoftCourses
{
    public class MicrosoftCourseList
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public MicrosoftCourseList()
        {
            this.Page = 0;
            this.PageSize = 30;
        }
    }
}
