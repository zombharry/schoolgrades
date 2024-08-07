﻿using Microsoft.Toolkit.Mvvm.Input;
using MS0XLT_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MS0XLT_HFT_202341.WpfClient
{
    public class GradeWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Grade> Grades { get; set; }

        private Grade selectedGrade;

        public Grade SelectedGrade
        {

            get { return selectedGrade; }
            set
            {
                
                SetProperty(ref selectedGrade, value);
                (DeleteGradeCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        public ICommand CreateGradeCommand { get; set; }

        public ICommand DeleteGradeCommand { get; set; }

        public ICommand UpdateGradeCommand { get; set; }



        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public GradeWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Grades = new RestCollection<Grade>("http://localhost:48224/", "grade");

               

                
                DeleteGradeCommand = new RelayCommand(
                    () => { Grades.Delete(SelectedGrade.GradeId); },
                    () =>
                    {
                        return (SelectedGrade != null) && (SelectedGrade.GradeId != 0);
                    });

                SelectedGrade = new Grade();
                CreateGradeCommand = new RelayCommand(() =>
                {
                    Grades.Add(new Grade()
                    {
                        StudentId = SelectedGrade.StudentId,
                        GradeValue = SelectedGrade.GradeValue,

                        Date = DateTime.Today

                    });
                });
                UpdateGradeCommand = new RelayCommand(
                    () =>
                    {
                        try
                        {
                            SelectedGrade.Date = SelectedDate;

                            Grades.Update(SelectedGrade);
                        }
                        catch (ArgumentException ex)
                        {

                            ErrorMessage = ex.Message;
                        }
                    });

            }
        }
    }
}
