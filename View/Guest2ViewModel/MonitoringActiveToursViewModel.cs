using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest2ViewModel
{
    public class MonitoringActiveToursViewModel
    {
        public Tour Tour { get; set; }
        public KeyPointController KeyPointController { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public RelayCommand CancelCommand { get; }
        public MonitoringActiveToursViewModel(Tour tour)
        {
            Tour = tour;
            KeyPointController = new KeyPointController();

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);

            KeyPoints = new ObservableCollection<KeyPoint>(KeyPointController.GetToursKeyPoints(Tour.Id));
        }

        private bool CanExecute(object param) { return true; }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(MonitoringActiveToursView)) { window.Close(); }
            }
        }

        private void Button_Click_Cancel(object param)
        {
            CloseWindow();
        }
    }
}