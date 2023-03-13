using BookingProject.Controller;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        

        public string AccName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string NumberOfGuests { get; set; } = string.Empty;
        public string MinNumDaysOfReservation { get; set; } = string.Empty;

        public Guest1View()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            _accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
            AccommodationDataGrid.ItemsSource = _accommodations;

            ourComboBox.ItemsSource = new List<string>() { "APARTMENT", "HOUSE", "COTTAGE"};

        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            SearchAccommodationsView searchAccommodationsView = new SearchAccommodationsView(AccName, City, State, Type, NumberOfGuests, MinNumDaysOfReservation);
            searchAccommodationsView.Show();
        }

        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {
            ReservationAccommodationView reservationAccommodationView = new ReservationAccommodationView();
            reservationAccommodationView.Show();
        }

        private void Button_Click_Cancel_Search(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
