using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.View.Guest1View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest1ViewModel
{
    public class FindAvailableDatesForAccommodationViewModel
    {
        public ObservableCollection<Range> Ranges { get; set; }
        private AccommodationReservationController accommodationReservationController;
        private UserController userController;
        private SuperGuestController superGuestController;
        public event PropertyChangedEventHandler PropertyChanged;

        public Range selectedDates { get; set; }
        public Accommodation _selectedAccommodation { get; set; }
        public RelayCommand BookCommand { get; }
        public RelayCommand HomePageCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand MyProfileCommand { get; }

        public FindAvailableDatesForAccommodationViewModel(List<(DateTime, DateTime)> ranges, Accommodation selectedAccommodation)
        {
            accommodationReservationController = new AccommodationReservationController();
            userController = new UserController();
            superGuestController = new SuperGuestController();
            _selectedAccommodation = selectedAccommodation;
            Ranges = new ObservableCollection<Range>(ranges.Select(r => new Range { StartDate = r.Item1, EndDate = r.Item2 }).ToList());
            BookCommand = new RelayCommand(Button_Click_Book, CanIfSelected);
            HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(FindAvailableDatesForAccommodation)) { window.Close(); }
            }
        }
        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public string _numberOfGuests;
        public string NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                if (_numberOfGuests != value)
                {
                    _numberOfGuests = value;
                    OnPropertyChanged();
                }
            }

        }

        public class Range
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        private bool CanIfSelected(object param)
        {
            if (selectedDates == null || NumberOfGuests == null) { return false; }
            else { return true; }
        }

        private void Button_Click_Book(object param)
        {
            if (!accommodationReservationController.CheckNumberOfGuests(_selectedAccommodation, NumberOfGuests))
            {
                MessageBox.Show("Maximum number of guests in this accommodation is " + _selectedAccommodation.MaxGuestNumber + " !");
            }
            else
            {
                User user = userController.GetLoggedUser();
                if (user.IsSuper)
                {
                    superGuestController.ReduceBonusPoints(user);
                }
                accommodationReservationController.BookAccommodation(selectedDates.StartDate, selectedDates.EndDate, _selectedAccommodation);
                MessageBox.Show("Successfully reserved accommodation!");
            }

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
