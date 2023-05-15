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
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class SecondGuestMyReservationsViewModel : INotifyPropertyChanged
    {
        private TourReservationController _tourReservationController;
        public ObservableCollection<TourReservation> MyReservations { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SeeMoreCommand { get; set; }
        public int GuestId { get; set; }
        public NavigationService NavigationService { get; set; }
        public TourReservation ChosenReservation { get; set; }

        public SecondGuestMyReservationsViewModel(int guestId, NavigationService navigationService)
        {
            GuestId = guestId;
            _tourReservationController = new TourReservationController();
            MyReservations = new ObservableCollection<TourReservation>(_tourReservationController.GetUserReservations(guestId));
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
            SeeMoreCommand = new RelayCommand(Button_SeeMore, CanWhenSelected);

            NavigationService = navigationService;
        }

        private bool CanExecute(object param) { return true; } 

        private bool CanWhenSelected(object param) 
        {
            if (ChosenReservation == null) return false;
            else return true;   
        }

        private void Button_SeeMore(object param)
        {
            NavigationService.Navigate(new SeeMoreAboutTourView(ChosenReservation.Tour, GuestId, NavigationService));
        }

        private void Button_Cancel(object param)
        {
            NavigationService.GoBack();
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