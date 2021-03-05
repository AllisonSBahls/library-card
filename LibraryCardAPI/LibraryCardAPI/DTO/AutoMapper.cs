using AutoMapper;
using LibraryCardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.DTO
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
