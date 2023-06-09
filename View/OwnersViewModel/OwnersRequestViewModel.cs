using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingProject.View.OwnerViewModel
{
    public class OwnersRequestViewModel
    {
        private RequestAccommodationReservationController _requestController;
        public ObservableCollection<RequestAccommodationReservation> Requests { get; set; }
        public ObservableCollection<RequestAccommodationReservation> DisplayedRequests { get; set; }
        public RequestAccommodationReservation SelectedMovingRequest { get; set; }
        public RelayCommand ViewCommand { get; }
        public RelayCommand MenuCommand { get; }
        public NavigationService NavigationService { get; set; }
        public OwnerNotificationCustomBox box { get; set; }
        public OwnersRequestViewModel(NavigationService navigationService)
        {
            _requestController = new RequestAccommodationReservationController();
            int ownerId = SignInForm.LoggedInUser.Id;                                                                                                  
            DisplayedRequests = new ObservableCollection<RequestAccommodationReservation>(_requestController.GetAllRequestForOwner(ownerId));
            Requests = new ObservableCollection<RequestAccommodationReservation>();
            box = new OwnerNotificationCustomBox();
            foreach(RequestAccommodationReservation r in DisplayedRequests)
            {
                if (r.AccommodationReservation.InitialDate > DateTime.Now)
                {
                    Requests.Add(r);
                }
            }
            ViewCommand = new RelayCommand(Button_Click_View, CanExecute);
            MenuCommand = new RelayCommand(Button_Click_Menu, CanExecute);
            NavigationService = navigationService;
        }
        private bool CanExecute(object param) { return true; }
        private void Button_Click_View(object param)
        {
            if (SelectedMovingRequest == null)
            {
                box.ShowCustomMessageBox("You need to select a request you want to see!");
                return;
            }
            //OwnersApprovingDenyingRequestView view = new OwnersApprovingDenyingRequestView(SelectedMovingRequest);
            //view.Show();
            //CloseWindow();
            NavigationService.Navigate(new OwnersApprovingDenyingRequestView(SelectedMovingRequest, NavigationService));
        }
        private void Button_Click_Menu(object param)
        {
            //MenuView view = new MenuView();
            //view.Show();
            //CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(OwnersRequestView)) { window.Close(); }
            }
        }
    }
}
