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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.View.Guest2ViewModel;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for ReservationTourOtherOffersView.xaml
    /// </summary>
    public partial class ReservationTourOtherOffersView : Page
    {
        public ReservationTourOtherOffersView(Tour chosenTour, DateTime selectedDate, int guestId, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new ReservationTourOtherOffersViewModel(chosenTour, selectedDate, guestId, navigationService);
        }
    }
}