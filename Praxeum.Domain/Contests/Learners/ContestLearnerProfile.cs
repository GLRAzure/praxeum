﻿using AutoMapper;
using Praxeum.Data;

namespace Praxeum.Domain.Contests.Learners
{
    public class ContestLearnerProfile : Profile
    {
        public ContestLearnerProfile()
        {
            CreateMap<ContestLearner, ContestLearnerAdded>()
                .ForMember(d => d.ContestId, o => o.Ignore());
            CreateMap<ContestLearner, ContestLearnerDeleted>()
                .ForMember(d => d.ContestId, o => o.Ignore());
            CreateMap<ContestLearner, ContestLearnerFetched>()
                .ForMember(d => d.ContestId, o => o.Ignore());
            CreateMap<ContestLearnerAdd, ContestLearner>();
            CreateMap<MicrosoftProfile, ContestLearner>()
                .ForMember(d => d.Status, o => o.Ignore())
                .ForMember(d => d.StatusMessage, o => o.Ignore())
                .ForMember(d => d.Rank, o => o.Ignore())
                .ForMember(d => d.OriginalProgressStatus, o => o.Ignore())
                .ForMember(d => d.CurrentProgressStatus, o => o.Ignore())
                .ForMember(d => d.LastModifiedOn, o => o.Ignore());
            CreateMap<MicrosoftProfileProgressStatus, ContestLearnerProgressStatus>();
        }
    }
}
