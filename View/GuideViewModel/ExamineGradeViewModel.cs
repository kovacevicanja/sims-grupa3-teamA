using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class ExamineGradeViewModel
    {
        public TourEvaluation ChosenEvaluation { get; set; }
        public TourTimeInstance ChosenTour { get; set; }
        public string GuestKeyPoint { get; set; } = "LOREM IPSUM";
        public TourEvaluationController _tourEvaluationController { get; set; }
        public TourPresenceController _tourPresenceController { get; set; }
        public KeyPointController _keyPointController { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand MarkCommand { get; }
        public ExamineGradeViewModel(TourEvaluation chosenEvaluation, TourTimeInstance chosenTour)
        {
            ChosenEvaluation = chosenEvaluation;
            ChosenTour = chosenTour;
            _tourEvaluationController = new TourEvaluationController();
            _tourPresenceController = new TourPresenceController();
            _keyPointController = new KeyPointController();
            GuestKeyPoint = GetGuestKeyPoint();

            CancelCommand = new RelayCommand(Button_Click_Close, CanExecute);
            MarkCommand = new RelayCommand(Button_Click_Mark, CanExecute);
        }
        public void MarkAsInvalid()
        {
            _tourEvaluationController.GetById(ChosenEvaluation.Id).IsValid = false;
            _tourEvaluationController.Save();
        }
        public string GetGuestKeyPoint()
        {
            foreach (TourPresence presence in _tourPresenceController.GetAll())
            {
                if (presence.UserId == ChosenEvaluation.Guest.Id && ChosenTour.Id == presence.TourId)
                {
                    return _keyPointController.GetById(presence.KeyPointId).Point;
                }
            }
            return "LOREM IPSUM";
        }

        private bool CanExecute(object param) { return true; }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ExamineGradeWindow)) { window.Close(); }
            }
        }
        private void Button_Click_Close(object param)
        {
            GuestReviewsWindow guestReviewsWindow = new GuestReviewsWindow(ChosenTour);
            guestReviewsWindow.Show();
            CloseWindow();
        }
        private void Button_Click_Mark(object param)
        {
            MarkAsInvalid();
            GuestReviewsWindow guestReviewsWindow = new GuestReviewsWindow(ChosenTour);
            guestReviewsWindow.Show();
            CloseWindow();
        }
    }
}
