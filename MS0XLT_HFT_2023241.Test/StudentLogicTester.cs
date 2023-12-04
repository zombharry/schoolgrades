using Moq;
using MS0XLT_HFT_2023241.Logic;
using MS0XLT_HFT_2023241.Models;
using MS0XLT_HFT_2023241.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using static MS0XLT_HFT_2023241.Logic.StudentLogic;
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
            };

            mockStudentRepo.Setup(x => x.ReadAll()).Returns(StudentList.AsQueryable());
            mockStudentRepo.Setup(x => x.Read(1)).Returns(StudentList[0]);
            mockStudentRepo.Setup(x => x.Read(2)).Returns(StudentList[1]);
            mockStudentRepo.Setup(x => x.Read(3)).Returns(StudentList[2]);
            mockStudentRepo.Setup(x => x.Read(4)).Returns(StudentList[3]);

            logic = new StudentLogic(mockStudentRepo.Object);
        }

        [TestCase(1, 4)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 1)]
        public void GetAvarageGradeTest(int studentId, double expected)
        {
            var actual = logic.GetAvarageGrade(studentId);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AllAvarageGradeTest()
        {
            var expected = new List<StudentInfo> {
                new StudentInfo{
                    StudentId=1,
                    GradeAvg=4
                },
                new StudentInfo{
                    StudentId=2,
                    GradeAvg=2
                },
                new StudentInfo{
                    StudentId=3,
                    GradeAvg=3
                },
                new StudentInfo{
                    StudentId=4,
                    GradeAvg=1
                },
            };
            var actual = logic.AllAvarageGrade();
            Assert.AreEqual(expected, actual);
            

        }
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
