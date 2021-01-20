using AutoMapper;
using Autuas.Dtos;
using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Profiles
{
    public class GoalsProfile : Profile
    {
        public GoalsProfile()
        {
            //source -> tagret
            CreateMap<Goal, GoalReadDto>();
            CreateMap<GoalCreateDto, Goal>();
            CreateMap<GoalUpdateDto, Goal>();
            CreateMap<Goal, GoalUpdateDto>();
        }
    }
}
