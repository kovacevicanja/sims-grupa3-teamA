using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.Guest2View;
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
    public class ReservationTourOtherOffersViewModel
    {
        public ObservableCollection<Tour> Tours { get; set; }
        private TourController _tourController;
        public Tour ChosenTour { get; set; }
        public Tour NewlyChosenTour { get; set; }
        public int GuestId { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand TryToBookCommand { get; }
        public RelayCommand SeeMoreCommand { get; }
        public NavigationService NavigationService { get; set; }
        public ReservationTourOtherOffersViewModel(Tour chosenTour, DateTime selectedDate, int guestId, NavigationService navigationService)
        {
            ChosenTour = chosenTour;
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetFilteredTours(chosenTour.Location, selectedDate));
            GuestId = guestId;

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            TryToBookCommand = new RelayCommand(Button_Click_TryToBook, CanWhenSelected);
            SeeMoreCommand = new RelayCommand(Button_Click_SeeMore, CanWhenSelected);

            NavigationService = navigationService;
        }

        private bool CanWhenSelected(object param)
        {
            if (NewlyChosenTour == null) { return false; }
            else { return true; }
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ReservationTourOtherOffersView)) { window.Close(); }
            }
        }

        private void Button_Click_SeeMore(object param)
        {
            NavigationService.Navigate(new SeeMoreAboutTourView(NewlyChosenTour, GuestId, NavigationService));
        }

        private void Button_Click_Cancel(object param)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_TryToBook(object param)
        {
            NavigationService.Navigate(new ReservationTourView(NewlyChosenTour, GuestId, NavigationService));
        }

        private bool CanExecute(object param) { return true; }
    }
}