using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain.Enums;
using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Xml.Linq;
using BookingProject.Commands;
using System.Windows.Navigation;
using BookingProject.View.CustomMessageBoxes;

namespace BookingProject.View.OwnerViewModel
{
    public class OwnersApprovingDenyingRequestViewModel
    {
        public RequestAccommodationReservation SelectedMovingRequest { get; set; }
        public Boolean Availability { get; set; }
        private AccommodationReservationController _reservationController;
        private RequestAccommodationReservationController _movingController;
        public RelayCommand AcceptCommand { get; }
        public RelayCommand DeclineCommand { get; }
        public RelayCommand MenuCommand { get; }
        public RelayCommand BackCommand { get; }
        public NavigationService NavigationService { get; set; }
        public String AvailabilityDisplay { get; set; }
        public OwnerNotificationCustomBox box { get; set; }

        public OwnersApprovingDenyingRequestViewModel(RequestAccommodationReservation selectedMovingRequest, NavigationService navigationService)
        {
            SelectedMovingRequest = selectedMovingRequest;
            _reservationController = new AccommodationReservationController();
            _movingController = new RequestAccommodationReservationController();
            Availability = _reservationController.IsAvailableToMove(selectedMovingRequest);
            if (Availability)
            {
                AvailabilityDisplay = "Available";
            } else
            {
                AvailabilityDisplay = "Not available";
            }
            AcceptCommand = new RelayCommand(Button_Click_Accept, CanExecute);
            DeclineCommand = new RelayCommand(Button_Click_Decline, CanExecute);
            MenuCommand = new RelayCommand(Button_Click_Menu, CanExecute);
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            NavigationService = navigationService;
            box = new OwnerNotificationCustomBox();

        }
        private bool CanExecute(object param) { return true; }

        private void Button_Click_Accept(object param)
        {
            SelectedMovingRequest.Status = RequestStatus.APPROVED;
            if (_movingController.PermissionToAcceptDenyRequest(SelectedMovingRequest))
            {
                //SelectedMovingRequest.Status = RequestStatus.APPROVED;
                _movingController.Update(SelectedMovingRequest);
                _movingController.AcceptRequest(SelectedMovingRequest);
                box.ShowCustomMessageBox("You have moved a reservation from "+SelectedMovingRequest.AccommodationReservation.InitialDate.ToString("MM/dd") + " - "+ SelectedMovingRequest.AccommodationReservation.EndDate.ToString("MM/dd")
                    + " to " + SelectedMovingRequest.NewArrivalDay.ToString("MM/dd") + " - " + SelectedMovingRequest.NewDeparuteDay.ToString("MM/dd"));
                //var view = new OwnersRequestView();
                //view.Show();
                //CloseWindow();
                NavigationService.Navigate(new OwnersRequestView(NavigationService));
            } else
            {
                box.ShowCustomMessageBox("You don't have permission to accept this request!");
                NavigationService.GoBack();
            }
        }
        private void Button_Click_Menu(object param)
        {
            MenuView view = new MenuView();
            view.Show();
            CloseWindow();
        }
        private void Button_Click_Back(object param)
        {
            NavigationService.Navigate(new OwnersRequestView(NavigationService));
            //var view = new OwnersRequestView();
            //view.Show();
            //CloseWindow();
        }
        private void Button_Click_Decline(object param)
        {
            SelectedMovingRequest.Status = RequestStatus.DECLINED;
            if (_movingController.PermissionToAcceptDenyRequest(SelectedMovingRequest))
            {
                _movingController.Update(SelectedMovingRequest);
                NavigationService.GoBack();
            } else
            {
                box.ShowCustomMessageBox("You don't have permission to decline this request!");
                NavigationService.GoBack();
            }
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(OwnersApprovingDenyingRequestView)) { window.Close(); }
            }
        }

        //private void Comment_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (Comment.Text.Length > 0)
        //    {
        //        Button_Comment.IsEnabled = false;
        //    }
        //    else
        //    {
        //        Button_Comment.IsEnabled = true;
        //    }
        //}
    }
}
