using BookingProject.Controller;
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
    /// Interaction logic for GuestPreseanceCheck.xaml
    /// </summary>
    public partial class GuestPreseanceCheck : Window
    {
        public KeyPoint ChosenKeyPoint;

        public TourTimeInstance ChosenTour;

        public TourGuest ChosenGuest;

        public TourGuestController _tourGuestController;

        public GuestPreseanceCheck(TourTimeInstance chosenTour, KeyPoint chosenKeyPoint, TourGuest chosenGuest)
        {
            InitializeComponent();
            var app = Application.Current as App;
            ChosenKeyPoint = chosenKeyPoint;
            ChosenTour = chosenTour;
            ChosenGuest = chosenGuest;
            _tourGuestController= app.TourGuestController;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            GuestListView guestListView = new GuestListView(ChosenTour, ChosenKeyPoint);
            guestListView.Show();
            Close();
        }

        private void Button_Click_Kreiraj(object sender, RoutedEventArgs e)
        {
            _tourGuestController.GetByID(ChosenGuest.Id).KeyPointId=ChosenKeyPoint.Id;
            _tourGuestController.Save();
            GuestListView guestListView = new GuestListView(ChosenTour, ChosenKeyPoint);
            guestListView.Show();
            Close();
        }
    }
}
