using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for ReservationAccommodationView.xaml
    /// </summary>
    public partial class ReservationAccommodationView : Window
    {
        private Accommodation _selectedAccommodation;
        private AccommodationReservationController accommodationReservationController;

        public event PropertyChangedEventHandler PropertyChanged;

        public ReservationAccommodationView(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            this.DataContext = this;
            _selectedAccommodation = new Accommodation();
            _selectedAccommodation = selectedAccommodation;
            accommodationReservationController = new AccommodationReservationController();
            InitialDate = DateTime.Now;
            EndDate = DateTime.Now;
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



        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {
            int NumberOfDaysToStay = (EndDate - InitialDate).Days;

            if (!accommodationReservationController.CheckEnteredDates(InitialDate, EndDate))
            {
                MessageBox.Show("You didn't enter valid date!");
                this.Close();
            }else if (!accommodationReservationController.CheckNumberOfGuests(_selectedAccommodation, NumberOfGuests))
            {
                MessageBox.Show("Maximum number of guests in this accommodation is " + _selectedAccommodation.MaxGuestNumber + " !");
                this.Close();
            }else if(_selectedAccommodation.MinDays > NumberOfDaysToStay)
            {
                MessageBox.Show("this accommodation requires a minimum stay of " + _selectedAccommodation.MinDays +" days!");
                this.Close();
            }else if (accommodationReservationController.CheckAvailableDate(_selectedAccommodation, InitialDate, EndDate, NumberOfDaysToStay, NumberOfGuests))
            {
                accommodationReservationController.BookAccommodation(InitialDate, EndDate, _selectedAccommodation);
                MessageBox.Show("Successfully reserved this accommodation!");

            }
            else
            {       
                    MessageBox.Show("This accommodation is not available in this range of dates!");
                    List<(DateTime, DateTime)> ranges = accommodationReservationController.FindAvailableDates(_selectedAccommodation, InitialDate, EndDate, NumberOfDaysToStay);
                    FindAvailableDatesForAccommodation findAvailableDatesForAccommodation = new FindAvailableDatesForAccommodation(ranges, _selectedAccommodation);
                    findAvailableDatesForAccommodation.Show();
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
