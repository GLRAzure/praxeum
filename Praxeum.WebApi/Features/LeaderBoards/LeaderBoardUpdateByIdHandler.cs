using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardUpdateByIdHandler : IRequestHandler<LeaderBoardUpdateById, LeaderBoardUpdatedById>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly IAzureQueueStorageEventPublishService _eventPublishService;

        public LeaderBoardUpdateByIdHandler(
            ILeaderBoardRepository leaderBoardRepository,
            IAzureQueueStorageEventPublishService eventPublishService)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
            _eventPublishService =
                eventPublishService;
        }

        public async Task<LeaderBoardUpdatedById> ExecuteAsync(
            LeaderBoardUpdateById leaderBoardUpdateById)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardUpdateById.Id);

            Mapper.Map(leaderBoardUpdateById, leaderBoard);

            leaderBoard = await _leaderBoardRepository.UpdateByIdAsync(
                leaderBoardUpdateById.Id, 
                leaderBoard);

            var leaderBoardUpdatedById =
                Mapper.Map(leaderBoard, new LeaderBoardUpdatedById());

            await _eventPublishService.PublishAsync(
                "leaderBoard.updated", 
                leaderBoardUpdatedById);

            return leaderBoardUpdatedById;
        }
    }
}