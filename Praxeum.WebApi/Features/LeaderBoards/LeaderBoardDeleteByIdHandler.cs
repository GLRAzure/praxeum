using System.Threading.Tasks;
using AutoMapper;
using Praxeum.WebApi.Helpers;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardDeleteByIdHandler : IRequestHandler<LeaderBoardDeleteById, LeaderBoardDeletedById>
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly IAzureQueueStorageEventPublishService _eventPublishService;

        public LeaderBoardDeleteByIdHandler(
            ILeaderBoardRepository leaderBoardRepository,
            IAzureQueueStorageEventPublishService eventPublishService)
        {
            _leaderBoardRepository =
                leaderBoardRepository;
            _eventPublishService =
                eventPublishService;
        }

        public async Task<LeaderBoardDeletedById> ExecuteAsync(
            LeaderBoardDeleteById leaderBoardDeleteById)
        {
            var leaderBoard =
                await _leaderBoardRepository.FetchByIdAsync(
                    leaderBoardDeleteById.Id);

            LeaderBoardDeletedById leaderBoardDeletedById;

            leaderBoard =
                await _leaderBoardRepository.DeleteByIdAsync(
                    leaderBoardDeleteById.Id);

            leaderBoardDeletedById =
                Mapper.Map(leaderBoard, new LeaderBoardDeletedById());

            var teams =
                await _leaderBoardRepository.FetchTeamListByLeaderBoardIdAsync(
                    leaderBoardDeleteById.Id);

            foreach(var team in teams)
            {
                team.LeaderBoards.Remove(
                    leaderBoardDeleteById.Id);

                await _leaderBoardRepository.UpdateTeamByIdAsync(
                    team.Id,
                    team);
            }

            await _eventPublishService.PublishAsync("leaderBoard.deleted", leaderBoardDeletedById);

            return leaderBoardDeletedById;
        }
    }
}