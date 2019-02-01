using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.Domain.Learners
{
    public class LearnerDelete
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}