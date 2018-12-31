using System;
using System.ComponentModel.DataAnnotations;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardUpdateById : IRequest
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}