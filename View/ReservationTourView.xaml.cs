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
using BookingProject.Controller;
using BookingProject.Model;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for ReservationTourView.xaml
    /// </summary>
    public partial class ReservationTourView : Window
    {
        public Tour Tour { get; set; }
        private TourController _tourController; //
        private ObservableCollection<Tour> _tours; //
        public string NumberOfGuests { get; set; } = string.Empty;
        private TourReservationController _tourReservationController;
        private ObservableCollection<TourReservation> _tourReservations;

        public ReservationTourView(Tour choosenTour)
        {
            InitializeComponent();

            Tour = choosenTour;

           
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            

        }

        private void Button_Click_TryToBook(object sender, RoutedEventArgs e)
        {
            _tourReservationController.TryToBook(_tourReservations, Tour, NumberOfGuests); //i dalje je ovo null

        }
    }
}
