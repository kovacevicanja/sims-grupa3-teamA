using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
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
    /// Interaction logic for CreateTourRequestView.xaml
    /// </summary>
    public partial class CreateTourRequestView : Window
    {
        public ObservableCollection<LanguageEnum> Languages { get; set; }
        public LanguageEnum ChosenLanguage { get; set; }
        public TourLocationController _tourLocationController { get; set; }
        public TourRequestController _tourRequestController { get; set; }
        public UserController _userController { get; set; }

        public CreateTourRequestView()
        {
            InitializeComponent();
            this.DataContext = this;

            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

            _tourRequestController = new TourRequestController();
            _tourLocationController = new TourLocationController();
            _userController = new UserController();

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
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
        private int _guestsNumber;
        public int GuestsNumber
        {
            get => _guestsNumber;
            set
            {
                if (value != _guestsNumber)
                {
                    _guestsNumber = value;
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

        private void Button_Click_CreateTourRequest(object sender, RoutedEventArgs e)
        {
            TourRequest tourRequest = new TourRequest();

            Location location = new Location();
            location.City = City;
            location.Country = Country;
            _tourLocationController.Create(location);
            tourRequest.Location = location;

            tourRequest.Description = Description;
            tourRequest.StartDate = StartDate;
            tourRequest.EndDate = EndDate;
            tourRequest.Language = ChosenLanguage;
            tourRequest.GuestsNumber = GuestsNumber;
            tourRequest.Status = Domain.Enums.TourRequestStatus.PENDING;

            _tourRequestController.Create(tourRequest);
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e) 
        {
            this.Close();
        }   
    }
}