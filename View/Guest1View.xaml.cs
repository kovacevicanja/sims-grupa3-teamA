using BookingProject.Controller;
using BookingProject.FileHandler;
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

        public List<string> AccommodationTypes;

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

        public Guest1View()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            AllCities = new ObservableCollection<string>();
            _accommodationLocationController = new AccommodationLocationController();
            _accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
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

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
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
            _accommodationController.Search(_accommodations, AccName, City, State, AccommodationTypes, NumberOfGuests, MinNumDaysOfReservation);
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

       
        
    }
}
