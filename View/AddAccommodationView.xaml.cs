using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
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
    /// Interaction logic for AddAccommodationView.xaml
    /// </summary>
    public partial class AddAccommodationView : Window, IDataErrorInfo
    {
        public Accommodation accommodation { get; set; }
        public ObservableCollection<AccommodationType> accommodationTypes { get; set; }
        public AccommodationType chosenType { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public AccommodationLocationController LocationController { get; set; }
        public AccommodationImageController ImageController { get; set; }
        public ObservableCollection<AccommodationImage> Images { get; set; }
        public AccommodationImage AccommodationImage { get; set; }
        
        public AddAccommodationView()
        {
            InitializeComponent();
            this.DataContext= this;
            var types = Enum.GetValues(typeof(AccommodationType)).Cast<AccommodationType>();
            accommodationTypes = new ObservableCollection<AccommodationType>(types);

            var app = Application.Current as App;
            AccommodationController = app.AccommodationController;
            LocationController = app.AccommodationLocationController;
            ImageController= app.AccommodationImageController;
        }

        private string _accommodationName;

        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _accommodationCity;

        public string City
        {
            get => _accommodationCity;
            set
            {
                if (value != _accommodationCity)
                {
                    _accommodationCity = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _accommodationCountry;

        public string Country
        {
            get => _accommodationCountry;
            set
            {
                if (value != _accommodationCountry)
                {
                    _accommodationCountry = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _accommodationType;
        public int TypeOfAccommodation
        {
            get => _accommodationType;
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuestNumber;
        public int MaxGuestNumber
        {
            get => _maxGuestNumber;
            set
            {
                if (value != _maxGuestNumber)
                {
                    _maxGuestNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _minNumberOfDays;
        public int MinDays
        {
            get => _minNumberOfDays;
            set
            {
                if (value != _minNumberOfDays)
                {
                    _minNumberOfDays = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _cancellationPeriod;
        public int CancellationPeriod
        {
            get => _cancellationPeriod;
            set
            {
                if (value != _cancellationPeriod)
                {
                    _cancellationPeriod = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _url;

        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Accommodation accommodation = new Accommodation();
            accommodation.AccommodationName = AccommodationName;
            accommodation.Type = chosenType;
            accommodation.MaxGuestNumber = MaxGuestNumber;
            accommodation.MinDays = MinDays;
            accommodation.CancellationPeriod = CancellationPeriod;

            Location location = new Location();
            location.City = City;
            location.Country = Country;

            LocationController.Create(location);
            //LocationController.SaveLocation();
            accommodation.IdLocation = location.Id;

            AccommodationController.Create(accommodation);
            //AccommodationController.SaveAccommodation();

            ImageController.LinkToAccommodation(accommodation.Id);
            //ImageController.SaveImage();

            if (IsValid)
            {
                AccommodationController.Create(accommodation);
            }
            this.Close();
        }
        private void Button_Click_Cancel(Object sender, RoutedEventArgs e)
        {
            ImageController.Load();
            ImageController.DeleteUnused();
            ImageController.SaveImage();
            this.Close();
        }
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        private void Button_Click_Add_Image(object sender, RoutedEventArgs e)
        {
            AddPhotosToAccommodationView addPhoto = new AddPhotosToAccommodationView();
            addPhoto.Show();
        }
        public string this[string columnName]
        {
            get
            {
                if (columnName == "AccommodationName")
                {
                    if (string.IsNullOrEmpty(AccommodationName))
                        return "You must enter accommodation name!";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "You must enter accommodation city!";
                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "You must enter accommodation country!";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "AccommodationName", "City", "Country" };

        public string Error => null;
    }
}