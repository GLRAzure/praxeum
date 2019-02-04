using System;
using System.Threading.Tasks;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerAdder : IHandler<ContestLearnerAdd, ContestLearnerAdded>
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ContestLearnerAdded> ExecuteAsync(
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            ContestLearnerAdd request)
        {
            throw new NotImplementedException();
        }
    }
}