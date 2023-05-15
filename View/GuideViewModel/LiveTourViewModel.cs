using BookingProject.Controller;
using BookingProject.Domain;
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
    public class LiveTourViewModel
    {
        public bool IsValid { get; set; }
        public KeyPoint ChosenKeyPoint { get; set; }
        public TourTimeInstance ChosenTour { get; set; }
        private TourTimeInstanceController _tourTimeInstanceController;
        private KeyPointController _keyPointController;
        private UserController _userControler;
        public ObservableCollection<KeyPoint> _keyPoints;
        private TourPresenceController _tourPresenceController;
        public RelayCommand CancelCommand { get; }
        public RelayCommand MarkCommand { get; }
        public RelayCommand EndCommand { get; }
        public LiveTourViewModel(TourTimeInstance chosenTour)
        {
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourPresenceController = new TourPresenceController();
            _keyPointController = new KeyPointController();
            _userControler = new UserController();
            _keyPoints = new ObservableCollection<KeyPoint>(chosenTour.Tour.KeyPoints);
            InitState();
            ChosenTour = chosenTour;
            TourStarting();
            SaveStates();
            SavePresence();
            if (_keyPoints.Last().State == KeyPointState.CURRENT)
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
            CancelCommand = new RelayCommand(Button_Click_Cancell, CanExecute);
            MarkCommand = new RelayCommand(Button_Click_Mark, CanExecute);
            EndCommand = new RelayCommand(Button_Click_End, CanExecute);
        }
        public void InitState()
        {
            bool initState = true;
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                if (keyPoint.State != KeyPointState.EMPTY)
                {
                    _keyPoints[0].State = KeyPointState.PASSED;
                    initState = false;
                }
            }
            if (initState)
            {
                _keyPoints[0].State = KeyPointState.CURRENT;
            }
            _keyPointController.Save();
        }
        public void CurrentState()
        {
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                if (keyPoint.Id == ChosenKeyPoint.Id && keyPoint.State == KeyPointState.EMPTY)
                {
                    keyPoint.State = KeyPointState.CURRENT;
                }
            }
            _keyPointController.Save();
        }
        public void PassedState()
        {
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                if (keyPoint.State == KeyPointState.CURRENT && keyPoint != _keyPoints[_keyPoints.Count - 1])
                {
                    keyPoint.State = KeyPointState.PASSED;
                }
            }
            _keyPointController.Save();
        }
        private void Button_Click_Mark(object param)
        {
            if (ChosenKeyPoint != null)
            {
                PassedState();
                CurrentState();
                if (_keyPoints.Last().State == KeyPointState.CURRENT) { IsValid = true; }
                _keyPointController.Save();
                GuestListView guestListView = new GuestListView(ChosenTour, ChosenKeyPoint);
                guestListView.Show();
                CloseWindow();
            }
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(LiveTourView)) { window.Close(); }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SaveStates()
        {
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                _keyPointController.GetById(keyPoint.Id).State = keyPoint.State;
            }
            _keyPointController.Save();
        }
        public void RevertStates()
        {
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                _keyPointController.GetById(keyPoint.Id).State = KeyPointState.EMPTY;
            }
            _keyPointController.Save();
        }
        public void RevertUsers()
        {
            foreach (User user in _userControler.GetAll())
            {
                _userControler.GetById(user.Id).IsPresent = false;
            }
            _userControler.Save();
        }
        private void Button_Click_Cancell(object param)
        {
            SaveStates();
            LiveToursList liveTourList = new LiveToursList();
            liveTourList.Show();
            CloseWindow();
        }
        public void TourEnding()
        {
            _tourTimeInstanceController.GetById(ChosenTour.Id).State = TourState.COMPLETED;
            _tourTimeInstanceController.Save();
        }
        public void TourStarting()
        {
            _tourTimeInstanceController.GetById(ChosenTour.Id).State = TourState.STARTED;
            _tourTimeInstanceController.Save();

        }
        public bool PresenceCheck(TourPresence presence)
        {
            if (_userControler.GetById(presence.UserId).IsPresent && (presence.KeyPointId == -1))
            {
                return true;
            }
            return false;
        }
        public void SavePresence()
        {
            foreach (TourPresence presence in _tourPresenceController.GetAll())
            {
                if (PresenceCheck(presence) && _keyPointController.GetCurrentKeyPoint() != null)
                {
                    _tourPresenceController.GetById(presence.Id).KeyPointId = _keyPointController.GetCurrentKeyPoint().Id;
                }
            }
            _tourPresenceController.Save();
        }
        private void Button_Click_End(object param)
        {
            if (!IsValid) { return; }
            TourEnding();
            RevertStates();
            RevertUsers();
            LiveToursList liveTourList = new LiveToursList();
            liveTourList.Show();
            CloseWindow();
        }

    }
}
