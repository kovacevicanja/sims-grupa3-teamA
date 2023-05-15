using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class GuestReviewsViewModel
    {
        private TourEvaluationController _tourEvaluationController;
        public ObservableCollection<TourEvaluation> _grades;
        public TourTimeInstance ChosenTour { get; set; }
        public TourEvaluation ChosenEvaluation { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand EnterCommand { get; }

        public GuestReviewsViewModel(TourTimeInstance chosenTour)
        {

            _tourEvaluationController = new TourEvaluationController();
            ChosenTour = chosenTour;
            _grades = new ObservableCollection<TourEvaluation>(FilterGrades(_tourEvaluationController.GetAll()));
            CancelCommand = new RelayCommand(Button_Click_Close, CanExecute);
            EnterCommand = new RelayCommand(Button_Click_Examine, CanExecute);

        }
        public List<TourEvaluation> FilterGrades(List<TourEvaluation> grades)
        {
            List<TourEvaluation> filteredGrades = new List<TourEvaluation>();
            foreach (TourEvaluation grade in grades)
            {
                if (grade.Tour.Id == ChosenTour.TourId)
                {
                    filteredGrades.Add(grade);
                }
            }
            return filteredGrades;
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(GuestReviewsWindow)) { window.Close(); }
            }
        }
        private void Button_Click_Close(object param)
        {
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow("all");
            tourStatisticsWindow.Show();
            CloseWindow();
        }
        private void Button_Click_Examine(object param)
        {
            if (ChosenEvaluation == null)
            {
                return;
            }
            ExamineGradeWindow examineGradeWindow = new ExamineGradeWindow(ChosenEvaluation, ChosenTour);
            examineGradeWindow.Show();
            CloseWindow();
        }
    }
}
