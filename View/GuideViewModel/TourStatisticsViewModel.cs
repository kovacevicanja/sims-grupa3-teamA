using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.Commands;

namespace BookingProject.View.GuideViewModel
{
    public class TourStatisticsViewModel
    {
        private TourTimeInstanceController _tourTimeInstanceController;
        private TourStartingTimeController _tourStartingTimeController;
        private TourReservationController _tourReservationController;
        private TourController _tourController;
        public ObservableCollection<TourTimeInstance> _instances;
        public TourTimeInstance ChosenTour { get; set; }
        public string PickedYear { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand EnterCommand { get; }
        public RelayCommand RCommand { get; }
        public RelayCommand YCommand { get; }
        public TourStatisticsViewModel(string pickedYear)
        {
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourStartingTimeController = new TourStartingTimeController();
            _tourReservationController = new TourReservationController();
            PickedYear = pickedYear;
            _tourController = new TourController();
            _instances = new ObservableCollection<TourTimeInstance>(FilterTours(TourYearFilter(_tourTimeInstanceController.GetAll())));
            TopTour = getMostVisitedTourName();
            CancelCommand = new RelayCommand(Button_Click_Close, CanExecute);
            EnterCommand = new RelayCommand(Button_Click_1, CanExecute);
            RCommand = new RelayCommand(Button_Click_2, CanExecute);
            YCommand = new RelayCommand(Button_Click_3, CanExecute);
        }
        public List<TourTimeInstance> FilterTours(List<TourTimeInstance> tours)
        {
            List<TourTimeInstance> filteredTours = new List<TourTimeInstance>();

            foreach (TourTimeInstance tour in tours)
            {
                if (tour.State == TourState.COMPLETED)
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }
        public List<TourTimeInstance> TourYearFilter(List<TourTimeInstance> tours)
        {
            List<TourTimeInstance> filteredTours = new List<TourTimeInstance>();
            if (PickedYear.Equals("all"))
            {
                return tours;
            }
            else
            {
                int pickedYear = int.Parse(PickedYear);
                foreach (TourTimeInstance tour in tours)
                {
                    if (_tourStartingTimeController.GetById(tour.DateId).StartingDateTime.Year == pickedYear)
                    {
                        filteredTours.Add(tour);
                    }
                }
                return filteredTours;
            }
        }
        public string TopTour { get; set; } = "LOREM IPSUM";
        public string getMostVisitedTourName()
        {
            string maxTourName = "Lorem Ipsum";
            int guestCount = 0;
            foreach (TourTimeInstance instance in _instances)
            {
                if (guestCount < countTourGuests(instance.TourId))
                {
                    guestCount = countTourGuests(instance.TourId);
                    maxTourName = _tourController.GetById(instance.TourId).Name;
                }
            }
            return maxTourName;
        }
        public int countTourGuests(int TourId)
        {
            int guestCount = 0;
            foreach (TourReservation reservation in _tourReservationController.GetAll())
            {
                if (reservation.Tour.Id == TourId)
                {
                    guestCount += reservation.GuestsNumberPerReservation;
                }
            }
            return guestCount;
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(TourStatisticsWindow)) { window.Close(); }
            }
        }
        private void Button_Click_3(object param)
        {
            ChangeStatsYearWindow changeStatsYearWindow = new ChangeStatsYearWindow();
            changeStatsYearWindow.Show();
            CloseWindow();
        }
        private void Button_Click_2(object param)
        {
            if (ChosenTour == null)
            {
                return;
            }
            GuestReviewsWindow guestReviewsWindow = new GuestReviewsWindow(ChosenTour);
            guestReviewsWindow.Show();
            CloseWindow();  
        }
        private void Button_Click_1(object param)
        {
            if (ChosenTour == null)
            {
                return;
            }
            SelectedTourStatsWindow selectedTourStatsWindow = new SelectedTourStatsWindow(ChosenTour);
            selectedTourStatsWindow.Show();
            CloseWindow();
        }
        private void Button_Click_Close(object param)
        {
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            CloseWindow();
        }
    }
}
