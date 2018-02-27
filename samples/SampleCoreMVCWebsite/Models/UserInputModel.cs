using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sample.Entity;
using Threenine.Map;

namespace SampleCoreMVCWebsite.Models
{
    public class UserInputModel :   IMapTo<Person>
    {
        public int Id { get; set; }
        public string Salutation { get; set; }

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "Bio")]
        public string  Profile { get; set; }

        [Display(Name="Tag")]
        public string TagLine { get; set; }


        
    }
}
