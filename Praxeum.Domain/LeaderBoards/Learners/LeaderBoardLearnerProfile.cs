using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.LeaderBoards.Learners
{
    public class LeaderBoardLearnerProfile : Profile
    {
        public LeaderBoardLearnerProfile()
        {
            CreateMap<Learner, LeaderBoardLearnerAdded>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<Learner, LeaderBoardLearnerFetched>()
                .ForMember(d => d.IsCached, o => o.Ignore());
            CreateMap<LeaderBoardLearnerAdd, LeaderBoardLearner>();
        }
    }
}
