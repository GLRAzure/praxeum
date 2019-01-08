using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerDelete
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}