using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardProfile : Profile
    {
        public LeaderBoardProfile()
        {
            CreateMap<LeaderBoard, LeaderBoardDeleted>();
            CreateMap<LeaderBoard, LeaderBoardListed>()
                .ForMember(d => d.Learners, o => o.Ignore());
            CreateMap<LeaderBoard, LeaderBoardFetched>()
                .ForMember(d => d.Learners, o => o.Ignore());
            CreateMap<LeaderBoardAdd, LeaderBoard>()
                .ForMember(d => d.IsActive, o => o.Ignore())
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<LeaderBoard, LeaderBoardAdded>()
                .ForMember(d => d.Learners, o => o.Ignore());
            CreateMap<LeaderBoardUpdate, LeaderBoard>()
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<LeaderBoard, LeaderBoardUpdated>();
        }
    }
}
