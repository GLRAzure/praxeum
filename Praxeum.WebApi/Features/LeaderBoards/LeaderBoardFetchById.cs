using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardFetchById
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}