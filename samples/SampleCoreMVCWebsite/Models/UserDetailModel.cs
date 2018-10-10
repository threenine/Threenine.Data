using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sample.Entity;
using Threenine.Map;

namespace SampleCoreMVCWebsite.Models
{
    public class UserDetailModel : ICustomMap
    {
        public int UserID { get; set; }
        
        public string Title { get; set; }
        
        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string Lastname { get; set; }
        
        public string Bio { get; set; }

        public string Headline { get; set; }

        public void CustomMap(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Person, UserDetailModel>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => string.Concat(src.FirstName, " ", src.LastName)))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest=> dest.Lastname , opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Headline, opt => opt.MapFrom(src => src.TagLine))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Profile))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.Id));
                
        }
    }
}
