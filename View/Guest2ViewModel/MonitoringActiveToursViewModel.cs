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
        public RelayCommand LogOutCommand { get; }
        public List<int> ActiveToursIds { get; set; }
        public int GuestId { get; set; }
        public User User { get; set; }
        public MonitoringActiveToursViewModel(Tour tour, List<int> activeToursIds, int guestId)
        {
            ActiveToursIds = activeToursIds;
            Tour = tour;
            GuestId = guestId;
            User = new User();
            KeyPointController = new KeyPointController();

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_LogOut, CanExecute);

            KeyPoints = new ObservableCollection<KeyPoint>(KeyPointController.GetToursKeyPoints(Tour.Id));
        }
        private void Button_Click_LogOut(object param)
        {
            User.Id = GuestId;
            User.IsLoggedIn = false;
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
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
            ActiveToursView activeTours = new ActiveToursView(ActiveToursIds, GuestId);
            activeTours.Show();
            CloseWindow();
        }
    }
}