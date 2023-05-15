using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.View.Guest1View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest1ViewModel
{
    public class AccommodationRequestsViewModel
    {
        public ObservableCollection<RequestAccommodationReservation> MyRequests { get; set; }
        public RequestAccommodationReservationController requestAccommodationReservationController;
        public UserController userController;
        public RelayCommand HomepageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogoutCommand { get; }
        public AccommodationRequestsViewModel()
        {
            requestAccommodationReservationController = new RequestAccommodationReservationController();
            userController = new UserController();
            MyRequests = new ObservableCollection<RequestAccommodationReservation>(requestAccommodationReservationController.GetAllForUser(userController.GetLoggedUser()));
            HomepageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogoutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AccommodationRequestsView)) { window.Close(); }
            }
        }
        private void Button_Click_Homepage(object param)
        {
            var Guest1Homepage = new Guest1HomepageView();
            Guest1Homepage.Show();
            CloseWindow();
        }

        private void Button_Click_MyReservations(object param)
        {
            var Guest1Reservations = new Guest1Reservations();
            Guest1Reservations.Show();
            CloseWindow();
        }

        private void Button_Click_Logout(object param)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }
    }
}
