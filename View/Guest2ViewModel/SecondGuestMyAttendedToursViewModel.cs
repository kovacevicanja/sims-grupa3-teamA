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
using BookingProject.Commands;
using BookingProject.View.Guest2View;

namespace BookingProject.View.Guest2ViewModel
{
    public class SecondGuestMyAttendedToursViewModel
    {
        public Tour ChosenTour { get; set; }
        public User Guest;
        public ObservableCollection<Tour> AttendedTours { get; set; }
        private TourPresenceController _tourPresenceController { get; set; }
        public UserController UserController { get; set; }
        public int GuestId { get; set; }
        public RelayCommand RateCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand SeeMoreCommand { get; }
        public SecondGuestMyAttendedToursViewModel(int guestId)
        {
            Guest = new User();
            UserController = new UserController();
            GuestId = guestId;
            Guest = UserController.GetById(GuestId);
            Guest.MyTours = UserController.GetById(GuestId).MyTours;
            _tourPresenceController = new TourPresenceController();
            AttendedTours = new ObservableCollection<Tour>(_tourPresenceController.FindAttendedTours(Guest));

            RateCommand = new RelayCommand(Button_Rate, CanWhenSelected);
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
            SeeMoreCommand = new RelayCommand(Button_Click_SeeMore, CanWhenSelected);
        }

        private bool CanWhenSelected (object param)
        {
            if (ChosenTour == null) { return false; }
            else { return true; }
        }

        private void Button_Click_SeeMore(object param)
        {
            SeeMoreAboutTourView seeMore = new SeeMoreAboutTourView(ChosenTour, GuestId, "MyAttendedTours");
            seeMore.Show();
            CloseWindow();
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(SecondGuestMyAttendedToursView)) { window.Close(); }
            }
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Cancel(object param)
        {
            CloseWindow();
        }

        private void Button_Rate(object param)
        {
            if (ChosenTour != null)
            {
                ToursAndGuidesEvaluationView toursAndGuidesEvaluationView = new ToursAndGuidesEvaluationView(ChosenTour, GuestId);
                toursAndGuidesEvaluationView.Show();
                CloseWindow();
            }
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