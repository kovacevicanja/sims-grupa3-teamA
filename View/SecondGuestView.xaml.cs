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
    public partial class SecondGuestView : Window
    {
        private TourController _tourController;
        private ObservableCollection<Tour> _tours;

        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string ChoosenLanguage { get; set; } = string.Empty;
        public string NumOfGuests { get; set; } = string.Empty;
        public Tour ChoosenTour { get; set; }  

        public SecondGuestView()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourController = new TourController();
            _tours = new ObservableCollection<Tour>(_tourController.GetAll());
            TourDataGrid.ItemsSource = _tours;

            languageComboBox.ItemsSource = new List<string>() { "ENGLISH", "SERBIAN", "GERMAN" };
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            _tourController.Search(_tours, City, Country, Duration, ChoosenLanguage, NumOfGuests);

        }

        private void Button_Click_ShowAll(object sender, RoutedEventArgs e)
        {

            _tourController.ShowAll(_tours);
        }

        private void Button_Click_Cancel_Search(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {
            if (ChoosenTour != null)
            {
                ReservationTourView reservationTourView = new ReservationTourView(ChoosenTour);
                reservationTourView.Show();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
                ReservationTourView reservationTourView = new ReservationTourView(ChoosenTour);
                reservationTourView.Show();

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

        //
        private List<DateTime> _startingTime; 

        public List <DateTime> StartingTime
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
