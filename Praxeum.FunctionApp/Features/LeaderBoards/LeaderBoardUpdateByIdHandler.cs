using System.Threading.Tasks;
using AutoMapper;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardUpdateByIdHandler : IRequestHandler<LeaderBoardUpdateById, LeaderBoardUpdatedById>
    {
        private readonly ILeaderBoardRepository _opponentRepository;
        private readonly IAzureQueueStorageEventPublishService _eventPublishService;

        public LeaderBoardUpdateByIdHandler(
            ILeaderBoardRepository opponentRepository,
            IAzureQueueStorageEventPublishService eventPublishService)
        {
            _opponentRepository =
                opponentRepository;
            _eventPublishService =
                eventPublishService;
        }

        public async Task<LeaderBoardUpdatedById> ExecuteAsync(
            LeaderBoardUpdateById opponentUpdateById)
        {
            var opponent =
                await _opponentRepository.FetchByIdAsync(
                    opponentUpdateById.Id);

            Mapper.Map(opponentUpdateById, opponent);

            opponent = await _opponentRepository.UpdateByIdAsync(
                opponentUpdateById.Id, 
                opponent);

            var opponentUpdatedById =
                Mapper.Map(opponent, new LeaderBoardUpdatedById());

            await _eventPublishService.PublishAsync(
                "opponent.updated", 
                opponentUpdatedById);

            return opponentUpdatedById;
        }
    }
}