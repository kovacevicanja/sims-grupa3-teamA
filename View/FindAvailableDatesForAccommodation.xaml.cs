using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FindAvailableDatesForAccommodation.xaml
    /// </summary>
    public partial class FindAvailableDatesForAccommodation : Window
    {
        /// <summary>
        /// Interaction logic for ShowDatesAccommodationsReservations.xaml
        /// </summary>
        public ObservableCollection<Range> Ranges { get; set; }
        private AccommodationReservationController accommodationReservationController;
        public event PropertyChangedEventHandler PropertyChanged;

        public Range selectedDates { get; set; }
        public Accommodation _selectedAccommodation { get; set; }

        public FindAvailableDatesForAccommodation(List<(DateTime, DateTime)> ranges, Accommodation selectedAccommodation)
        {
            InitializeComponent();
            this.DataContext = this;
            accommodationReservationController = new AccommodationReservationController();
            _selectedAccommodation = selectedAccommodation;

            Ranges = new ObservableCollection<Range>(ranges.Select(r => new Range { StartDate = r.Item1, EndDate = r.Item2 }).ToList());
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

        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {
            if(accommodationReservationController.checkNumberOfGuestsAndBook(selectedDates, NumberOfGuests, _selectedAccommodation))
            {
                MessageBox.Show("Successfully reserved accommodation!");
            }
            else
            {
                MessageBox.Show("Unsuccessfully reserved accommodation!");
            }
        }
    }
}
