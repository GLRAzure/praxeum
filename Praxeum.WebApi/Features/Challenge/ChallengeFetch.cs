using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Challenges
{
    public class ChallengeFetch
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}