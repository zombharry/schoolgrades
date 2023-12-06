using Microsoft.AspNetCore.Mvc;
using MS0XLT_HFT_2023241.Logic;
using MS0XLT_HFT_2023241.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MS0XLT_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentLogic logic;

        public StudentController(IStudentLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<Student> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public Student Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Create([FromBody] Student value)
        {
            this.logic.Create(value);
        }

        // PUT api/<StudentController>/5
        [HttpPut]
        public void Update([FromBody] Student value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
