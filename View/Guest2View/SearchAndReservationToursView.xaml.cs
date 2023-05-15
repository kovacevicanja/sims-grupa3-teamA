using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using BookingProject.View.Guest2ViewModel;
using Microsoft.VisualBasic.FileIO;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestView.xaml
    /// </summary>
    public partial class SerachAndReservationToursView : Page
    {
        public SerachAndReservationToursView(int guestId, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new SearchAndReservationToursViewModel(guestId, navigationService);
        }
    }
}
