using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.View.Guest2View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class TourRequestsViewModel
    {
        public int GuestId { get; set; }
        public TourRequestController _tourRequestController;
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public RelayCommand CancelCommand { get; }
        public NavigationService NavigationService { get; set; }

        public TourRequestsViewModel(int guestId, NavigationService navigationService)
        {
            GuestId = guestId;

            _tourRequestController = new TourRequestController();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestController.GetGuestRequests(guestId, ""));

            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);

            NavigationService = navigationService;
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Cancel(object param)
        {
            NavigationService.GoBack();
        }
    }
}