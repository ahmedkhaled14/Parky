using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ParkyAPI.Models;
using ParkyAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Mapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();
            CreateMap<Trail,TrailDTO>().ReverseMap();
            CreateMap<Trail,TrailInsertDTO>().ReverseMap();
            CreateMap<Trail,TrailUpdateDTO>().ReverseMap();

        }
    }
}
