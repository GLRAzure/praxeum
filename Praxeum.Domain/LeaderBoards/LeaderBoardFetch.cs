using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.Domain.LeaderBoards
{
    public class LeaderBoardFetch
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}