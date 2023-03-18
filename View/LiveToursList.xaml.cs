using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using Microsoft.VisualBasic.FileIO;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestView.xaml
    /// </summary>
    public partial class LiveToursList : Window
    {
        private TourController _tourController;
        private ObservableCollection<Tour> _tours;

        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string ChoosenLanguage { get; set; } = string.Empty;
        public string NumOfGuests { get; set; } = string.Empty;

        public Tour ChosenTour { get; set; }



        public LiveToursList()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourController = new TourController();

            _tours = new ObservableCollection<Tour>(FilterTours(_tourController.GetAll()));
            TourDataGrid.ItemsSource = _tours;

        }

        public List<Tour> FilterTours(List<Tour> tours)
        {
            List<Tour> filteredTours= new List<Tour>();
            foreach(Tour tour in tours)
            {
                if (TodayCheck(tour))
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }

        public bool TodayCheck(Tour tour)
        {
            foreach(TourDateTime tourDate in tour.StartingTime)
            {
                if(tourDate.StartingDateTime.Date == DateTime.Now.Date)
                {
                    return true;
                }
            }

            return false;
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {

            TourCreationView tourCreationView = new TourCreationView();
            tourCreationView.Show();
            Close();
        }



        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            if (ChosenTour != null)
            {
                LiveTourView liveTourView = new LiveTourView(ChosenTour);
                liveTourView.Show();
            }
        }

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
    }
}

