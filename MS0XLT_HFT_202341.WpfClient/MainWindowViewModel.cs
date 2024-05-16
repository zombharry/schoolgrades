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
using System.Windows.Controls;
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

        //public RestCollection<Object> Statistics { get; set; }
        //public RestCollection<Grade> Grades { get; set; }
        //public RestCollection<Subject> Subjects { get; set; }

        public ICommand ShowStatisticWindowCommand { get; }

        public ICommand ShowGradeWindowCommand { get; }

        public ICommand ShowSubjectWindowCommand { get; }

        public ICommand CreateStudentCommand { get; set; }

        public ICommand DeleteStudentCommand { get; set; }

        public ICommand UpdateStudentCommand { get; set; }

     



        private Student selectedStudent;

        public Student SelectedStudent
        {

            get { return selectedStudent; }
            set
            {
                SetProperty(ref selectedStudent, value);
                (DeleteStudentCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateStudentCommand as RelayCommand).NotifyCanExecuteChanged();
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
                Students = new RestCollection<Student>("http://localhost:48224/", "student", "hub");
                //Subjects = new RestCollection<Subject>("http://localhost:48224/", "subject");
                //Grades = new RestCollection<Grade>("http://localhost:48224/", "grade");



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
                    () =>
                    {
                        return (SelectedStudent != null) && (SelectedStudent.StudentId!=0);
                    });

                SelectedStudent = new Student();

                CreateStudentCommand = new RelayCommand(() =>
                {
                    Students.Add(new Student()
                    {
                       
                        StudentName = SelectedStudent.StudentName,
                        Semester = SelectedStudent.Semester

                    }); ;
                });


                ShowStatisticWindowCommand = new RelayCommand(() =>
                {
                    StatisticWindow statisticWindow = new StatisticWindow();
                    statisticWindow.Show();
                });

                ShowGradeWindowCommand = new RelayCommand(() =>
                {
                    GradeWindow gradeWindow = new GradeWindow();
                    gradeWindow.Show();
                });

                ShowSubjectWindowCommand = new RelayCommand(() =>
                {
                    SubjectWindow subjectWindow = new SubjectWindow();
                    subjectWindow.Show();
                });
            }
        }


    }
}
