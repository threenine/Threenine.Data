using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Entity;
using SampleCoreMVCWebsite.Models;
using Threenine.Data;

namespace SampleCoreMVCWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserInputModel model)
        {
            var repo = _unitOfWork.GetRepository<Person>();

            var person = Mapper.Map<Person>(model);
           repo.Add(person);
            _unitOfWork.SaveChanges();

            var detail = Mapper.Map<UserDetailModel>(person);

          return  RedirectToAction("UserDetail", "Home", new {id = person.Id});



        }

        public IActionResult UserDetail(int id)
        {
            var repo = _unitOfWork.GetRepository<Person>();

            var user = repo.Single(x => x.Id == id);

            var details = Mapper.Map<UserDetailModel>(user);

            return View("UserDetail", details);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var profile = Mapper.Map<EditProfileDetail>(_unitOfWork.GetRepository<Person>().Single(p => p.Id == id));
            return View("Edit", profile);
        }

        [HttpPost]
        public IActionResult Edit(EditProfileDetail profile)
        {
            _unitOfWork.GetRepository<Person>().Update(Mapper.Map<Person>(profile));

            return RedirectToAction("UserDetail", "Home", new { id = profile.UserId });

        }
       

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
