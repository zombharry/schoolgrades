using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MS0XLT_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MS0XLT_HFT_202341.WpfClient.helperclass;
using System.Windows.Navigation;

namespace MS0XLT_HFT_202341.WpfClient
{
    public class StatisticWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        

        private RestService rest;

        public List<StudentInfo> AvgGrades { get { return rest.Get<StudentInfo>("stat/AllAvarageGrade/"); } }
        public List<StudentInfo> Credits { get { 
                return rest.Get<StudentInfo>("stat/StudentsCredits/");
            } }

        public List<Student> FailedStudents { get { return rest.Get<Student>("stat/failedStudents/"); } }

        //public List<object> combinedList
        //{
        //    get {
        //        List<object> transformedItems = (List<object>)AvgGrades.Select(item =>
        //        {

        //            return new StudentInfo
        //            {
        //                StudentId =((dynamic) item).StudentId,
        //                GradeAvg = ((dynamic)item).GradeAvg
        //            };
        //        });
        //        return transformedItems;
        //    }
        //}



        public RestCollection<Student> Students { get; set; }





        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public StatisticWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Students = new RestCollection<Student>("http://localhost:48224/", "student");

                rest = new RestService("http://localhost:48224/");

               // FailedStudents = new RestCollection<Student>("http://localhost:48224/stat/", "failedStudents", "hub");

            }
        }

    }
}
