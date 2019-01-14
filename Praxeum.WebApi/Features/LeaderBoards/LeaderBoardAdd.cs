using System;
using System.ComponentModel.DataAnnotations;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardAdd
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }


        public LeaderBoardAdd()
        {
            this.Id = Guid.NewGuid();
        }
    }
}