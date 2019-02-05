using Praxeum.Data;
using System.Linq;

namespace Praxeum.Domain.Contests
{
    public class ContestListed : Contest
    {
        public int NumberOfLearners => this.Learners.Count();
    }
}