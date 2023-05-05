using System;
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
using BookingProject.Commands;
using BookingProject.View.Guest2ViewModel;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for ReservationTourView.xaml
    /// </summary>
    public partial class ReservationTourView : Window
    {
        public ReservationTourView(Tour chosenTour, int guestId)
        {
            InitializeComponent();
            this.DataContext = new ReservationTourViewModel(chosenTour, guestId);
        }
    }
}
