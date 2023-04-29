using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest2ViewModel
{
    public class ReservationTourOtherOffersViewModel
    {
        public ObservableCollection<Tour> Tours { get; set; }
        private TourController _tourController;
        public Tour ChosenTour { get; set; }
        public int GuestId { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand TryToBookCommand { get; }
        public ReservationTourOtherOffersViewModel(Tour chosenTour, DateTime selectedDate, int guestId)
        {
            ChosenTour = chosenTour;
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetFilteredTours(chosenTour.Location, selectedDate));
            GuestId = guestId;

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            TryToBookCommand = new RelayCommand(Button_Click_TryToBook, CanExecute);
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ReservationTourOtherOffersView)) { window.Close(); }
            }
        }

        private void Button_Click_Cancel(object param)
        {
            CloseWindow();
        }

        private void Button_Click_TryToBook(object param)
        {
            ReservationTourView reservationTourView = new ReservationTourView(ChosenTour, GuestId);
            reservationTourView.Show();
            CloseWindow(); 
        }

        private bool CanExecute(object param) { return true; }
    }
}
