using System.Threading.Tasks;
using AutoMapper;
using Praxeum.FunctionApp.Helpers;

namespace Praxeum.FunctionApp.Features.LeaderBoards
{
    public class LeaderBoardDeleteByIdHandler : IRequestHandler<LeaderBoardDeleteById, LeaderBoardDeletedById>
    {
        private readonly ILeaderBoardRepository _opponentRepository;
        private readonly IAzureQueueStorageEventPublishService _eventPublishService;

        public LeaderBoardDeleteByIdHandler(
            ILeaderBoardRepository opponentRepository,
            IAzureQueueStorageEventPublishService eventPublishService)
        {
            _opponentRepository =
                opponentRepository;
            _eventPublishService =
                eventPublishService;
        }

        public async Task<LeaderBoardDeletedById> ExecuteAsync(
            LeaderBoardDeleteById opponentDeleteById)
        {
            var opponent =
                await _opponentRepository.FetchByIdAsync(
                    opponentDeleteById.Id);

            LeaderBoardDeletedById opponentDeletedById;

            opponent =
                await _opponentRepository.DeleteByIdAsync(
                    opponentDeleteById.Id);

            opponentDeletedById =
                Mapper.Map(opponent, new LeaderBoardDeletedById());

            var teams =
                await _opponentRepository.FetchTeamListByLeaderBoardIdAsync(
                    opponentDeleteById.Id);

            foreach(var team in teams)
            {
                team.LeaderBoards.Remove(
                    opponentDeleteById.Id);

                await _opponentRepository.UpdateTeamByIdAsync(
                    team.Id,
                    team);
            }

            await _eventPublishService.PublishAsync("opponent.deleted", opponentDeletedById);

            return opponentDeletedById;
        }
    }
}