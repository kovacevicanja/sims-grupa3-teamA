using BookingProject.Controller;
using BookingProject.Domain;
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

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuestPreseanceCheck.xaml
    /// </summary>
    public partial class GuestPreseanceCheck : Window
    {
        public KeyPoint ChosenKeyPoint;
        public TourTimeInstance ChosenTour;
        public User ChosenGuest;
        public UserController _userController;
        public TourPresenceController _tourPresenceController;

        public GuestPreseanceCheck(TourTimeInstance chosenTour, KeyPoint chosenKeyPoint, User chosenGuest)
        {
            InitializeComponent();
            ChosenKeyPoint = chosenKeyPoint;
            ChosenTour = chosenTour;
            ChosenGuest = chosenGuest;
            _tourPresenceController = new TourPresenceController();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_Ask(object sender, RoutedEventArgs e)
        {
            TourPresence tourPresence = new TourPresence();
            tourPresence.TourId = ChosenTour.Id;
            tourPresence.UserId = ChosenGuest.Id;
            _tourPresenceController.Create(tourPresence);
            _tourPresenceController.Save();
            _tourPresenceController.SendNotification(ChosenGuest);
            Close();
        }
    }
}
