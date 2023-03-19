using BookingProject.Controller;
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
    /// Interaction logic for NotGradedView.xaml
    /// </summary>
    public partial class NotGradedView : Window
    {
        private AccommodationReservationController _accommodationController;
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }
        public NotGradedView()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _accommodationController = app.AccommodationReservationController;


            _accommodationController.Load();
            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationController.GetAllNotGradedReservations());
        }
        private void selectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = SelectedReservation;
            var window2 = new GuestRateView(selectedItem);
            window2.SelectedObject = selectedItem;
            window2.Show();
        }
        private void Button_Grade(object sender, RoutedEventArgs e)
        {
            //if(SelectedReservation != null)
            //{
                GuestRateView view = new GuestRateView(SelectedReservation);
                view.Show();
            //}
        }
        public int RowNum()
        {
            return Reservations.Count;
        }
    }
}
