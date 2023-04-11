using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.FileHandler;
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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestProfile.xaml
    /// </summary>
    public partial class SecondGuestProfile : Window
    {
        private int GuestId { get; set; }
        private Guest2Controller Guest2Controller { get; set; } 
        public Guest2 Guest { get; set; }
        public UserController UserController { get; set; }  
        public User User { get; set; }  
        public TourReservationController TourReservationController { get; set; }
        public TourReservationHandler TourReservationHandler { get; set; }
        public SecondGuestProfile(int idGuest)
        {
            InitializeComponent();
            this.DataContext = this;
           
            this.GuestId = idGuest;
            //Guest2Controller = new Guest2Controller();
            UserController = new UserController();
            User = new User();
            TourReservationController = new TourReservationController();
            TourReservationHandler = new TourReservationHandler();
        }

        private void Button_Click_MyTours(object sender, RoutedEventArgs e)
        {
            SecondGuestMyTours secondGuestMyTours = new SecondGuestMyTours(GuestId);
            secondGuestMyTours.Show();
        }

        private void Button_Click_SecondGuestView(object sender, RoutedEventArgs e)
        {
            SecondGuestView secondGuestView = new SecondGuestView(GuestId);
            secondGuestView.Show();
        }

        private void Button_Click_MyVouchers (object sender, RoutedEventArgs e)
        {
            SecondGuestMyVouchersView secondGuestMyVouchers = new SecondGuestMyVouchersView(GuestId);
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
            //prolazim kroz listu trenutno aktivnih tura
            List<TourReservation> tourReservations = new List<TourReservation>();
            tourReservations = TourReservationHandler.Load();
            int flag = 0;
            List<TourReservation> activeTours = new List<TourReservation>(); 
            List<int> activeToursIds = new List<int>();

            foreach (TourReservation tr in tourReservations)
            {
                if (GuestId == tr.Guest.Id && tr.ReservationStartingTime.Date == DateTime.Now.Date)
                {
                    flag = 1;
                    //MonitoringActiveToursView monitoringActiveTours = new MonitoringActiveToursView(tr);
                    //monitoringActiveTours.ShowDialog();
                    //prenosim rez ture koja je trenutno aktivna za tog gosta
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
