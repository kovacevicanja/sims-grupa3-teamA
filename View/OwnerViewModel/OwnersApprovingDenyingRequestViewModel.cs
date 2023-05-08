using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain.Enums;
using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;
using BookingProject.Commands;

namespace BookingProject.View.OwnerViewModel
{
    public class OwnersApprovingDenyingRequestViewModel
    {
        public RequestAccommodationReservation SelectedMovingRequest { get; set; }
        public Boolean Availability { get; set; }
        private AccommodationReservationController _reservationController;
        private RequestAccommodationReservationController _movingController;
        public RelayCommand AcceptCommand { get; }
        public RelayCommand DeclineCommand { get; }

        public OwnersApprovingDenyingRequestViewModel(RequestAccommodationReservation selectedMovingRequest)
        {
            SelectedMovingRequest = selectedMovingRequest;
            _reservationController = new AccommodationReservationController();
            _movingController = new RequestAccommodationReservationController();
            Availability = _reservationController.IsAvailableToMove(selectedMovingRequest);
            AcceptCommand = new RelayCommand(Button_Click_Accept, CanExecute);
            DeclineCommand = new RelayCommand(Button_Click_Decline, CanExecute);

        }
        private bool CanExecute(object param) { return true; }

        private void Button_Click_Accept(object param)
        {
            SelectedMovingRequest.Status = RequestStatus.APPROVED;
            if (_movingController.PermissionToAcceptDenyRequest(SelectedMovingRequest))
            {
                //SelectedMovingRequest.Status = RequestStatus.APPROVED;
                _movingController.Update(SelectedMovingRequest);
                _movingController.AcceptRequest(SelectedMovingRequest);
                CloseWindow();
            }
        }
        private void Button_Click_Decline(object param)
        {
            SelectedMovingRequest.Status = RequestStatus.DECLINED;
            if (_movingController.PermissionToAcceptDenyRequest(SelectedMovingRequest))
            {
                _movingController.Update(SelectedMovingRequest);
                CloseWindow();
            }
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(OwnersApprovingDenyingRequestView)) { window.Close(); }
            }
        }

        //private void Comment_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (Comment.Text.Length > 0)
        //    {
        //        Button_Comment.IsEnabled = false;
        //    }
        //    else
        //    {
        //        Button_Comment.IsEnabled = true;
        //    }
        //}
    }
}
