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

namespace BookingProject.View.Guest2ViewModel
{
    public class TourRequestsViewModel
    {
        public int GuestId { get; set; }
        public TourRequestController _tourRequestController;
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public RelayCommand CancelCommand { get; }

        public TourRequestsViewModel(int guestId)
        {
            GuestId = guestId;

            _tourRequestController = new TourRequestController();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestController.GetGuestRequests(guestId, ""));

            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Cancel(object param)
        {
            CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(TourRequestsView)) { window.Close(); }
            }
        }
    }
}