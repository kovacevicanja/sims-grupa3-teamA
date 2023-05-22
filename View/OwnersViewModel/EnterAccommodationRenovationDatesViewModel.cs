using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.Services.Implementations;
using BookingProject.View.OwnersView;
using BookingProject.View.OwnerView;
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
    public class EnterAccommodationRenovationDatesViewModel
    {
        public AccommodationRenovationController _renovationController { get; set; }
        public AccommodationReservationController _reservationController { get; set; }
        public RelayCommand ShowCommand { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public EnterAccommodationRenovationDatesViewModel(Accommodation selectedAccommodation)
        {
            SelectedAccommodation=selectedAccommodation;
            AvailableDatesPair = new ObservableCollection<Tuple<DateTime, DateTime>>();
            _renovationController = new AccommodationRenovationController();
            _reservationController = new AccommodationReservationController();
            ShowCommand = new RelayCommand(Button_Click_Show, CanExecute);
        }
        private bool CanExecute(object param) { return true; }

        private void Button_Click_Show(object param)
        {
            //AvailableDatesPair = new ObservableCollection<Tuple<DateTime, DateTime>>(_renovationService.FindAvailableDates(StartDate, EndDate, RenovationDuration, SelectedAccommodation));
            AvailableDatesPair = new ObservableCollection<Tuple<DateTime, DateTime>>(_reservationController.FindAvailableDates(StartDate, EndDate, RenovationDuration, SelectedAccommodation));
            var view = new AvailableDatesForAccommodationRenovationView(SelectedAccommodation, AvailableDatesPair);
            view.Show();
            CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(EnterAccommodationRenovationDatesView)) { window.Close(); }
            }
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

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _renovationDuration;
        public int RenovationDuration
        {
            get => _renovationDuration;
            set
            {
                if (value != _renovationDuration)
                {
                    _renovationDuration = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
