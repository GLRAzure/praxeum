using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.Learners
{
    public class LearnerDeleteByIdHandler : ILearnerHandler<LearnerDeleteById, LearnerDeletedById>
    {
        private readonly ILearnerRepository _learnerRepository;

        public LearnerDeleteByIdHandler(
            ILearnerRepository learnerRepository)
        {
            _learnerRepository =
                learnerRepository;
        }

        public async Task<LearnerDeletedById> ExecuteAsync(
            LearnerDeleteById learnerDeleteById)
        {
            var learner =
                await _learnerRepository.DeleteByIdAsync(
                    learnerDeleteById.Id);

            var learnerDeletedById =
                Mapper.Map(learner, new LearnerDeletedById());

            return learnerDeletedById;
        }
    }
}