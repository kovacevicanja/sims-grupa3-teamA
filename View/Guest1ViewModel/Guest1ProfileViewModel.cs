using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.Guest1View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest1ViewModel
{
    public class Guest1ProfileViewModel
    {
        private UserController userController;
        private SuperGuestController superGuestController;
        public RelayCommand HomepageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogoutCommand { get; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand MyProfileCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Guest1ProfileViewModel()
        {
            userController = new UserController();
            superGuestController = new SuperGuestController();
            User guest = userController.GetLoggedUser();
            SetParameters(guest);
            HomepageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogoutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1ProfileView)) { window.Close(); }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string _numberOfReservations;
        public string NumberOfReservations
        {
            get => _numberOfReservations;
            set
            {
                if (_numberOfReservations != value)
                {
                    _numberOfReservations = value;
                    OnPropertyChanged();
                }

            }
        }
        public string _typeOfGuest;
        public string TypeOfGuest
        {
            get => _typeOfGuest;
            set
            {
                if (_typeOfGuest != value)
                {
                    _typeOfGuest = value;
                    OnPropertyChanged();
                }

            }
        }
        public string _bonusPoints;
        public string BonusPoints
        {
            get => _bonusPoints;
            set
            {
                if (_bonusPoints != value)
                {
                    _bonusPoints = value;
                    OnPropertyChanged();
                }

            }
        }
        public void SetParameters(User guest)
        {
            if (guest.IsSuper)
            {
                SetSuperGuest(guest);
            }
            else
            {
                SetOrdinaryGuest(guest);
            }
        }
        public void SetSuperGuest(User guest)
        {
            NumberOfReservations = superGuestController.FindNumberOfReservations(guest).ToString();
            TypeOfGuest = "SUPER";
            SuperGuest superGuest = superGuestController.GetById(guest.Id);
            BonusPoints = superGuest.BonusPoints.ToString();
        }
        public void SetOrdinaryGuest(User guest)
        {
            NumberOfReservations = superGuestController.FindNumberOfReservations(guest).ToString();
            TypeOfGuest = "ORDINARY";
            BonusPoints = "0";
        }
        private void Button_Click_Homepage(object param)
        {
            var Guest1Homepage = new Guest1HomepageView();
            Guest1Homepage.Show();
            CloseWindow();
        }

        private void Button_Click_MyReservations(object param)
        {
            var Guest1Reservations = new Guest1Reservations();
            Guest1Reservations.Show();
            CloseWindow();
        }

        private void Button_Click_Logout(object param)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }

        private void Button_Click_MyReviews(object param)
        {
            var reviews = new Guest1ReviewsView();
            reviews.Show();
            CloseWindow();
        }

        private void Button_Click_MyProfile(object param)
        {
            var profile = new Guest1ProfileView();
            profile.Show();
            CloseWindow();
        }
    }
}
