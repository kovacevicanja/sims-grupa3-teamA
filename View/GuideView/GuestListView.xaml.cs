using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Serialization;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuestListView.xaml
    /// </summary>
    public partial class GuestListView : Window
    {




        public bool IsValid { get; set; }
        public User ChosenGuest { get; set; }

        private UserController _userController;
        private ObservableCollection<User> _guests;
        private KeyPointController _keyPointController;
        private TourReservationController _tourReservationController;


        public KeyPoint ChosenKeyPoint { get; set; }
        public TourTimeInstance ChosenTour { get; set; }

        public GuestListView(TourTimeInstance chosenTour, KeyPoint chosenKeyPoint)
        {
            InitializeComponent();
            this.DataContext = this;
            _userController = new UserController();
            _keyPointController = new KeyPointController();
            _tourReservationController= new TourReservationController();
            ChosenTour = chosenTour;
            ChosenKeyPoint = chosenKeyPoint;
            _guests = new ObservableCollection<User>(filterGuests(_tourReservationController.GetAll()));
            GuestDataGrid.ItemsSource = _guests;
        }

        public List<User> filterGuests(List<TourReservation> reservations)
        {
            List<User> users= new List<User>();
            foreach(TourReservation reservation in reservations)
            {
                if (reservation.Tour.Id == ChosenTour.Id)
                {
                    users.Add(reservation.Guest);
                }
            }
            return users;
        } 

        private void Button_Click_Mark(object sender, RoutedEventArgs e)
        {
            if (ChosenGuest != null)
            {
                GuestPreseanceCheck guestPreseanceCheck = new GuestPreseanceCheck(ChosenTour, ChosenKeyPoint, ChosenGuest);
                guestPreseanceCheck.Show();
            
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Cancell(object sender, RoutedEventArgs e)
        {
            LiveTourView liveTourView = new LiveTourView(ChosenTour);
            liveTourView.Show();
            Close();

        }


    }

}
