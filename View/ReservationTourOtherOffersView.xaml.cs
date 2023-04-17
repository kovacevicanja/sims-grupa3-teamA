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
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for ReservationTourOtherOffersView.xaml
    /// </summary>
    public partial class ReservationTourOtherOffersView : Window
    {
        private ObservableCollection<Tour> _tours;
        private TourReservationController _tourReservationController;
        public Tour ChoosenTour { get; set; } //ovo je stara odabrana tura
        public int GuestId { get; set; }
        public ReservationTourOtherOffersView(Tour choosenTour, DateTime selectedDate, int guestId)
        {
            InitializeComponent();
            this.DataContext = this;
            ChoosenTour = choosenTour;
            _tourReservationController = new TourReservationController();
            _tours = new ObservableCollection<Tour>(_tourReservationController.GetFilteredTours(choosenTour.Location, selectedDate));
            TourDataGrid.ItemsSource = _tours;
            GuestId = guestId;
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_TryToBook(object sender, RoutedEventArgs e)
        {
            ReservationTourView reservationTourView = new ReservationTourView(ChoosenTour, GuestId);
            reservationTourView.Show();

        }
    }
}
