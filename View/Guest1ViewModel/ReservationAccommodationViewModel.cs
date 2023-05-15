using BookingProject.Commands;
using BookingProject.Controller;
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
    public class ReservationAccommodationViewModel
    {
        private Accommodation _selectedAccommodation;
        private AccommodationReservationController accommodationReservationController;
        public AccommodationDateController accommodationDateController;

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand BookCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand HomepageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogoutCommand { get; }

        public ReservationAccommodationViewModel(Accommodation selectedAccommodation)
        {
            _selectedAccommodation = new Accommodation();
            _selectedAccommodation = selectedAccommodation;
            accommodationReservationController = new AccommodationReservationController();
            accommodationDateController = new AccommodationDateController();
            InitialDate = DateTime.Now;
            EndDate = DateTime.Now;
            BookCommand = new RelayCommand(Button_Click_Book, CanExecute);
            CloseCommand = new RelayCommand(Button_Click_Close, CanExecute);
            HomepageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogoutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ReservationAccommodationView)) { window.Close(); }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DateTime _initialDate;
        public DateTime InitialDate
        {
            get => _initialDate;
            set
            {
                if (_initialDate != value)
                {
                    _initialDate = value;
                    OnPropertyChanged();
                }
            }
        }


        public DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
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



        private void Button_Click_Book(object param)
        {
            int NumberOfDaysToStay = (EndDate - InitialDate).Days;

            if (EndDate == null || InitialDate == null || NumberOfGuests == null)
            {
                MessageBox.Show("First you need to fill all fields!");
            }
            else if (!accommodationDateController.CheckEnteredDates(InitialDate, EndDate))
            {
                MessageBox.Show("You didn't enter valid date!");
            }
            else if (!accommodationReservationController.CheckNumberOfGuests(_selectedAccommodation, NumberOfGuests))
            {
                MessageBox.Show("Maximum number of guests in this accommodation is " + _selectedAccommodation.MaxGuestNumber + " !");
            }
            else if (_selectedAccommodation.MinDays > NumberOfDaysToStay)
            {
                MessageBox.Show("This accommodation requires a minimum stay of " + _selectedAccommodation.MinDays + " days!");
            }
            else if (accommodationDateController.CheckAvailableDate(_selectedAccommodation, InitialDate, EndDate, NumberOfDaysToStay, NumberOfGuests))
            {
                accommodationReservationController.BookAccommodation(InitialDate, EndDate, _selectedAccommodation);
                MessageBox.Show("Successfully reserved this accommodation!");

            }
            else
            {
                MessageBox.Show("This accommodation is not available in this range of dates!");
                List<(DateTime, DateTime)> ranges = accommodationDateController.FindAvailableDates(_selectedAccommodation, InitialDate, EndDate, NumberOfDaysToStay);
                FindAvailableDatesForAccommodation findAvailableDatesForAccommodation = new FindAvailableDatesForAccommodation(ranges, _selectedAccommodation);
                findAvailableDatesForAccommodation.Show();
                CloseWindow();
            }
        }

        private void Button_Click_Close(object param)
        {
            CloseWindow();
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
    }
}
