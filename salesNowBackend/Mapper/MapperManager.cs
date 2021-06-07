using AutoMapper;
using salesNowBackend.DTO.ActivityDto;
using salesNowBackend.DTO.ContactPersonDto;
using salesNowBackend.DTO.CompanyDTO;
using salesNowBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using salesNowBackend.DTO.CompanyActivityDto;
using salesNowBackend.DTO.BusinessOpportunityDto;
using salesNowBackend.DTO.CompanyContactPersonDto;
using salesNowBackend.DTO.CompanyBusinessOpportunityDTO;

namespace salesNowBackend.Mapper
{
    public class MapperManager : Profile
    {
        public MapperManager()
        {           
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<CompanyForCreateDTO, Company>().ReverseMap();
            CreateMap<CompanyForUpdateDTO, Company>().ReverseMap(); ;

            CreateMap<Activity, ActivityDTO>().ReverseMap();
            CreateMap<Activity, ActivityForCreateDTO>().ReverseMap();
            CreateMap<Activity, ActivityForUpdateDTO>().ReverseMap();
            CreateMap<CompanyActivityDTO, Company>().ReverseMap();

            CreateMap<ContactPerson, ContactPersonDTO>().ReverseMap();
            CreateMap<ContactPerson, ContactPersonForCreateDTO>().ReverseMap();
            CreateMap<ContactPerson, ContactPersonForUpdateDTO>().ReverseMap();
            CreateMap<CompanyContactPersonDTO, Company>().ReverseMap();
            
            CreateMap<BusinessOpportunity, BusinessOpportunityDTO>().ReverseMap();
            CreateMap<BusinessOpportunity, BusinessOpportunityForCreateDTO>().ReverseMap();
            CreateMap<BusinessOpportunity, BusinessOpportunityForUpdateDTO>().ReverseMap();
            CreateMap<CompanyBusinessOpportunityDTO, Company>().ReverseMap();
        }
    }
}
