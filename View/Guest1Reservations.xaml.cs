﻿using BookingProject.Controller;
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
    /// Interaction logic for Guest1Reservations.xaml
    /// </summary>
    public partial class Guest1Reservations : Window
    {
        public ObservableCollection<AccommodationReservation> _reservations;
        public AccommodationReservationController _accommodationReservationController;
        public AccommodationReservation SelectedReservation { get; set; }

        public Guest1Reservations()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationReservationController = new AccommodationReservationController();
            _reservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationController.GetAll());
            ReservationsDataGrid.ItemsSource = _reservations;
        }

        
        private void Button_Click_Review(object sender, RoutedEventArgs e)
        {
            AccommodationOwnerReview aor = new AccommodationOwnerReview(SelectedReservation);
            aor.Show();
        }
    }
}
