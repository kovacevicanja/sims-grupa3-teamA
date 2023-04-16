using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
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
    /// Interaction logic for GuestReviewsWindow.xaml
    /// </summary>
    /// 

    public partial class GuestReviewsWindow : Window
    {

        private TourTimeInstanceController _tourTimeInstanceController;
        private TourStartingTimeController _tourStartingTimeController;
        private TourReservationController _tourReservationController;
        private TourController _tourController;
        private TourEvaluationController _tourEvaluationController;
        private ObservableCollection<TourEvaluation> _grades;
        public TourTimeInstance ChosenTour { get; set; }
        public TourEvaluation ChosenEvaluation { get; set; }

        public GuestReviewsWindow(TourTimeInstance chosenTour)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourStartingTimeController = new TourStartingTimeController();
            _tourReservationController = new TourReservationController();
            _tourEvaluationController= new TourEvaluationController();
            _tourController = new TourController();
            ChosenTour = chosenTour;
            _grades = new ObservableCollection<TourEvaluation>(FilterGrades(_tourEvaluationController.GetAll()));
            TourDataGrid.ItemsSource = _grades;
        }

        public List<TourEvaluation> FilterGrades(List<TourEvaluation> grades)
        {
            List<TourEvaluation> filteredGrades = new List<TourEvaluation>();

            foreach (TourEvaluation grade in grades)
            {
                if (grade.TourReservation.Tour.Id == ChosenTour.TourId)
                {
                    filteredGrades.Add(grade);
                }
            }
            return filteredGrades;
        }


        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow("all");
            tourStatisticsWindow.Show();
            Close();
        }

        private void Button_Click_Examine(object sender, RoutedEventArgs e)
        {
            ExamineGradeWindow examineGradeWindow = new ExamineGradeWindow(ChosenEvaluation, ChosenTour);
            examineGradeWindow.Show();
            Close();
        }
    }
}
