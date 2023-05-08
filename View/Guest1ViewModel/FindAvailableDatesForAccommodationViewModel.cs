using BookingProject.Commands;
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

namespace BookingProject.View.Guest1ViewModel
{
    public class FindAvailableDatesForAccommodationViewModel
    {
        public ObservableCollection<Range> Ranges { get; set; }
        private AccommodationReservationController accommodationReservationController;
        public event PropertyChangedEventHandler PropertyChanged;

        public Range selectedDates { get; set; }
        public Accommodation _selectedAccommodation { get; set; }
        public RelayCommand BookCommand { get; }

        public FindAvailableDatesForAccommodationViewModel(List<(DateTime, DateTime)> ranges, Accommodation selectedAccommodation)
        {
            accommodationReservationController = new AccommodationReservationController();
            _selectedAccommodation = selectedAccommodation;
            Ranges = new ObservableCollection<Range>(ranges.Select(r => new Range { StartDate = r.Item1, EndDate = r.Item2 }).ToList());
            BookCommand = new RelayCommand(Button_Click_Book, CanExecute);
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

        private void Button_Click_Book(object param)
        {
            if (!accommodationReservationController.CheckNumberOfGuests(_selectedAccommodation, NumberOfGuests))
            {
                MessageBox.Show("Maximum number of guests in this accommodation is " + _selectedAccommodation.MaxGuestNumber + " !");
                CloseWindow();
            }
            else
            {
                accommodationReservationController.BookAccommodation(selectedDates.StartDate, selectedDates.EndDate, _selectedAccommodation);
                MessageBox.Show("Successfully reserved accommodation!");
            }

        }
    }
}
