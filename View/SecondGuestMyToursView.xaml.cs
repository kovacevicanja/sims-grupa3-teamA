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
    public partial class SecondGuestMyTours : Window, INotifyPropertyChanged
    {
        private Guest2Controller _guest2Controller;
        private ToursGuestsController _toursGuestsController;
        private ObservableCollection<Guest2> _guests2;
        private ObservableCollection<ToursGuests> _toursGuests;
        public ObservableCollection<Tour> MyToursWpf { get; set; }
        private ObservableCollection<Tour> _tours;

        private TourController _tourController;

        public TourReservation ChoosenReservation { get; set; } //

        private Guest2 _selectedGuest;
        public Guest2 Guest { get; set; }

        private List<Object> tourGuestsGrid;
        private readonly TourReservationHandler _reservationHandler;

        private ObservableCollection<TourReservation> _reservations;

        public TourReservationController TourReservationController { get; set; }

        private ObservableCollection<TourReservation> tourCollection = new ObservableCollection<TourReservation>(); //

        private TourReservationController _tourReservationController; //
        private ObservableCollection<TourReservation> _toursReservation; //

        public int GuestId { get; set; }

        public SecondGuestMyTours(int guestId)
        {
            InitializeComponent();
            this.DataContext = this;

            _reservationHandler = new TourReservationHandler();
            //Guest = guest;

            GuestId = guestId;

            TourReservationController = new TourReservationController();
            _reservations = new ObservableCollection<TourReservation>(TourReservationController.GetAll());

            //TourReservationController.ReservationGuestBind(guest.Id);

            _guest2Controller = new Guest2Controller();
            //_guests2 = new ObservableCollection<Guest2>(_guest2Controller.GetAll());
            //MyToursDataGrid.ItemsSource = _guests2;
            tourGuestsGrid = new List<Object>();

            //_toursGuestsController = new ToursGuestsController();
            //_toursGuestsController.Load();
            //_toursGuests = new ObservableCollection<ToursGuests>(_toursGuestsController.GetAll());

            //
            _tourReservationController = new TourReservationController();
            //_toursReservation = new ObservableCollection<TourReservation>(_tourReservationController.GetAll());
            _toursReservation = new ObservableCollection<TourReservation>(_tourReservationController.GetUserTours(guestId));
            //_tourReservationController.ReservationGuestBind(guestId);
            MyToursDataGrid.ItemsSource = _toursReservation;
            //


            //MyToursDataGrid.ItemsSource = tourCollection;

            //Guest = _guest2Controller.GetByID(guestId);

            //_tourController = new TourController();
            //_tourController.Load();
            //_tours = new ObservableCollection<Tour>(_tourController.GetAll());

            /*
            foreach (ToursGuests tg in _toursGuests) 
            { 
                foreach (Tour tour in _tours)
                {
                    if (tg.Tour.Id == tour.Id)
                    {
                        MyToursDataGrid.Add()
                    }
                }
            }
            */


            //MyToursDataGrid.ItemsSource = _toursGuests;


            //fillDataGrid(_toursGuests);
            //fillDataGrid();
            //MyToursDataGrid.ItemsSource = tourGuestsGrid;

            //MyToursWpf = new ObservableCollection<Tour>(guest.MyTours);
            //CollectionViewSource.GetDefaultView(MyToursWpf).Refresh();

        }

        /*
        private void MyToursDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyToursDataGrid.SelectedItem == null)
            {
                MyToursWpf.Clear();
                return;
            }

            _selectedGuest = MyToursDataGrid.SelectedItem as Guest2;

            if (_selectedGuest != null)
            {
                MyToursWpf.Clear();
                foreach (var tour in _selectedGuest.MyTours)
                {
                    MyToursWpf.Add(tour);
                }
            }
        }
        */
        private void Button_Rate(object sender, RoutedEventArgs e)
        {
            if (ChoosenReservation != null)
            {
                ToursAndGuidesEvaluationView toursAndGuidesEvaluationView = new ToursAndGuidesEvaluationView(ChoosenReservation);
                toursAndGuidesEvaluationView.Show();
            }

        }

        /*public void SetToursAndGuests()
        {
            TourReservationController.Load();
            foreach (ToursGuests tg in _toursGuests)
            {
                TourReservation tour = TourReservationController.GetByID(tg.TourReservation.Id);
                tg.TourReservation = tour;
            }

            GuestController.Load();
            foreach (ToursGuests tg in _toursGuests)
            {
                Guest2 guest = GuestController.GetByID(tg.Guest.Id);
                tg.Guest = guest;
            }
        }*/

        /*private void fillDataGrid()
        {
            //List<TourReservation> reservationsFromFile = _reservationHandler.Load();
            //TourReservationController.Load();
            //List <TourReservation> reservationsFromFile = TourReservationController.GetAll();

            /*if (_reservations.Count() != 0)
            {
                foreach (TourReservation tr in _reservations)
                {
                    foreach (Guest2 g in tr.Guests)
                    {
                        if (g.Id == Guest.Id)
                        {
                            Guest.MyTours.Add(tr);
                        }
                    }
                }
            }*/

            /*
            if (_reservations.Count == 0)
            {
                MessageBox.Show("nemate ni jednu rezervaciju");
            }

            List<TourReservation> toursList = new List<TourReservation>();
            toursList = Guest.MyTours;

            foreach (TourReservation reservation in _reservations)
            {
                if (reservation.Guest.Id == Guest.Id)
                {
                    toursList.Add(reservation);
                }
            }

            foreach (TourReservation tour in toursList)
            {
                tourGuestsGrid.Add(new
                {
                    TourName = tour.Tour.Name,
                    TourCity = tour.Tour.Location.City,
                    TourCountry = tour.Tour.Location.Country,
                    TourDescription = tour.Tour.Description,
                    TourDuration = tour.Tour.DurationInHours,
                    TourKeyPoints = tour.Tour.KeyPoints,
                    TourLanguage = tour.Tour.Language,
                    TourStartingTime = tour.ReservationStartingTime,
                    TourMaxGuests = tour.Tour.MaxGuests,
                    TourImages = tour.Tour.Images
                });
            }
            */

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
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

        private LanguageEnum _language;
        public LanguageEnum Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
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