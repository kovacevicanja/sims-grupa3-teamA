using BookingProject.Controller;
using BookingProject.FileHandler;
using BookingProject.Model;
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
        public Guest1View()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            _accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
            AccommodationDataGrid.ItemsSource = _accommodations;

        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            SearchAccommodationsView searchAccommodationsView = new SearchAccommodationsView();
            searchAccommodationsView.Show();
        }

        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {
            ReservationAccommodationView reservationAccommodationView = new ReservationAccommodationView();
            reservationAccommodationView.Show();
        }
    }
}
