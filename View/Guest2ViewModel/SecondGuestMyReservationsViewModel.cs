using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using BookingProject.View.Guest2View;
using BookingProject.Commands;

namespace BookingProject.View.Guest2ViewModel
{
    public class SecondGuestMyReservationsViewModel : INotifyPropertyChanged
    {
        private TourReservationController _tourReservationController;
        public ObservableCollection<TourReservation> MyReservations { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public int GuestId { get; set; }

        public SecondGuestMyReservationsViewModel(int guestId)
        {
            GuestId = guestId;
            _tourReservationController = new TourReservationController();
            MyReservations = new ObservableCollection<TourReservation>(_tourReservationController.GetUserReservations(guestId));
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(SecondGuestMyReservations)) { window.Close(); }
            }
        }

        private bool CanExecute(object param) { return true; } 

        private void Button_Cancel(object param)
        {
            CloseWindow();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _location;
        public Location Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private LanguageEnum _tourLanguage;
        public LanguageEnum TourLanguage
        {
            get => _tourLanguage;
            set
            {
                if (value != _tourLanguage)
                {
                    _tourLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _durationInHours;
        public double DurationInHours
        {
            get => _durationInHours;
            set
            {
                if (value != _durationInHours)
                {
                    _durationInHours = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<DateTime> _startingTime;
        public List<DateTime> StartingTime
        {
            get => _startingTime;
            set
            {
                _startingTime = value;
                OnPropertyChanged();
            }
        }
    }
}