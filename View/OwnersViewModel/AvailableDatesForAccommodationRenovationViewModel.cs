using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.Services.Implementations;
using BookingProject.View.OwnersView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.OwnersViewModel
{
    public class AvailableDatesForAccommodationRenovationViewModel
    {
        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationRenovationController _renovationController { get; set; }
        public RelayCommand ScheduleRenovationCommand { get; set; }
        public Tuple<DateTime, DateTime> SelectedDatePair { get; set; }
        public AvailableDatesForAccommodationRenovationViewModel(Accommodation selectedAccommodation, ObservableCollection<Tuple<DateTime, DateTime>> availableDates)
        {
            SelectedAccommodation = selectedAccommodation;
            _renovationController = new AccommodationRenovationController();
            AvailableDatesPair = availableDates;
            ScheduleRenovationCommand = new RelayCommand(Button_Click_Schedule, CanExecute);
        }
        private void Button_Click_Schedule(object param)
        {
            var view = new AccommodationRenovationsView();
            view.Show();
            CloseWindow();
        }
        private ObservableCollection<Tuple<DateTime, DateTime>> _availableDatesPair;
        public ObservableCollection<Tuple<DateTime, DateTime>> AvailableDatesPair
        {
            get => _availableDatesPair;
            set
            {
                if (value != _availableDatesPair)
                {
                    _availableDatesPair = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _renovationDescription;
        public string RenovationDescription
        {
            get => _renovationDescription;
            set
            {
                if (value != _renovationDescription)
                {
                    _renovationDescription = value;
                    OnPropertyChanged();
                }
            }
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AvailableDatesForAccommodationRenovationView)) { window.Close(); }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanExecute(object param) { return true; }
    }
}
