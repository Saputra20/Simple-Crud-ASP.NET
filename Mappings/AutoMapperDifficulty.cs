using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Simple_Api.Models.Domain;
using Simple_Api.Models.DTO.Difficulty;

namespace Simple_Api.Mappings
{
    public class AutoMapperDifficulty : Profile
    {
        public AutoMapperDifficulty()
        {
            CreateMap<Difficulty, DifficultyResponse>().ReverseMap();
        }
    }
}