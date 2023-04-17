using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
    /// Interaction logic for SecondGuestMyTours.xaml
    /// </summary>
    public partial class SecondGuestMyAttendedTours : Window, INotifyPropertyChanged
    {
        public Tour ChosenTour { get; set; } //
        public User Guest;                     
        private ObservableCollection<Tour> _toursReservation; 
        private TourPresenceController _tourPresenceController { get; set; }
        public UserController UserController { get; set; }
        public int GuestId { get; set; }
        public SecondGuestMyAttendedTours(int guestId)
        {
            InitializeComponent();
            this.DataContext = this;

            Guest = new User();
            UserController = new UserController();
            GuestId = guestId;
            Guest = UserController.GetByID(GuestId);
            Guest.MyTours = UserController.GetByID(GuestId).MyTours;
            _tourPresenceController = new TourPresenceController();
            _toursReservation = new ObservableCollection<Tour>(_tourPresenceController.FindAttendedTours(Guest));
            MyToursDataGrid.ItemsSource = _toursReservation;
        }
        private void Button_Rate(object sender, RoutedEventArgs e)
        {
            if (ChosenTour != null)
            {
                ToursAndGuidesEvaluationView toursAndGuidesEvaluationView = new ToursAndGuidesEvaluationView(ChosenTour);
                toursAndGuidesEvaluationView.Show();
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _location;
        public Location Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private LanguageEnum _tourLanguage;
        public LanguageEnum TourLanguage
        {
            get => _tourLanguage;
            set
            {
                if (value != _tourLanguage)
                {
                    _tourLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _durationInHours;
        public double DurationInHours
        {
            get => _durationInHours;
            set
            {
                if (value != _durationInHours)
                {
                    _durationInHours = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<DateTime> _startingTime;
        public List<DateTime> StartingTime
        {
            get => _startingTime;
            set
            {
                _startingTime = value;
                OnPropertyChanged();
            }
        }
    }
}