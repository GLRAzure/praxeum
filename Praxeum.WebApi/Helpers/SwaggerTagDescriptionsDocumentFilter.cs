using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Praxeum.WebApi.Helpers
{
    public class SwaggerTagDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new[] {
                new Tag { Name = "Contests"},
                new Tag { Name = "Contest Learners"},
                new Tag { Name = "Leader Boards"},
                new Tag { Name = "Learner Leader Boards"},
                new Tag { Name = "Learners"}
            };
        }
    }
}