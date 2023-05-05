using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.Guest2ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestMyTours.xaml
    /// </summary>
    public partial class SecondGuestMyReservations : Window
    {
        public SecondGuestMyReservations(int guestId)
        {
            InitializeComponent();
            this.DataContext = new SecondGuestMyReservationsViewModel(guestId);
        }
    }
}