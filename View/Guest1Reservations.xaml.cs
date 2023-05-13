using BookingProject.Controller;
using BookingProject.Model;
using OisisiProjekat.Observer;
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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for Guest1Reservations.xaml
    /// </summary>
    public partial class Guest1Reservations : Window, IObserver
    {
        public ObservableCollection<AccommodationReservation> _reservations;
        public AccommodationReservationController _accommodationReservationController;
        public AccommodationReservation SelectedReservation { get; set; }
        public UserController _userController { get; set; }

        public Guest1Reservations()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationReservationController = new AccommodationReservationController();
            _userController = new UserController();
            _reservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.getReservationsForGuest(_userController.GetLoggedUser()));
            //_accommodationReservationController.Subscribe(this);
            ReservationsDataGrid.ItemsSource = _reservations;
        }

        private void Button_Click_Review(object sender, RoutedEventArgs e)
        {
            if (_accommodationReservationController.PermissionToRate(SelectedReservation)) {
                AccommodationOwnerReview aor = new AccommodationOwnerReview(SelectedReservation);
                aor.Show();
            }
            else
            {
                MessageBox.Show("You don't have permission to review this accommodation and owner!");
            }
            this.Close();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
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

        private void Button_Click_Reschedule(object sender, RoutedEventArgs e)
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
            this.Close();
        }

        public void Update()
        {
            _reservations.Clear();
            foreach(AccommodationReservation reservation in _accommodationReservationController.getReservationsForGuest(_userController.GetLoggedUser()))
            {
                _reservations.Add(reservation);
            }
        }

        private void Button_Click_See_Requests(object sender, RoutedEventArgs e)
        {
            var reqAccView = new AccommodationRequestsView();
            reqAccView.Show();
            this.Close();
        }

        private void Button_Click_Homepage(object sender, RoutedEventArgs e)
        {
            var Guest1Homepage = new Guest1Homepage();
            Guest1Homepage.Show();
            this.Close();
        }

        private void Button_Click_MyReservations(object sender, RoutedEventArgs e)
        {
            var Guest1Reservations = new Guest1Reservations();
            Guest1Reservations.Show();
            this.Close();
        }

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }
    }
}