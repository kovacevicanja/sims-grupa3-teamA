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
using BookingProject.View.GuideView;
using BookingProject.View.Guest2View;

namespace BookingProject.View.Guest2ViewModel
{
    public class ActiveToursViewModel
    {
        public ObservableCollection<Tour> ActiveToursCollection { get; set; }
        public TourController TourController { get; set; }
        public List<Tour> Tours { get; set; }
        public List<int> ActiveToursIds { get; set; }
        public Tour ChosenTour { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand FollowTourCommand { get; }

        public ActiveToursViewModel(List<int> activeToursIds)
        {
            ActiveToursIds = activeToursIds;
            List<Tour> activeTours = new List<Tour>();

            TourController = new TourController();
            Tours = TourController.GetAll();

            foreach (Tour tour in Tours)
            {
                foreach (int id in ActiveToursIds)
                {
                    if (tour.Id == id)
                    {
                        activeTours.Add(tour);
                    }
                }
            }

            ActiveToursCollection = new ObservableCollection<Tour>(activeTours);

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            FollowTourCommand = new RelayCommand(Button_Click_FollowTour, CanExecute);
        }

        private bool CanExecute(object param) { return true; }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ActiveToursView)) { window.Close(); }
            }
        }

        public void Button_Click_Cancel(object param)
        {
            CloseWindow();
        }

        private void Button_Click_FollowTour(object param)
        {
            MonitoringActiveToursView monitoringActiveToursView = new MonitoringActiveToursView(ChosenTour);
            monitoringActiveToursView.Show();
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