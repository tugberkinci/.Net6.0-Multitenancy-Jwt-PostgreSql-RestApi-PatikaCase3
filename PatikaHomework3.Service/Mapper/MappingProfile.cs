using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PatikaHomework3.Dto.Dto;
using PatikaHomework3.Data.Model;

namespace PatikaHomework3.Service.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountDto, Account>().ReverseMap();
            CreateMap<PersonDto, Person>().ReverseMap();
            

        }

        
        
    }
}
