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
    public class ReservationTourViewModel
    {
        public string EnteredGuests { get; set; } = string.Empty;
        public TourReservationController _tourReservationController { get; set; }
        public TourDateTime SelectedDate { get; set; }
        public Tour ChosenTour { get; set; }
        public int GuestId { get; set; }
        public User User { get; set; }
        public UserController UserController { get; set; }
        public RelayCommand TryToBookCommand { get; }
        public RelayCommand CancelCommand { get; }

        public ReservationTourViewModel(Tour chosenTour, int guestId)
        {
            _tourReservationController = new TourReservationController();
            ChosenTour = chosenTour;
            GuestId = guestId;
            UserController = new UserController();
            User = new User();
            User.Id = GuestId;

            TryToBookCommand = new RelayCommand(Button_Click_TryToBook, CanExecute);
            CancelCommand = new RelayCommand(Button_Click_Close, CanExecute);
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Click_TryToBook(object param)
        {
            _tourReservationController.TryToBook(ChosenTour, EnteredGuests, SelectedDate.StartingDateTime, User);
        }
        
        private void Button_Click_Close(object param)
        {
            CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ReservationTourView)) { window.Close(); }
            }
        }
    }
}