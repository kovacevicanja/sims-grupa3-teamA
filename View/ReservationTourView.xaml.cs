﻿using System;
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
using BookingProject.ConversionHelp;
using BookingProject.Controllers;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for ReservationTourView.xaml
    /// </summary>
    public partial class ReservationTourView : Window
    {
        public string EnteredGuests { get; set; } = string.Empty;
        public TourDateTime SelectedDate { get; set; } 
        private TourReservationController _tourReservationController;
        private ObservableCollection<TourReservation> _tourReservations;
        public Tour ChoosenTour { get; set; }
        public Guest2 Guest { get; set; }
        public int GuestId { get; set; }
        public Guest2Controller Guest2Controller { get; set; }

        public ReservationTourView(Tour choosenTour, int guestId)
        {
            InitializeComponent();
            this.DataContext = this;

             ChoosenTour = choosenTour;
            _tourReservationController = new TourReservationController();
            _tourReservations = new ObservableCollection<TourReservation>(_tourReservationController.GetAll());
            GuestId = guestId;
            Guest2Controller = new Guest2Controller();
            Guest = new Guest2();
            Guest.Id = GuestId;
            //Guest = Guest2Controller.GetByID(GuestId);
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click_TryToBook(object sender, RoutedEventArgs e)
        {
            _tourReservationController.TryToBook(ChoosenTour, EnteredGuests, SelectedDate.StartingDateTime, Guest);
        }
    }
}
