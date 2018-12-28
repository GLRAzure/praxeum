using AutoMapper;
using Praxeum.WebApi.Data;

namespace Praxeum.WebApi.Features.LeaderBoards
{
    public class LeaderBoardProfile : Profile
    {
        public LeaderBoardProfile()
        {
            CreateMap<LeaderBoard, LeaderBoardDeletedById>();
            CreateMap<LeaderBoard, LeaderBoardFetchedList>();
            CreateMap<LeaderBoard, LeaderBoardFetchedById>();
            CreateMap<LeaderBoardAdd, LeaderBoard>()
                .ForMember(d => d.Learners, o => o.Ignore())
                .ForMember(d => d.IsActive, o => o.Ignore())
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<LeaderBoard, LeaderBoardAdded>();
            CreateMap<LeaderBoardUpdateById, LeaderBoard>()
                .ForMember(d => d.Learners, o => o.Ignore())
                .ForMember(d => d.CreatedOn, o => o.Ignore());
            CreateMap<LeaderBoard, LeaderBoardUpdatedById>();
        }
    }
}
