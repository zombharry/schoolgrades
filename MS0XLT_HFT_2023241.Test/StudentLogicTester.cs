using Moq;
using MS0XLT_HFT_2023241.Logic;
using MS0XLT_HFT_2023241.Models;
using MS0XLT_HFT_2023241.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static MS0XLT_HFT_2023241.Logic.StudentLogic.StudentInfo;

namespace MS0XLT_HFT_2023241.Test
{
    [TestFixture]
    public class StudentLogicTester
    {
        StudentLogic logic;
        Mock<IRepository<Student>> mockStudentRepo;

        [SetUp]
        public void Init()
        {
            mockStudentRepo = new Mock<IRepository<Student>>();
            var StudentList = new List<Student>() {
                new Student{
                    StudentId=1,
                    StudentName="Arnold",
                    Grades = new List<Grade>(){
                        new Grade{
                            GradeValue = 5,
                            Subject = new Subject { Credit=5 }
                        },
                        new Grade{
                            GradeValue = 3,
                            Subject = new Subject { Credit=2 }
                        }
                    }
                },
                new Student{
                    StudentId=2,
                    StudentName="Bob",
                    Grades = new List<Grade>(){
                        new Grade{
                            GradeValue = 1,
                            Subject = new Subject { Credit=5 }
                        },
                        new Grade{
                            GradeValue = 3,
                            Subject = new Subject { Credit=2 }
                        }
                    }
                },
                new Student{
                    StudentId=3,
                    StudentName="Charlie",
                    Grades = new List<Grade>(){
                        new Grade{
                            GradeValue = 3,
                            Subject = new Subject { Credit=1 }
                        },
                        new Grade{
                            GradeValue = 3,
                            Subject = new Subject { Credit=1 }
                        }
                    }
                },
                new Student{
                    StudentId=4,
                    StudentName="Daniel",
                    Grades = new List<Grade>(){
                        new Grade{
                            GradeValue = 1,
                            Subject = new Subject { Credit=3 } 
                        },
                        new Grade{
                            GradeValue = 1,
                            Subject = new Subject { Credit=4 }
                        }
                    } 
                },
            }.AsQueryable();

            mockStudentRepo.Setup(x => x.ReadAll()).Returns(StudentList);

            logic = new StudentLogic(mockStudentRepo.Object);
        }

        [Test]
        public void GetAvarageGradeTest()
        {
            
        }

        [Test]
        public void AllAvarageGradeTest()
        { }
        [Test]
        public void FailedStudentsTest()
        {
            var expected = new List<Student> {
                new Student{
                    StudentId=2,
                    StudentName="Bob",
                    Grades = new List<Grade>(){
                        new Grade{
                            GradeValue = 1,
                            Subject = new Subject { Credit=5 }
                        },
                        new Grade{
                            GradeValue = 3,
                            Subject = new Subject { Credit=2 }
                        }
                    }
                },
                 new Student{
                    StudentId=4,
                    StudentName="Daniel",
                    Grades = new List<Grade>(){
                        new Grade{
                            GradeValue = 1,
                            Subject = new Subject { Credit=3 }
                        },
                        new Grade{
                            GradeValue = 1,
                            Subject = new Subject { Credit=4 }
                        }
                    }
                 }
            };
            var actual = logic.FailedStudents().ToList();
            CollectionAssert.AreEqual(expected, actual,new SimpleStudentComparer());

        }

        [TestCase(1, 7)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        [TestCase(4, 0)]
        public void StudentCreditsTest(int studentId,int expected)
        {
            var credits = logic.StudentsCredits();
            Assert.AreEqual(expected, credits.First(x=>x.StudentId==studentId).NumberOfCredits);
        }
    }
}
