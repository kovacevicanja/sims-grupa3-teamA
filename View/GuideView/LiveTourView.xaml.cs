using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for LiveTourView.xaml
    /// </summary>
    /// 
    public partial class LiveTourView : Window
    {
        public bool IsValid { get; set; }
        public KeyPoint ChosenKeyPoint { get; set; }
        public TourTimeInstance ChosenTour { get; set; }

        private TourTimeInstanceController _tourTimeInstanceController;
        private KeyPointController _keyPointController;
        private UserController _userControler;
        private ObservableCollection<KeyPoint> _keyPoints;
        private TourPresenceController _tourPresenceController;
        public LiveTourView(TourTimeInstance chosenTour)
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            _tourTimeInstanceController = app.TourTimeInstanceController;
            _tourPresenceController= app.TourPresenceController;
            _keyPointController = app.KeyPointController;
            _userControler = app.UserController;
            _keyPoints = new ObservableCollection<KeyPoint>(chosenTour.Tour.KeyPoints);
            InitState();
            KeyPointDataGrid.ItemsSource = _keyPoints;
            ChosenTour= chosenTour;
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
            foreach(KeyPoint keyPoint in _keyPoints) 
            {
                if (keyPoint.Id == ChosenKeyPoint.Id && keyPoint.State==KeyPointState.EMPTY)
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


        private void Button_Click_Mark(object sender, RoutedEventArgs e)
        {
            if (ChosenKeyPoint != null)
            {
                PassedState();
                CurrentState();
                if(_keyPoints.Last().State == KeyPointState.CURRENT) { IsValid= true; }
                _keyPointController.Save();
                GuestListView guestListView = new GuestListView(ChosenTour, ChosenKeyPoint);
                guestListView.Show();
                Close();

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveStates()
        {
            foreach(KeyPoint keyPoint in _keyPoints)
            {
                _keyPointController.GetByID(keyPoint.Id).State = keyPoint.State;
            }
            _keyPointController.Save();

        }

        public void RevertStates()
        {
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                _keyPointController.GetByID(keyPoint.Id).State = KeyPointState.EMPTY;
            }
            _keyPointController.Save();

        }

        public void RevertUsers()
        {
            foreach (User user in _userControler.GetAll())
            {
                _userControler.GetByID(user.Id).IsPresent = false;
            }
            _userControler.Save();
        }

        private void Button_Click_Cancell(object sender, RoutedEventArgs e)
        {
            SaveStates();
            LiveToursList liveTourList = new LiveToursList();
            liveTourList.Show();
            Close();

        }

        public void TourEnding()
        {
            _tourTimeInstanceController.GetByID(ChosenTour.Id).State = TourState.COMPLETED;
            _tourTimeInstanceController.Save();

        }

        public void TourStarting()
        {
            _tourTimeInstanceController.GetByID(ChosenTour.Id).State = TourState.STARTED;
            _tourTimeInstanceController.Save();

        }

        public bool PresenceCheck(TourPresence presence)
        {
            if(_userControler.GetByID(presence.UserId).IsPresent && (presence.KeyPointId== -1))
            {
                return true;
            }
            return false;
        }


        public void SavePresence()
        {
            foreach(TourPresence presence in _tourPresenceController.GetAll())
            {
                if (PresenceCheck(presence) && _keyPointController.GetCurrentKeyPoint()!=null)
                {
                    _tourPresenceController.GetByID(presence.Id).KeyPointId = _keyPointController.GetCurrentKeyPoint().Id;
                }


            }

            _tourPresenceController.Save();

        }



        private void Button_Click_End(object sender, RoutedEventArgs e)
        {
            TourEnding();
            RevertStates();
            RevertUsers();
            LiveToursList liveTourList = new LiveToursList();
            liveTourList.Show();
            Close();
        }
        private void Window_LayoutUpdated(object sender, System.EventArgs e)
        {
            if (IsValid)
                EndButton.IsEnabled = true;
            else
                EndButton.IsEnabled = false;
        }


    }
}
