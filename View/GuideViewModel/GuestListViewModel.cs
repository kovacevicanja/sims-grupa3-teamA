using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class GuestListViewModel
    {
        public bool IsValid { get; set; }
        public User ChosenGuest { get; set; }
        private UserController _userController;
        public ObservableCollection<User> _guests;
        private TourReservationController _tourReservationController;
        public KeyPoint ChosenKeyPoint { get; set; }
        public TourTimeInstance ChosenTour { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand MarkCommand { get; }
        public GuestListViewModel(TourTimeInstance chosenTour, KeyPoint chosenKeyPoint)
        {
            _userController = new UserController();
            _tourReservationController = new TourReservationController();
            ChosenTour = chosenTour;
            ChosenKeyPoint = chosenKeyPoint;
            _guests = new ObservableCollection<User>(filterGuests(_tourReservationController.GetAll()));
            CancelCommand = new RelayCommand(Button_Click_Cancell, CanExecute);
            MarkCommand = new RelayCommand(Button_Click_Mark, CanExecute);
        }
        public List<User> filterGuests(List<TourReservation> reservations)
        {
            List<User> users = new List<User>();
            foreach (TourReservation reservation in reservations)
            {
                if (reservation.Tour.Id == ChosenTour.TourId)
                {
                    users.Add(_userController.GetById(reservation.Guest.Id));
                }
            }
            return users;
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(GuestListView)) { window.Close(); }
            }
        }

        private void Button_Click_Mark(object param)
        {
            if (ChosenGuest != null)
            {
                GuestPreseanceCheck guestPreseanceCheck = new GuestPreseanceCheck(ChosenTour, ChosenKeyPoint, ChosenGuest);
                guestPreseanceCheck.Show();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_Cancell(object param)
        {
            LiveTourView liveTourView = new LiveTourView(ChosenTour);
            liveTourView.Show();
            CloseWindow();
        }
    }
}
