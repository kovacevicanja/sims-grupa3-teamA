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

namespace BookingProject.View.Guest1ViewModel
{
    public class Guest1ReservationsViewModel
    {
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservationController _accommodationReservationController;
        public AccommodationReservation SelectedReservation { get; set; }
        public UserController _userController { get; set; }
        public RelayCommand ReviewCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand RescheduleCommand { get; }
        public RelayCommand SeeRequestsCommand { get; }
        public RelayCommand HomepageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogoutCommand { get; }

        public Guest1ReservationsViewModel()
        {
            _accommodationReservationController = new AccommodationReservationController();
            _userController = new UserController();
            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.getReservationsForGuest(_userController.GetLoggedUser()));
            //_accommodationReservationController.Subscribe(this);
            // ReservationsDataGrid.ItemsSource = _reservations;
            ReviewCommand = new RelayCommand(Button_Click_Review, CanExecute);
            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            RescheduleCommand = new RelayCommand(Button_Click_Reschedule, CanExecute);
            SeeRequestsCommand = new RelayCommand(Button_Click_See_Requests, CanExecute);
            HomepageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogoutCommand = new RelayCommand(Button_Click_Logout, CanExecute);

        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1Reservations)) { window.Close(); }
            }
        }

        private void Button_Click_Review(object param)
        {
            if (_accommodationReservationController.PermissionToRate(SelectedReservation))
            {
                AccommodationOwnerReview aor = new AccommodationOwnerReview(SelectedReservation);
                aor.Show();
            }
            else
            {
                MessageBox.Show("You don't have permission to review this accommodation and owner!");
            }
            CloseWindow();
        }

        private void Button_Click_Cancel(object param)
        {
            if (_accommodationReservationController.PermissionToCancel(SelectedReservation))
            {
                MessageBox.Show("You have successfully cancelled your reservation!");
            }
            else
            {
                MessageBox.Show("You have failed to cancel your reservation because you have not met the accommodation owner's requirements!");
            }
        }

        private void Button_Click_Reschedule(object param)
        {
            DateTime today = DateTime.Now.Date;
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);
            if (SelectedReservation.InitialDate > todayMidnight)
            {
                var RescheduleAccommodationReservation = new RescheduleAccommodationReservationView(SelectedReservation);
                RescheduleAccommodationReservation.Show();
            }
            else
            {
                MessageBox.Show("You can't reschedule finished reservations!");
            }
            CloseWindow();
        }

        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in _accommodationReservationController.getReservationsForGuest(_userController.GetLoggedUser()))
            {
                Reservations.Add(reservation);
            }
        }

        private void Button_Click_See_Requests(object param)
        {
            var reqAccView = new AccommodationRequestsView();
            reqAccView.Show();
            CloseWindow();
        }

        private void Button_Click_Homepage(object param)
        {
            var Guest1Homepage = new Guest1Homepage();
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
