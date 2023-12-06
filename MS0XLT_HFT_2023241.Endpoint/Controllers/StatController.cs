using Microsoft.AspNetCore.Mvc;
using MS0XLT_HFT_2023241.Logic;
using MS0XLT_HFT_2023241.Models;
using System.Collections.Generic;
using static MS0XLT_HFT_2023241.Logic.StudentLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MS0XLT_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {

        IStudentLogic studentLogic;
        ISubjectLogic subjectLogic;

        public StatController(IStudentLogic studentLogic, ISubjectLogic subjectLogic)
        {
            this.studentLogic = studentLogic;
            this.subjectLogic = subjectLogic;
        }
        
        [HttpGet("{num}")]
        public IEnumerable<Subject> MostFailedSubjects(int num)
        {
            return this.subjectLogic.MostFailedSubjects(num);
        }

        [HttpGet]
        public IEnumerable<StudentInfo> AllAvarageGrade()
        {
            return this.studentLogic.AllAvarageGrade();
        }


        [HttpGet]
        public IEnumerable<Student> FailedStudents()
        {
            return this.studentLogic.FailedStudents();
        }

        [HttpGet("{id}")]
        public double? GetAvarageGrade(int id)
        {
            return this.studentLogic.GetAvarageGrade(id);
        }

        [HttpGet]
        public IEnumerable<StudentInfo> StudentsCredits()
        {
            return this.studentLogic.StudentsCredits();
        }
    }
}
