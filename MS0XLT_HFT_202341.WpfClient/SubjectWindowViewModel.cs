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

namespace MS0XLT_HFT_202341.WpfClient
{
    class SubjectWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Subject> Subjects { get; set; }

        private Subject selectedSubject;

        public Subject SelectedSubject
        {

            get { return selectedSubject; }
            set
            {
                SetProperty(ref selectedSubject, value);
                (DeleteSubjectCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateSubjectCommand { get; set; }

        public ICommand DeleteSubjectCommand { get; set; }

        public ICommand UpdateSubjectCommand { get; set; }



        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public SubjectWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Subjects = new RestCollection<Subject>("http://localhost:48224/", "subject");

               

                UpdateSubjectCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            Subjects.Update(SelectedSubject);
                        }
                        catch (ArgumentException ex)
                        {

                            ErrorMessage = ex.Message;
                        }
                    });

                DeleteSubjectCommand = new RelayCommand(
                    () => { Subjects.Delete(SelectedSubject.SubjectId); },
                    () =>
                    {
                        return SelectedSubject != null;
                    });

                CreateSubjectCommand = new RelayCommand(() =>
                {
                    Subjects.Add(new Subject()
                    {
                        SubjectId = SelectedSubject.SubjectId,
                        SubjectName = SelectedSubject.SubjectName,
                        Credit = SelectedSubject.Credit

                    }); ;
                });

                SelectedSubject = new Subject();
            }
        }

    }
}
