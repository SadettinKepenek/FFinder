﻿using AutoMapper;
using FFinder.Core.DataTransferObjects.CommentRate;
using FFinder.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FFinder.MappingProfiles
{
    public class CommentRateMappingProfile:Profile
    {
        public CommentRateMappingProfile()
        {
            CreateMap<CommentRateAddDto, CommentRate>().ReverseMap();
            CreateMap<CommentRateListDto, CommentRate>();
            CreateMap<CommentRateDetailDto, CommentRate>();
            CreateMap<CommentRateUpdateDto, CommentRate>().ReverseMap();
        }
    }
}
