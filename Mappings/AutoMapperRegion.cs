using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Simple_Api.Models.Domain;
using Simple_Api.Models.DTO.Region;

namespace Simple_Api.Mappings
{
    public class AutoMapperRegion : Profile
    {
        public AutoMapperRegion()
        {
            CreateMap<Region, RegionResponse>().ReverseMap();
        }

    }
}