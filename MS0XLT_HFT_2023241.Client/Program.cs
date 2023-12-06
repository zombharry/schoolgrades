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
        }
        static void List(string entity)
        {
            if (entity == "Student")
            {
                List<Student> students = rest.Get<Student>("student");
                foreach (var item in students)
                {
                    Console.WriteLine(item.StudentName);
                }
                
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:48224","student");
          

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

            var subjectorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Subject"))
                .Add("Create", () => Create("Subject"))
                .Add("Delete", () => Delete("Subject"))
                .Add("Update", () => Update("Subject"))
                .Add("Exit", ConsoleMenu.Close);

           


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Grades", () => gradeSubMenu.Show())
                .Add("Students", () => studentSubMenu.Show())
                .Add("Subjectss", () => subjectorSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
