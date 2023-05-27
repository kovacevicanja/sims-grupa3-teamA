using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.OwnersView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class WizardAddAccommodationViewModel
    {
        public RelayCommand AddImageCommand { get; }
        public NavigationService NavigationService { get; set; }
        public AccommodationType chosenType { get; set; }
        public ObservableCollection<AccommodationType> accommodationTypes { get; set; }
        public AccommodationLocationController LocationController { get; set; }

        public WizardAddAccommodationViewModel(NavigationService navigationService )
        {
            var types = Enum.GetValues(typeof(AccommodationType)).Cast<AccommodationType>();
            accommodationTypes = new ObservableCollection<AccommodationType>(types);
            NavigationService = navigationService;
            AddImageCommand = new RelayCommand(Button_Click_Add_Image, CanExecute);
            LocationController = new AccommodationLocationController();
        }
        private bool CanExecute(object param) { return true; }

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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_Add_Image(object param)
        {
            Accommodation accommodation = new Accommodation();
            accommodation.AccommodationName = AccommodationName;
            accommodation.Type = chosenType;
            accommodation.MaxGuestNumber = MaxGuestNumber;
            accommodation.MinDays = MinDays;
            accommodation.CancellationPeriod = CancellationPeriod;
            accommodation.Owner.Id = SignInForm.LoggedInUser.Id;

            Location location = new Location();
            location.City = City;
            location.Country = Country;

            LocationController.Create(location);
            LocationController.SaveLocation();
            accommodation.Location = location;
            accommodation.IdLocation = location.Id;

            NavigationService.Navigate(new WizardAddImageView(accommodation, NavigationService));

            //AccommodationController.Create(accommodation);
            //AccommodationController.SaveAccommodation();

        }
    }
}
