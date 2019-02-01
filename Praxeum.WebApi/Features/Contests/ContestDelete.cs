using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Contests
{
    public class ContestDelete
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}