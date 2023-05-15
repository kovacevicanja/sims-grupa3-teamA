using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model;
using BookingProject.View.GuideView;
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

namespace BookingProject.View.GuideViewModel
{
    public class LiveToursListViewModel
    {
        private TourTimeInstanceController _tourTimeInstanceController;
        private TourStartingTimeController _tourStartingTimeController;
        private UserController _userController;
        public ObservableCollection<TourTimeInstance> _instances;
        public TourTimeInstance ChosenTour { get; set; }

        public RelayCommand CancelCommand { get; }
        public RelayCommand EnterCommand { get; }
        public LiveToursListViewModel()
        {
            _userController= new UserController();  
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourStartingTimeController = new TourStartingTimeController();
            _instances = new ObservableCollection<TourTimeInstance>(FilterTours(_tourTimeInstanceController.GetAll()));
            CancelCommand = new RelayCommand(Button_Click_Home, CanExecute);
            EnterCommand = new RelayCommand(Button_Click_Start, CanExecute);
        }
        private TourState _state;
        public TourState State
        {
            get => _state;
            set
            {
                if (value != _state)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(LiveToursList)) { window.Close(); }
            }
        }
        public List<TourTimeInstance> FilterTours(List<TourTimeInstance> tours)
        {
            List<TourTimeInstance> filteredTours = new List<TourTimeInstance>();
            List<TourTimeInstance> exceptionFilteredTours = new List<TourTimeInstance>();
            foreach (TourTimeInstance tour in tours)
            {
                if (tour.State == TourState.STARTED)
                {
                    exceptionFilteredTours.Add(tour);
                    return exceptionFilteredTours;
                }


                if (TodayCheck(tour))
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }
        public bool TodayCheck(TourTimeInstance tour)
        {
            TourDateTime tourDate = new TourDateTime();
            tourDate = _tourStartingTimeController.GetById(tour.DateId);
            if (tourDate == null)
            {
                return false;
            }
            if (tourDate.StartingDateTime.Date == DateTime.Now.Date && tour.State != TourState.COMPLETED && tour.State != TourState.CANCELLED && tour.Tour.GuideId == _userController.GetLoggedUser().Id)
            {
                return true;
            }
            return false;
        }

        private void Button_Click_Start(object param)
        {
            if (ChosenTour != null)
            {
                LiveTourView liveTourView = new LiveTourView(ChosenTour);
                liveTourView.Show();
                CloseWindow();
            }
        }
        private void Button_Click_Home(object param)
        {
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            CloseWindow();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

