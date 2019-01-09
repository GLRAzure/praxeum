using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardDelete
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}