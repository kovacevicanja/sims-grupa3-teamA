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
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestMyTours.xaml
    /// </summary>
    public partial class SecondGuestMyReservations : Page
    {
        public SecondGuestMyReservations(int guestId, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new SecondGuestMyReservationsViewModel(guestId, navigationService);
        }
    }
}