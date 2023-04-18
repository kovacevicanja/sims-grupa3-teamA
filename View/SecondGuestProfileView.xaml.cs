using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
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
    /// Interaction logic for SecondGuestProfile.xaml
    /// </summary>
    public partial class SecondGuestProfile : Window
    {
        private int GuestId { get; set; }
        public UserController UserController { get; set; }
        public User User { get; set; }
        public Tour Tour { get; set; }
        public TourReservationController TourReservationController { get; set; }
        //public TourReservationHandler TourReservationHandler { get; set; }
        public ITourReservationRepository _tourReservationRepository;
        public TourReservation TourReservation { get; set; }    
        public SecondGuestProfile(int idGuest)
        {
            InitializeComponent();
            this.DataContext = this;

            this.GuestId = idGuest;

            UserController = new UserController();
            User = new User();
            Tour = new Tour();
            TourReservation = new TourReservation();
            TourReservationController = new TourReservationController();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
        }
        private void Button_Click_MyAttendedTours(object sender, RoutedEventArgs e)
        {
            SecondGuestMyAttendedTours secondGuestMyAttendedTours = new SecondGuestMyAttendedTours(GuestId);
            secondGuestMyAttendedTours.Show();
        }
        private void Button_Click_MyReservations(object sender, RoutedEventArgs e)
        {
            SecondGuestMyReservations secondGuestMyReservations = new SecondGuestMyReservations(GuestId);
            secondGuestMyReservations.Show();
        }

        private void Button_Click_SerachAndReservationTours(object sender, RoutedEventArgs e)
        {
            SecondGuestSerachAndReservationTours secondGuestSerachAndReservationTours = new SecondGuestSerachAndReservationTours(GuestId);
            secondGuestSerachAndReservationTours.Show();
        }

        private void Button_Click_MyVouchers (object sender, RoutedEventArgs e)
        {
            SecondGuestMyVouchersView secondGuestMyVouchers = new SecondGuestMyVouchersView(GuestId, Tour);
            secondGuestMyVouchers.ShowDialog();
        }

        private void Button_Click_LogOut(object sender, RoutedEventArgs e)
        {
            UserController.GetByID(GuestId);
            User.Id = GuestId;
            User.IsLoggedIn = false;
            SignInForm signInForm = new SignInForm();
            signInForm.ShowDialog();
        }
        private void Button_Click_MonitoringActiveTours(object sender, RoutedEventArgs e)
        {
            List<TourReservation> tourReservations = new List<TourReservation>();
            int flag = 0;
            List<TourReservation> activeTours = new List<TourReservation>(); 
            List<int> activeToursIds = new List<int>();

            foreach (TourReservation tr in tourReservations)
            {
                if (GuestId == tr.Guest.Id && tr.ReservationStartingTime.Date == DateTime.Now.Date)
                {
                    flag = 1;
                    activeTours.Add(tr);
                    activeToursIds.Add(tr.Tour.Id);
                }
            }
            if (flag != 1)
            {
                MessageBox.Show("There are currently no active tours that you can follow.");
            }
            else
            {
                ActiveToursView activeToursView = new ActiveToursView(activeToursIds);
                activeToursView.ShowDialog();
            }
        }
    }
}
