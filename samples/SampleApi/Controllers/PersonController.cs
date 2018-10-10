using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Entity;
using Threenine.Data;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _UOW;
        public PersonController(IUnitOfWork uow)
        {
            _UOW = uow;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var repo = _UOW.GetReadOnlyRepository<Person>();
            
           return repo.GetList().Items.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
