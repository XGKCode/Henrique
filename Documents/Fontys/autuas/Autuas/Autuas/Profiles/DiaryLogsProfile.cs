using AutoMapper;
using Autuas.Dtos;
using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Profiles
{
    public class DiaryLogsProfile : Profile
    {
        public DiaryLogsProfile()
        {
            //source -> target
            CreateMap<ReflectiveDiaryLog, DiaryLogReadDto>();
            CreateMap<DiaryLogCreateDto, ReflectiveDiaryLog>();
            CreateMap<ReflectiveDiaryLog, DiaryLogUpdateDto>();
            CreateMap<DiaryLogUpdateDto, ReflectiveDiaryLog>();
        }


    }
}
