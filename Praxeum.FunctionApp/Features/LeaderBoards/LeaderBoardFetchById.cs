using System;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardFetchById : IRequest
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}