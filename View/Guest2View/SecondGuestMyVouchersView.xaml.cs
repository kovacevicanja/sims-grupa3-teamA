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
using System.Windows.Navigation;
using System.Windows.Controls;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestMyVouchersView.xaml
    /// </summary>
    public partial class SecondGuestMyVouchersView : Page
    {
        public SecondGuestMyVouchersView(int guestId, int chosenTourId, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new SecondGuestMyVouchersViewModel(guestId, chosenTourId, navigationService);
        }
    }
}