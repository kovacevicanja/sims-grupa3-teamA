using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.View.Guest2View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class NewlyCreatedToursOfNeverAcceptedRequestsViewModel
    {
        public int GuestId { get; set; }
        public NavigationService NavigationService { get; set; }
        public ObservableCollection<Tour> NewlyCreatedTours { get; set; }
        public TourController TourController { get; set; }
        public TourRequestController TourRequestController { get; set; } 
        public RelayCommand BookTourCommand { get; }
        public RelayCommand SeeMoreCommand { get; }
        public RelayCommand CancelCommand { get; }
        public Tour ChosenTour { get; set; }
        public NewlyCreatedToursOfNeverAcceptedRequestsViewModel(int guestId, NavigationService navigationService)
        {
            GuestId = guestId;
            NavigationService = navigationService;
            TourController = new TourController();
            TourRequestController = new TourRequestController();
            NewlyCreatedTours = new ObservableCollection<Tour>(TourController.FindToursCreatedByStatistcisForGuest(GuestId).Distinct());

            SeeMoreCommand = new RelayCommand(Button_Click_SeeMore, CanWhenSelected);
            BookTourCommand = new RelayCommand(Button_Click_Book, CanWhenSelected);
            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Click_SeeMore(object param)
        {
            NavigationService.Navigate(new SeeMoreAboutTourView(ChosenTour, GuestId, NavigationService));
        }

        private bool CanWhenSelected(object param)
        {
            if (ChosenTour == null) return false;
            else return true;
        }
        private void Button_Click_Cancel(object param)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Book(object param)
        {
            NavigationService.Navigate(new ReservationTourView(ChosenTour, GuestId, NavigationService));
        }

    }
}