using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using BookingProject.Commands;
using System.Windows.Navigation;

namespace BookingProject.View
{
    public class AddAccommodationViewModel
    {
        public Accommodation accommodation { get; set; }
        public ObservableCollection<AccommodationType> accommodationTypes { get; set; }
        public AccommodationType chosenType { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public AccommodationLocationController LocationController { get; set; }
        public AccommodationImageController ImageController { get; set; }
        public ObservableCollection<AccommodationImage> Images { get; set; }
        public AccommodationImage AccommodationImage { get; set; }
        public RelayCommand AddImageCommand { get; }
        public RelayCommand AddAccommodationCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand MenuCommand { get; }
        public RelayCommand BackCommand { get; }
        public NavigationService NavigationService { get; set; }

        public AddAccommodationViewModel(NavigationService navigationService)
        {
            var types = Enum.GetValues(typeof(AccommodationType)).Cast<AccommodationType>();
            accommodationTypes = new ObservableCollection<AccommodationType>(types);

            //var app = Application.Current as App;
            AccommodationController = new AccommodationController();
            LocationController = new AccommodationLocationController();
            ImageController = new AccommodationImageController();
            //AccommodationController = app.AccommodationController;
            //LocationController = app.AccommodationLocationController;
            //ImageController= app.AccommodationImageController;
            AddImageCommand = new RelayCommand(Button_Click_Add_Image, CanExecute);
            AddAccommodationCommand = new RelayCommand(Button_Click_Add, CanExecute);
            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            MenuCommand = new RelayCommand(Button_Click_Menu, CanExecute);
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            NavigationService = navigationService;
        }
        private bool CanExecute(object param) { return true; }

        private string _accommodationName;

        private void Button_Click_Menu(object param)
        {
            MenuView view = new MenuView();
            view.Show();
            CloseWindow();
        }
        private void Button_Click_Back(object param)
        {
            //var view = new OwnerssView();
            //view.Show();
            //CloseWindow();
            NavigationService.GoBack();
        }
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

        private void Button_Click_Add(object param)
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

            //AccommodationController.Create(accommodation);
            //AccommodationController.SaveAccommodation();

            ImageController.LinkToAccommodation(accommodation.Id);
            ImageController.SaveImage();

            if (IsValid)
            {
                AccommodationController.Create(accommodation);
                MessageBox.Show("You have succesfully added new accommodation");
                //var view = new OwnerssView();
                //view.Show();
                NavigationService.Navigate(new OwnerssView(NavigationService));
            }
            CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AddAccommodationView)) { window.Close(); }
            }
        }
        private void Button_Click_Cancel(object param)
        {
            ImageController.DeleteUnused();
            ImageController.SaveImage();
            NavigationService.GoBack();
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
        private void Button_Click_Add_Image(object param)
        {
            //AddPhotosToAccommodationView addPhoto = new AddPhotosToAccommodationView();
            //addPhoto.Show();
            NavigationService.Navigate(new AddPhotosToAccommodationView(NavigationService));
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
