using System.Threading.Tasks;
using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Learners
{
    public class LearnerDeleter : IHandler<LearnerDelete, LearnerDeleted>
    {
        private readonly ILearnerRepository _learnerRepository;

        public LearnerDeleter(
            ILearnerRepository learnerRepository)
        {
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerDeleted> ExecuteAsync(
            LearnerDelete learnerDelete)
        {
            var learner =
                await _learnerRepository.DeleteByIdAsync(
                    learnerDelete.Id);

            var learnerDeleted =
                Mapper.Map(learner, new LearnerDeleted());

            return learnerDeleted;
        }
    }
}