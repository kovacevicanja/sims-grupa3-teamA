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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for TourCreationView.xaml
    /// </summary>
    public partial class TourCreationView : Window, IDataErrorInfo
    {

        public ObservableCollection<LanguageEnum> Languages { get; set; }
        public TourController TourController { get; set; }

        public LocationController LocationController { get; set; }
        public LanguageEnum ChosenLanguage { get; set; }

        public TourCreationView()
        {
            InitializeComponent();
            this.DataContext = this;
            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

            var app = Application.Current as App;
            TourController = app.TourController;
            LocationController = app.LocationController;
        }

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

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


        private string _tourLanguage;
        public string TourLanguage
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


        private double _duration;
        public double Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city;

        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }







        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void Button_Click_Kreiraj(object sender, RoutedEventArgs e)
        {
            Tour tour = new Tour();
            tour.Name = TourName;
            tour.Description = Description;
            tour.MaxGuests = MaxGuests;
            tour.DurationInHours = Duration;
            tour.Language = ChosenLanguage;

            Location location= new Location();
            location.City= City;
            location.Country= Country;

            LocationController.Create(location);
            LocationController.Save();

            tour.LocationId = location.Id;

            TourController.Create(tour);
            TourController.Save();


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
