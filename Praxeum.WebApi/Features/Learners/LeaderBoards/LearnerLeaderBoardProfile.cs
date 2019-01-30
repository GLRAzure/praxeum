using AutoMapper;
using Praxeum.Data;

namespace Praxeum.WebApi.Features.Learners.LeaderBoards
{
    public class LearnerLeaderBoardProfile : Profile
    {
        public LearnerLeaderBoardProfile()
        {
            CreateMap<LearnerLeaderBoard, LearnerLeaderBoardAdded>();
            CreateMap<LearnerLeaderBoard, LearnerLeaderBoardDeleted>();
        }
    }
}
