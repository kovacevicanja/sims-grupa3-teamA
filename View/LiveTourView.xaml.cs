using BookingProject.Controller;
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

namespace BookingProject.View
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
        private ObservableCollection<KeyPoint> _keyPoints;
        public LiveTourView(TourTimeInstance chosenTour)
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            _tourTimeInstanceController = app.TourTimeInstanceController;
            _keyPointController = new KeyPointController();
            _keyPoints = new ObservableCollection<KeyPoint>(chosenTour.Tour.KeyPoints);
            initState();
            KeyPointDataGrid.ItemsSource = _keyPoints;
            ChosenTour= chosenTour;
            if (_keyPoints.Last().State == KeyPointState.CURRENT) 
            { 
            IsValid = true;
            } 
            else
            {
                IsValid = false;
            }
        }



        public void initState()
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

        }

        public void currentState()
        {
            foreach(KeyPoint keyPoint in _keyPoints) 
            {
                if (keyPoint.Id == ChosenKeyPoint.Id && keyPoint.State==KeyPointState.EMPTY)
                {
                    keyPoint.State = KeyPointState.CURRENT;
                }
            
            }
        }

        public void passedState()
        {
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                if (keyPoint.State == KeyPointState.CURRENT && keyPoint != _keyPoints[_keyPoints.Count - 1])
                {
                    keyPoint.State = KeyPointState.PASSED;
                }

            }


        }


        private void Button_Click_Mark(object sender, RoutedEventArgs e)
        {
            if (ChosenKeyPoint != null)
            {
                passedState();
                currentState();
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

        private void Button_Click_Cancell(object sender, RoutedEventArgs e)
        {
            _keyPointController.Save();
            LiveToursList liveTourList = new LiveToursList();
            tourCancellation();
            liveTourList.Show();
            Close();

        }

        public void tourEnding()
        {
            _tourTimeInstanceController.GetByID(ChosenTour.Id).State = TourState.COMPLETED;
            _tourTimeInstanceController.Save();

        }

        public void tourCancellation()
        {
            _tourTimeInstanceController.GetByID(ChosenTour.Id).State = TourState.CANCELLED;
            _tourTimeInstanceController.Save();

        }


        private void Button_Click_End(object sender, RoutedEventArgs e)
        {
            _keyPointController.Save();
            LiveToursList liveTourList = new LiveToursList();
            tourEnding();
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
