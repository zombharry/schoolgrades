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
        //public RestCollection<Student> Students { get; set; }

        public RestCollection<StudentInfo> AvgGrades { get; set; }
        public RestCollection<StudentInfo> Credits { get; set; }

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
                AvgGrades = new RestCollection<StudentInfo>("http://localhost:48224/", "stat/AllAvarageGrade");

                Credits = new RestCollection<StudentInfo>("http://localhost:48224/", "stat/AllAvarageGrade");

            }
        }

    }
}
