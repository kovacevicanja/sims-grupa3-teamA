using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class BookingCalendarViewModel
    {
        public Accommodation SelectedAccommodation { get; set; }
        public RelayCommand BackCommand { get; set; }

        public NavigationService NavigationService { get; set; }
        private AccommodationReservationController _accommodationController;
        public ObservableCollection<AccommodationReservation> Accommodations { get; set; }
        public ObservableCollection<AccommodationReservation> UpcomingReservations { get; set; }

        public BookingCalendarViewModel(Accommodation selectedAccommodation, NavigationService navigationService)
        {
            SelectedAccommodation = selectedAccommodation;
            NavigationService = navigationService;
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            _accommodationController = new AccommodationReservationController();
            Accommodations = new ObservableCollection<AccommodationReservation>(_accommodationController.GetAll());
            UpcomingReservations = new ObservableCollection<AccommodationReservation>();
            foreach (AccommodationReservation a in Accommodations)
            {
                if(a.InitialDate > DateTime.Now)
                {
                    UpcomingReservations.Add(a);
                }
            }

        }
        private void Button_Click_Back(object param)
        {
            NavigationService.GoBack();
        }
        private bool CanExecute(object param) { return true; }
        
    }
}
