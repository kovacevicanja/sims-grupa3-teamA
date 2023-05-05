using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for ExamineGradeWindow.xaml
    /// </summary>
    public partial class ExamineGradeWindow : Window
    {
        public TourEvaluation ChosenEvaluation { get; set; }
        public TourTimeInstance ChosenTour { get; set; }
        public string GuestKeyPoint { get; set; } = "LOREM IPSUM";
        public TourEvaluationController _tourEvaluationController { get; set; }
        public TourPresenceController _tourPresenceController { get; set; }
        public KeyPointController _keyPointController { get; set; }
        public ExamineGradeWindow(TourEvaluation chosenEvaluation, TourTimeInstance chosenTour)
        {
            InitializeComponent();
            this.DataContext = this;
            ChosenEvaluation = chosenEvaluation;
            ChosenTour = chosenTour;
            _tourEvaluationController = new TourEvaluationController();
            _tourPresenceController = new TourPresenceController();
            _keyPointController = new KeyPointController();
            GuestKeyPoint = GetGuestKeyPoint();
        }
        public void MarkAsInvalid()
        {
            _tourEvaluationController.GetById(ChosenEvaluation.Id).IsValid = false;
            _tourEvaluationController.Save();
        }
        public string GetGuestKeyPoint()
        {
            foreach(TourPresence presence in _tourPresenceController.GetAll())
            {
                if(presence.UserId==ChosenEvaluation.Guest.Id && ChosenTour.Id == presence.TourId)
                {
                   return _keyPointController.GetById(presence.KeyPointId).Point;
                }
            }
            return "LOREM IPSUM";
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            GuestReviewsWindow guestReviewsWindow = new GuestReviewsWindow(ChosenTour);
            guestReviewsWindow.Show();
            Close();
        }
        private void Button_Click_Mark(object sender, RoutedEventArgs e)
        {
            MarkAsInvalid();
            GuestReviewsWindow guestReviewsWindow = new GuestReviewsWindow(ChosenTour);
            guestReviewsWindow.Show();
            Close();
        }
    }
}
