using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.OwnerViewModel
{
    public class OwnersRequestViewModel
    {
        private RequestAccommodationReservationController _requestController;
        public ObservableCollection<RequestAccommodationReservation> Requests { get; set; }
        public RequestAccommodationReservation SelectedMovingRequest { get; set; }
        public RelayCommand ViewCommand { get; }
        public RelayCommand MenuCommand { get; }
        public OwnersRequestViewModel()
        {
            _requestController = new RequestAccommodationReservationController();
            int ownerId = SignInForm.LoggedInUser.Id;                                                                                                  
            Requests = new ObservableCollection<RequestAccommodationReservation>(_requestController.GetAllRequestForOwner(ownerId));
            ViewCommand = new RelayCommand(Button_Click_View, CanExecute);
            MenuCommand = new RelayCommand(Button_Click_Menu, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void Button_Click_View(object param)
        {
            if (SelectedMovingRequest == null)
            {
                return;
            }
            OwnersApprovingDenyingRequestView view = new OwnersApprovingDenyingRequestView(SelectedMovingRequest);
            view.Show();
        }
        private void Button_Click_Menu(object param)
        {
            MenuView view = new MenuView();
            view.Show();
        }
    }
}
