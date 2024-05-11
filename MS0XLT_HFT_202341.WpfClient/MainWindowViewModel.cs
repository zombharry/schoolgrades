using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MS0XLT_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MS0XLT_HFT_202341.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Student> Students { get; set; }

        public ICommand CreateStudentCommand { get; set; }

        public ICommand DeleteStudentCommand { get; set; }

        public ICommand UpdateStudentCommand { get; set; }

        public ICommand GetCreditsCommand { get; set; }

        public ICommand GetFailedStudentsCommand { get; set; }



        private Student selectedStudent;

        public Student SelectedStudent
        {
            
            get { return selectedStudent; }
            set 
            { 
                SetProperty(ref selectedStudent, value);
                (DeleteStudentCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

       
        public static bool IsInDesignMode 
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Students = new RestCollection<Student>("http://localhost:48224", "student","hub");

                CreateStudentCommand = new RelayCommand(() =>
                {
                    Students.Add(new Student()
                    {
                        StudentId = SelectedStudent.StudentId,
                        StudentName = SelectedStudent.StudentName,
                        Semester = SelectedStudent.Semester

                    }); ;
                });

                UpdateStudentCommand = new RelayCommand(
                    () => 
                    {
                        try
                        {
                            Students.Update(SelectedStudent);
                        }
                        catch (ArgumentException ex)
                        {

                            ErrorMessage = ex.Message;
                        }
                    });

                DeleteStudentCommand = new RelayCommand(
                    () => { Students.Delete(selectedStudent.StudentId); },
                    () => { return SelectedStudent != null;
                    });

                SelectedStudent = new Student();
            }
        }

    }
}
