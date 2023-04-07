using BookingProject.Controller;
using BookingProject.Domain;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for OwnersApprovingDenyingRequestView.xaml
    /// </summary>
    public partial class OwnersApprovingDenyingRequestView : Window
    {
        public ReservationMovingRequest SelectedMovingRequest { get; set; }
        public Boolean Availability { get; set; }
        private AccommodationReservationController _reservationController;
        public OwnersApprovingDenyingRequestView(ReservationMovingRequest selectedMovingRequest)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedMovingRequest= selectedMovingRequest;
            _reservationController = new AccommodationReservationController();
            Availability = _reservationController.IsAvailableToMove(selectedMovingRequest);
        }
        
        private void Button_Click_Accept(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Decline(object sender, RoutedEventArgs e)
        {

        }
    }
}
