using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.GuideViewModel;
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

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuestPreseanceCheck.xaml
    /// </summary>
    public partial class GuestPreseanceCheck : Window
    {
        public GuestPreseanceCheck(TourTimeInstance chosenTour, KeyPoint chosenKeyPoint, User chosenGuest)
        {
            InitializeComponent();
            this.DataContext = new GuestPresenceCheckViewModel(chosenTour, chosenKeyPoint, chosenGuest);
        }
    }
}
