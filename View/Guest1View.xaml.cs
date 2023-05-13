using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
    /// Interaction logic for Guest1View.xaml
    /// </summary>
    public partial class Guest1View : Window
    {
        private AccommodationController _accommodationController;
        private ObservableCollection<Accommodation> _accommodations;
        public ObservableCollection<string> AllCities { get; set; }
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

        public List<string> AccommodationTypes;
        public Guest1View()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            AllCities = new ObservableCollection<string>();
            _accommodationLocationController = new AccommodationLocationController();
            //_accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
            FilteredAccommodations = new ObservableCollection<Accommodation>();
            List<Accommodation> accommodations = new List<Accommodation>(_accommodationController.GetAll());
            List<Accommodation> sortedAccommodations = accommodations.OrderByDescending(a => a.Owner.IsSuper).ToList();
            _accommodations = new ObservableCollection<Accommodation>(sortedAccommodations);
            AccommodationDataGrid.ItemsSource = _accommodations;

            AccommodationTypes = new List<String>();
            FindAllStates();

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

                comboBoxState.ItemsSource = distinctItems;

                if (comboBoxState.SelectedItem == null)
                {
                    comboBoxCity.IsEnabled = false;
                }

            }
        }
        private void FindCities(object sender, SelectionChangedEventArgs e)
        {

            AllCities.Clear();

            var locations = _accommodationLocationController.GetAll().Where(l => l.Country.Equals(State));

            foreach (Location location in locations)
            {
                AllCities.Add(location.City);
            }

            comboBoxCity.IsEnabled = true;

        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
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
            FilteredAccommodations.Clear();
            Filtered = _accommodationController.Search(_accommodations, AccName, City, State, AccommodationTypes, NumberOfGuests, MinNumDaysOfReservation).ToList();
            SortedFiltered = Filtered.OrderByDescending(a => a.Owner.IsSuper).ToList();
            foreach (var accommodation in SortedFiltered)
            {
                FilteredAccommodations.Add(accommodation);
            }
            AccommodationDataGrid.ItemsSource = FilteredAccommodations;

        }

        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {
            ReservationAccommodationView reservationAccommodationView = new ReservationAccommodationView(selectedAccommodation);
            reservationAccommodationView.Show();
        }

        private void Button_Click_Cancel_Search(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Res(object sender, RoutedEventArgs e)
        {
            Guest1Reservations g1r = new Guest1Reservations();
            g1r.Show();
            this.Close();
        }
    }
}