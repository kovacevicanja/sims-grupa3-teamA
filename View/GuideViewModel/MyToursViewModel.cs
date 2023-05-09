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
using BookingProject.Commands;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace BookingProject.View.GuideViewModel
{
    public class MyToursViewModel: INotifyPropertyChanged
    {
        private TourTimeInstanceController _tourTimeInstanceController;
        private TourStartingTimeController _tourStartingTimeController;
        public ObservableCollection<TourTimeInstance> _instances;
        public RelayCommand CancelCommand { get; }
        public RelayCommand CancelTourCommand { get; }
        public RelayCommand CreateCommand { get; }
        public TourTimeInstance ChosenTour { get; set; }
        public MyToursViewModel()
        {
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourStartingTimeController = new TourStartingTimeController();
            _instances = new ObservableCollection<TourTimeInstance>(FilterTours(_tourTimeInstanceController.GetAll()));
            CancelCommand = new RelayCommand(Button_Click_Close, CanExecute);
            CancelTourCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            CreateCommand = new RelayCommand(Button_Click_Create, CanExecute);
        }
        public List<TourTimeInstance> FilterTours(List<TourTimeInstance> tours)
        {
            List<TourTimeInstance> filteredTours = new List<TourTimeInstance>();
            foreach (TourTimeInstance tour in tours)
            {
                if (tour.State != TourState.CANCELLED)
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_Close(object param)
        {
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            CloseWindow();
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(MyToursWindow)) { window.Close(); }
            }
        }
        private void Button_Click_Cancel(object param)
        {
            if (ChosenTour != null && IsNotLate(ChosenTour))
            {
                TourCancellationWindow tourCancellationWindow = new TourCancellationWindow(ChosenTour);
                tourCancellationWindow.Show();
                CloseWindow();
            }
        }
        private bool IsNotLate(TourTimeInstance tour)
        {
            TourDateTime tourDate = new TourDateTime();
            tourDate = _tourStartingTimeController.GetById(tour.DateId);
            TimeSpan ts = tourDate.StartingDateTime - DateTime.Now;
            if (ts > TimeSpan.FromHours(48))
            {
                return true;
            }
            return false;
        }
        private void Button_Click_Create(object param)
        {
            TourCreationWindow tourCreationWindow = new TourCreationWindow();
            tourCreationWindow.Show();
            CloseWindow();
        }
    }
}
