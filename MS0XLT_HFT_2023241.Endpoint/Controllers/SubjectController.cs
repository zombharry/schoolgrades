using Microsoft.AspNetCore.Mvc;
using MS0XLT_HFT_2023241.Logic;
using MS0XLT_HFT_2023241.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MS0XLT_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        ISubjectLogic logic;

        public SubjectController(ISubjectLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<SubjectController>
        [HttpGet]
        public IEnumerable<Subject> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<SubjectController>/5
        [HttpGet("{id}")]
        public Subject Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<SubjectController>
        [HttpPost]
        public void Create([FromBody] Subject value)
        {
            this.logic.Create(value);
        }

        // PUT api/<SubjectController>/5
        [HttpPut("{id}")]
        public void Update([FromBody] Subject value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<SubjectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
