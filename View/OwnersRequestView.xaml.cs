﻿using BookingProject.Controllers;
using BookingProject.Domain;
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
    /// Interaction logic for OwnersRequestView.xaml
    /// </summary>
    public partial class OwnersRequestView : Window
    {
        private RequestAccommodationReservationController _requestController;
        public ObservableCollection<RequestAccommodationReservation> Requests { get; set; }
        public RequestAccommodationReservation SelectedMovingRequest { get; set; }
        public OwnersRequestView()
        {
            InitializeComponent();
            this.DataContext = this;
            _requestController = new RequestAccommodationReservationController();
            int ownerId = SignInForm.LoggedInUser.Id;                                                                                                   //change to logged user
            Requests = new ObservableCollection<RequestAccommodationReservation>(_requestController.GetAllRequestForOwner(ownerId));
        }
        private void Button_Click_View(object sender, RoutedEventArgs e)
        {
            if (SelectedMovingRequest == null)
            {
                return;
            }
            OwnersApprovingDenyingRequestView view = new OwnersApprovingDenyingRequestView(SelectedMovingRequest);
            view.Show();
        }
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            MenuView view = new MenuView();
            view.Show();
        }
    }
}
