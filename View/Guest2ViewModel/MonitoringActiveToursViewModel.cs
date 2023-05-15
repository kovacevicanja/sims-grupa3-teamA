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
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class MonitoringActiveToursViewModel
    {
        public Tour Tour { get; set; }
        public KeyPointController KeyPointController { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public RelayCommand CancelCommand { get; }
        public List<int> ActiveToursIds { get; set; }
        public int GuestId { get; set; }
        public User User { get; set; }
        public NavigationService NavigationService { get; set; }    
        public MonitoringActiveToursViewModel(Tour tour, List<int> activeToursIds, int guestId, NavigationService navigationService)
        {
            ActiveToursIds = activeToursIds;
            Tour = tour;
            GuestId = guestId;
            User = new User();
            KeyPointController = new KeyPointController();

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);

            KeyPoints = new ObservableCollection<KeyPoint>(KeyPointController.GetToursKeyPoints(Tour.Id));

            NavigationService = navigationService;
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Click_Cancel(object param)
        {
           NavigationService.GoBack();
        }
    }
}