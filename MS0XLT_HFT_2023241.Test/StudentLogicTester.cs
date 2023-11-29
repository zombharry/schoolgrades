using Moq;
using MS0XLT_HFT_2023241.Logic;
using MS0XLT_HFT_2023241.Models;
using MS0XLT_HFT_2023241.Repository;
using NUnit.Framework;
using System;

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

            logic = new StudentLogic(mockStudentRepo.Object);
        }
    }
}
