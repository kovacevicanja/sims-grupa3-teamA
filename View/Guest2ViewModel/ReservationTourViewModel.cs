using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Windows;
using System.Windows.Navigation;

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
        public CustomMessageBox CustomMessageBox { get; set; }
        public NavigationService NavigationService { get; set; }

        public ReservationTourViewModel(Tour chosenTour, int guestId, NavigationService navigationService)
        {
            _tourReservationController = new TourReservationController();
            ChosenTour = chosenTour;
            GuestId = guestId;
            UserController = new UserController();
            User = new User();
            User.Id = GuestId;

            TryToBookCommand = new RelayCommand(Button_Click_TryToBook, CanExecute);
            CancelCommand = new RelayCommand(Button_Click_Close, CanExecute);

            CustomMessageBox = new CustomMessageBox();

            NavigationService = navigationService;
        }
        private bool CanExecute(object param) { return true; }

        private void Button_Click_TryToBook(object param)
        {
            try
            {
                if (SelectedDate.StartingDateTime == null || int.Parse(EnteredGuests) <= 0 || EnteredGuests.IsEmpty()) { }
                _tourReservationController.TryToBook(ChosenTour, EnteredGuests, SelectedDate.StartingDateTime, User, NavigationService);
            }
            catch
            {
                CustomMessageBox.ShowCustomMessageBox("You must fill in the fields correctly or select one of the offered dates, otherwise you cannot make a reservation.");
                return;
            }
        }

        private void Button_Click_Close(object param)
        {
            NavigationService.GoBack();
        }
    }
}