using AutoMapper;
using Autuas.Dtos;
using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Profiles
{
    public class DailyCheckInsProfile : Profile
    {
        public DailyCheckInsProfile()
        {
            //source -> target

            CreateMap<DailyCheckIn, DailyCheckInReadDto>();
            CreateMap<DailyCheckInCreateDto, DailyCheckIn>();
        }
    }
}
