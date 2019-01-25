using System;
using System.Collections.Generic;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerListAdded
    {
        public Guid? LeaderBoardId { get; set; }

        public IEnumerable<LearnerAdded> Learners { get;set;}
    }
}