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
        //public RestCollection<Student> Students { get; set; }

        private RestService rest;

        public List<object> AvgGrades { get { return rest.Get<dynamic>("stat/AllAvarageGrade/"); } }
        public List<object> Credits { get { 
                return rest.Get<dynamic>("stat/StudentsCredits/");
            } }

        



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

            }
        }

    }
}
