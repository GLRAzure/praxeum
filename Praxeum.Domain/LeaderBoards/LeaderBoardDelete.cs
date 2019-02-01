using System;
using Praxeum.WebApi.Helpers;

namespace Praxeum.Domain.LeaderBoards
{
    public class LeaderBoardDelete
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}