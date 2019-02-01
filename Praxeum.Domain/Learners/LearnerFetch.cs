using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.Domain.Learners
{
    public class LearnerFetch
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}