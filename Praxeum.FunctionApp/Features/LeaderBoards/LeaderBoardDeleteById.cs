using System;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardDeleteById : IRequest
    {
        [SwaggerExclude]
        public Guid Id { get; set; }
    }
}