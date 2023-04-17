﻿using BookingProject.Controller;
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
        public RequestAccommodationReservation SelectedMovingRequest { get; set; }
        public Boolean Availability { get; set; }
        private AccommodationReservationController _reservationController;
        private RequestAccommodationReservationController _movingController;
        
        public OwnersApprovingDenyingRequestView(RequestAccommodationReservation selectedMovingRequest)
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedMovingRequest= selectedMovingRequest;
            _reservationController = new AccommodationReservationController();
            _movingController = new RequestAccommodationReservationController();
            Availability = _reservationController.IsAvailableToMove(selectedMovingRequest);
            
        }
        

        private void Button_Click_Accept(object sender, RoutedEventArgs e)
        {
            SelectedMovingRequest.Status = RequestStatus.APPROVED;
            if (_movingController.PermissionToAcceptDenyRequest(SelectedMovingRequest))
            {
                //SelectedMovingRequest.Status = RequestStatus.APPROVED;
                _movingController.Update(SelectedMovingRequest);
                _movingController.AcceptRequest(SelectedMovingRequest);
                Close();
            }                
        }
        private void Button_Click_Decline(object sender, RoutedEventArgs e)
        {
            SelectedMovingRequest.Status = RequestStatus.DECLINED;
            if (_movingController.PermissionToAcceptDenyRequest(SelectedMovingRequest))
            {
                _movingController.Update(SelectedMovingRequest);
                Close();
            }
        }

        private void Comment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Comment.Text.Length > 0)
            {
                Button_Comment.IsEnabled= false;
            } else
            {
                Button_Comment.IsEnabled= true;
            }
        }
    }
}
