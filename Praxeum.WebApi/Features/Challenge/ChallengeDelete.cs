using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Challenges
{
    public class ChallengeDelete
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}