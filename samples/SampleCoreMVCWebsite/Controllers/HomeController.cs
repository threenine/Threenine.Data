using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var list = _unitOfWork.GetRepository<Person>().GetList();

            var model = Mapper.Map<IEnumerable<UserDetailModel>>(list);

            return View("index", model);
        }
        

        [HttpPost]
        public IActionResult Add(UserInputModel model)
        {
            var repo = _unitOfWork.GetRepository<Person>();

            var person = Mapper.Map<Person>(model);
            repo.Add(person);
            _unitOfWork.SaveChanges();

            var detail = Mapper.Map<UserDetailModel>(person);

            return RedirectToAction("Index", "Home", new { id = person.Id });
        }

        public IActionResult UserDetail(int id)
        {
            var repo = _unitOfWork.GetRepository<Person>();

            var user = repo.Single(x => x.Id == id);

            var details = Mapper.Map<UserDetailModel>(user);

            return View("UserDetail", details);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var repo = _unitOfWork.GetRepository<Person>();

            var user = repo.Single(x => x.Id == id);

            var details = Mapper.Map<UserInputModel>(user);

            return View("Edit", details);
        }

        [HttpPost]
        public ActionResult Edit(UserInputModel model)
        {
            var repo = _unitOfWork.GetRepository<Person>();

            var person = Mapper.Map<Person>(model);
             repo.Update(person);
            _unitOfWork.SaveChanges();

            return RedirectToAction("UserDetail", "Home", new { id = person.Id });
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
