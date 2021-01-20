using AutoMapper;
using Autuas.Dtos;
using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            //source -> target
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }

    }
}
