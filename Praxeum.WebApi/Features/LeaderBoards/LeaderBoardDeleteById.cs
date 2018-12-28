using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardDeleteById : IRequest
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}