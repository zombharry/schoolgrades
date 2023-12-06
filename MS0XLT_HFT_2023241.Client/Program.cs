using ConsoleTools;
using MS0XLT_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MS0XLT_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Student")
            {
                Console.Write("Student name: ");
                string name = Console.ReadLine();
                rest.Post(new Student() { StudentName = name }, "student");
            }
            else if (entity == "Grade") 
            {
                Console.Write("Grade Value: ");
                int gradeValue = int.Parse(Console.ReadLine());
                Console.WriteLine("Subject Id: ");
                int subjectId =int.Parse(Console.ReadLine());
                Console.WriteLine("Student Id: ");
                int studentId = int.Parse(Console.ReadLine());

                rest.Post(new Grade() { GradeValue = gradeValue,SubjectId=subjectId,StudentId=studentId,Date=DateTime.Now }, "grade");
            }
            else
            {
                Console.WriteLine("Subject Name: ");
                string subjectName = Console.ReadLine();
                Console.WriteLine("Credit Value: ");
                int credit = int.Parse(Console.ReadLine());

                rest.Post(new Subject() {SubjectName = subjectName,Credit=credit },"subject");
            }
        }
        static void List(string entity)
        {
            if (entity == "Student")
            {
                List<Student> students = rest.Get<Student>("student");
                foreach (var item in students)
                {
                    Console.WriteLine(item.StudentId+": "+item.StudentName);
                }
                
            }
            else if (entity == "Grade")
            {
                List<Grade> grades = rest.Get<Grade>("grade");
                foreach (var item in grades)
                {
                    Console.WriteLine("Student: "+item.StudentId + ", Subject: " + item.SubjectId+", Value: "+item.GradeValue+" Date: "+item.Date);
                }

            }
            else
            {
                List<Subject> subjects = rest.Get<Subject>("subject");
                foreach (var item in subjects)
                {
                    Console.WriteLine(item.SubjectId+ ": "+item.SubjectName+ " Credit: "+item.Credit);
                }

            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Student")
            {
                Console.WriteLine( "Student Id: " );
                int id = int.Parse(Console.ReadLine());
                Student newStudent = rest.Get<Student>(id, "student");
                Console.WriteLine( "New name" );
                string name = Console.ReadLine();
                Console.WriteLine("New semester");
                int semester = int.Parse(Console.ReadLine());
                newStudent.StudentName = name;
                newStudent.Semester = semester;
                rest.Put(newStudent, "student");
            }
            else if (entity == "Grade")
            {
                Console.WriteLine("Grade Id: ");
                int id = int.Parse(Console.ReadLine());
                Console.Write("New Grade Value: ");
                int gradeValue = int.Parse(Console.ReadLine());
                Console.WriteLine("New Subject Id: ");
                int subjectId = int.Parse(Console.ReadLine());
                Console.WriteLine("New Student Id: ");
                int studentId = int.Parse(Console.ReadLine());
                Grade newGrade = rest.Get<Grade>(id, "grade");

                newGrade.GradeValue = gradeValue;
                newGrade.SubjectId = subjectId;
                newGrade.StudentId = studentId;
                newGrade.Date = DateTime.Now;
                rest.Put(newGrade, "grade");
            }
            else
            {
                Console.WriteLine("Subject Id: ");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("New Subject Name: ");
                string subjectName = Console.ReadLine();
                Console.WriteLine("New Credit Value: ");
                int credit = int.Parse(Console.ReadLine());

                Subject newSubject = rest.Get<Subject>(id, "subject");



                newSubject.SubjectName = subjectName;
                newSubject.Credit = credit;
                rest.Put(newSubject, "subject");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Student")
            {
                Console.WriteLine("Student Id: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "student");

            }
            else if (entity == "Grade")
            {
                Console.WriteLine("Grade Id: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "grade");

            }
            else
            {
                Console.WriteLine("Subject Id: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "subject");
            }

        }
        static void NonCruds(string action) 
        {
            if (action == "MostFailedSubjects") {
                Console.WriteLine("Number of subjects in the list: ");
                int num = int.Parse(Console.ReadLine());
                
                List<Subject> subjects = rest.Get<Subject>("stat/MostFailedSubjects/"+num);
                foreach (var item in subjects)
                {
                    Console.WriteLine(item.SubjectId + ": " + item.SubjectName + " Credit: " + item.Credit);
                }
                Console.ReadLine();
            }
            else if (action == "AllAvarageGrade") {
                var avarageGrades = rest.Get<dynamic>("stat/AllAvarageGrade/");
                foreach (var item in avarageGrades)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.ReadLine();


            }
            else if (action == "GetAvarageGrade") {

                Console.WriteLine("Student Id: ");
                int id = int.Parse(Console.ReadLine());
                double? avarageGrade = rest.Get<double>(id, "stat/GetAvarageGrade");
                Console.WriteLine("Avarage grade: "+ avarageGrade);
                Console.ReadLine();

            }
            else if (action == "FailedStudents") {
                List<Student> students = rest.Get<Student>("stat/FailedStudents/");
                foreach (var item in students)
                {
                    Console.WriteLine(item.StudentId+": "+ item.StudentName);
                }
                Console.ReadLine();

            }
            else if (action == "StudentsCredits") {
                var credits = rest.Get<dynamic>("stat/StudentsCredits/");
                foreach (var item in credits)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.ReadLine();
            }

        }

        static void Main(string[] args)
        {
            

            rest = new RestService("http://localhost:48224");

            

            var studentSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Student"))
                .Add("Create", () => Create("Student"))
                .Add("Delete", () => Delete("Student"))
                .Add("Update", () => Update("Student"))
                .Add("Exit", ConsoleMenu.Close);

            var gradeSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Grade"))
                .Add("Create", () => Create("Grade"))
                .Add("Delete", () => Delete("Grade"))
                .Add("Update", () => Update("Grade"))
                .Add("Exit", ConsoleMenu.Close);

            var subjectSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Subject"))
                .Add("Create", () => Create("Subject"))
                .Add("Delete", () => Delete("Subject"))
                .Add("Update", () => Update("Subject"))
                .Add("Exit", ConsoleMenu.Close);

            var statSubMenu = new ConsoleMenu(args, level: 1)
                 .Add("MostFailedSubjects", () => NonCruds("MostFailedSubjects"))
                 .Add("AllAvarageGrade", () => NonCruds("AllAvarageGrade"))
                 .Add("GetAvarageGrade", () => NonCruds("GetAvarageGrade"))
                 .Add("FailedStudents", () => NonCruds("FailedStudents"))
                 .Add("StudentsCredits", () => NonCruds("StudentsCredits"))
                 .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Grades", () => gradeSubMenu.Show())
                .Add("Students", () => studentSubMenu.Show())
                .Add("Subjectss", () => subjectSubMenu.Show())
                .Add("Stat", () => statSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
