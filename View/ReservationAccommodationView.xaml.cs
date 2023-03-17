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

        public string _numberOfDaysToStay;
        public string NumberOfDaysToStay
        {
            get => _numberOfDaysToStay;
            set
            {
                if(_numberOfDaysToStay != value)
                {
                    _numberOfDaysToStay = value;
                    OnPropertyChanged();
                }
            }

        }

        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {
            

            if (accommodationReservationController.isValidBook(_selectedAccommodation, InitialDate, EndDate, NumberOfDaysToStay))
            {
                MessageBox.Show("Successfully reserved accommodation!");
            }
            else
            {
                MessageBox.Show("Unsuccessfully reserved accommmodation!");
                List<(DateTime, DateTime)> ranges = accommodationReservationController.findAvailableDates(_selectedAccommodation, InitialDate, EndDate, NumberOfDaysToStay);
                FindAvailableDatesForAccommodation findAvailableDatesForAccommodation = new FindAvailableDatesForAccommodation(ranges);
                findAvailableDatesForAccommodation.Show();
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
