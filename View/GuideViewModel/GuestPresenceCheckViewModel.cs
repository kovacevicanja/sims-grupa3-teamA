using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class GuestPresenceCheckViewModel
    {
        public KeyPoint ChosenKeyPoint;
        public TourTimeInstance ChosenTour;
        public User ChosenGuest;
        public UserController _userController;
        public TourPresenceController _tourPresenceController;
        public RelayCommand NCommand { get; }
        public RelayCommand YCommand { get; }

        public GuestPresenceCheckViewModel(TourTimeInstance chosenTour, KeyPoint chosenKeyPoint, User chosenGuest)
        {
            ChosenKeyPoint = chosenKeyPoint;
            ChosenTour = chosenTour;
            ChosenGuest = chosenGuest;
            _tourPresenceController = new TourPresenceController();
            NCommand = new RelayCommand(CancelButton_Click, CanExecute);
            YCommand = new RelayCommand(Button_Click_Ask, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(GuestPreseanceCheck)) { window.Close(); }
            }
        }
        private void CancelButton_Click(object param)
        {
            CloseWindow();
        }





        private void Button_Click_Ask(object param)
        {
            TourPresence tourPresence = new TourPresence();
            tourPresence.TourId = ChosenTour.Id;
            tourPresence.UserId = ChosenGuest.Id;
            _tourPresenceController.Create(tourPresence);
            _tourPresenceController.Save();
            _tourPresenceController.SendNotification(ChosenGuest);
            CloseWindow();
        }
    }
}
