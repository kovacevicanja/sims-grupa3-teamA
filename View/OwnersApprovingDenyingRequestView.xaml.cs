using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using Microsoft.Win32;
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
using BookingProject.Controllers;

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
        private ReservationMovingRequestController _movingController;
        public OwnersApprovingDenyingRequestView(ReservationMovingRequest selectedMovingRequest)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedMovingRequest= selectedMovingRequest;
            _reservationController = new AccommodationReservationController();
            _movingController = new ReservationMovingRequestController();
            Availability = _reservationController.IsAvailableToMove(selectedMovingRequest);
            
        }

        private void Button_Click_Accept(object sender, RoutedEventArgs e)
        {
            SelectedMovingRequest.Status = RequestStatus.ACCEPTED;
            _movingController.Update(SelectedMovingRequest);
            _movingController.AcceptRequest(SelectedMovingRequest);
            Close();
        }
        private void Button_Click_Decline(object sender, RoutedEventArgs e)
        {
            SelectedMovingRequest.Status = RequestStatus.DECLINED;
            _movingController.Update(SelectedMovingRequest);
            Close();
        }
    }
}
