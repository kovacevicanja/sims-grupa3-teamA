using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingProject.Domain.Enums;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.Commands;
using BookingProject.View.Guest2ViewModel;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestMyVouchersView.xaml
    /// </summary>
    public partial class SecondGuestMyVouchersView : Window
    {
        public SecondGuestMyVouchersView(int guestId, Tour chosenTour)
        {
            InitializeComponent();
            this.DataContext = new SecondGuestMyVouchersViewModel(guestId, chosenTour);
        }
    }
}