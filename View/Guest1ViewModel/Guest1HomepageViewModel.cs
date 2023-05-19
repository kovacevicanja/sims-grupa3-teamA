using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.Guest1View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.ConversionHelp;

namespace BookingProject.View.Guest1ViewModel
{
    public class Guest1HomepageViewMddel : INotifyPropertyChanged
    {
        private AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public AccommodationLocationController _accommodationLocationController;
        public Accommodation selectedAccommodation { get; set; }
        public string AccName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string NumberOfGuests { get; set; } = string.Empty;
        public string MinNumDaysOfReservation { get; set; } = string.Empty;
        public Boolean IsCheckedApartment { get; set; } = false;
        public Boolean IsCheckedCottage { get; set; } = false;
        public Boolean IsCheckedHouse { get; set; } = false;
        public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }
        public ObservableCollection<string> CityCollection { get; set; }
        public ObservableCollection<string> CountryComboBox { get; set; }

        public List<string> AccommodationTypes;
        public RelayCommand SearchCommand { get; }
        public RelayCommand BookCommand { get; }
        public RelayCommand CancelSearchCommand { get; }
        public RelayCommand HomepageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand FillCityCommand { get; }
        public RelayCommand LogoutCommand { get; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand MyProfileCommand { get; }
        public Guest1HomepageViewMddel()
        {
            _accommodationController = new AccommodationController();
            _accommodationLocationController = new AccommodationLocationController();
            FilteredAccommodations = new ObservableCollection<Accommodation>();
            List<Accommodation> accommodations = new List<Accommodation>(_accommodationController.GetAll());
            List<Accommodation> sortedAccommodations = accommodations.OrderByDescending(a => a.Owner.IsSuper).ToList();
            Accommodations = new ObservableCollection<Accommodation>(sortedAccommodations);

            AccommodationTypes = new List<String>();
            CityCollection = new ObservableCollection<string>();
            CountryComboBox = new ObservableCollection<string>();
            
            SearchCommand = new RelayCommand(Button_Click_Search, CanExecute);
            BookCommand = new RelayCommand(Button_Click_Book, CanIfSelected);
            CancelSearchCommand = new RelayCommand(Button_Click_Cancel_Search, CanExecute);
            HomepageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            FillCityCommand = new RelayCommand(FindCities, CanExecute);
            LogoutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
            FindAllStates();
        }

        private bool _cityComboBoxEnabled;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CityComboboxEnabled
        {
            get => _cityComboBoxEnabled;
            set
            {
                if (_cityComboBoxEnabled != value)
                {
                    _cityComboBoxEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1HomepageView)) { window.Close(); }
            }
        }

        private bool CanIfSelected(object param)
        {
            if (selectedAccommodation == null) { return false; }
            else { return true; }
        }

        public void FindAllStates()
        {
            {
                List<string> items = new List<string>();

                using (StreamReader reader = new StreamReader("../../Resources/Data/accommodationLocations.csv"))
                {
                    while (!reader.EndOfStream)
                    {

                        string[] fields = reader.ReadLine().Split(',');
                        foreach (var field in fields)
                        {
                            string[] Countries = field.Split('|');
                            items.Add(Countries[2]);
                        }
                    }
                }
                var distinctItems = items.Distinct().ToList();

                UpdateCountryComboBox(distinctItems);
                if(State == null)
                {
                    CityComboboxEnabled = false;
                }

            }
        }
        public void UpdateCountryComboBox(List<string> coutries)
        {
            CountryComboBox.Clear();
            foreach (string s in coutries)
            {
                CountryComboBox.Add(s);
            }
        }
        private void FindCities(object param)
        {

            CityCollection.Clear();

            var locations = _accommodationLocationController.GetAll().Where(l => l.Country.Equals(State));

            foreach (Location location in locations)
            {
                CityCollection.Add(location.City);
            }

            CityComboboxEnabled = true;

        }

        private void Button_Click_MyReservations(object param)
        {
            var res = new Guest1Reservations();
            res.Show();
            CloseWindow();
        }

        private void Button_Click_Search(object param)
        {
            List<Accommodation> Filtered = new List<Accommodation>();
            List<Accommodation> SortedFiltered = new List<Accommodation>();
            AccommodationTypes.Clear();
            if (IsCheckedHouse)
            {
                AccommodationTypes.Add("HOUSE");
            }
            if (IsCheckedCottage)
            {
                AccommodationTypes.Add("COTTAGE");
            }
            if (IsCheckedApartment)
            {
                AccommodationTypes.Add("APARTMENT");
            }
            Accommodations.Clear();
            Filtered = _accommodationController.Search(Accommodations, AccName, City, State, AccommodationTypes, NumberOfGuests, MinNumDaysOfReservation).Distinct().ToList();
            SortedFiltered = Filtered.OrderByDescending(a => a.Owner.IsSuper).ToList();
            foreach (var accommodation in SortedFiltered)
            {
                Accommodations.Add(accommodation);
            }

        }

        private void Button_Click_Book(object param)
        {
            ReservationAccommodationView reservationAccommodationView = new ReservationAccommodationView(selectedAccommodation);
            reservationAccommodationView.Show();
            CloseWindow();
        }

        private void Button_Click_Cancel_Search(object param)
        {
            Accommodations.Clear();
            foreach(Accommodation a in _accommodationController.GetAll())
            {
                Accommodations.Add(a);
            }
        }

        private void Button_Click_Homepage(object param)
        {
            var ghp = new Guest1HomepageView();
            ghp.Show();
            CloseWindow();
        }

        private void Button_Click_Logout(object param)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }

        private void Button_Click_MyReviews(object param)
        {
            var reviews = new Guest1ReviewsView();
            reviews.Show();
            CloseWindow();
        }
        private void Button_Click_MyProfile(object param)
        {
            var profile = new Guest1ProfileView();
            profile.Show();
            CloseWindow();
        }
    }
}
