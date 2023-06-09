using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class OwnerSuggestionsViewModel
    {
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<Location> Locations2 { get; set; }
        public AccommodationReservationController ReservationController { get; set; }
        public AccommodationController _accommodationController { get; set; }
        public NavigationService NavigationService { get; set; }
        public ObservableCollection<int> numberOfRes { get; set; }
        public ObservableCollection<int> numberOfRes2 { get; set; }
        public ObservableCollection<Accommodation> Accommodation { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public UserController _userController { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public RelayCommand AddCommand { get; set; }
        public OwnerSuggestionsViewModel(NavigationService navigationService)
        {
            NavigationService = navigationService;
            ReservationController= new AccommodationReservationController();
            _accommodationController = new AccommodationController();
            numberOfRes = new ObservableCollection<int>();
            numberOfRes2 = new ObservableCollection<int>();
            Locations = new ObservableCollection<Location>(ReservationController.GetPopularLocations());
            Locations2 = new ObservableCollection<Location>(ReservationController.GetUnPopularLocations());
            AddCommand = new RelayCommand(Button_Click_Add, CanExecute);
            foreach(Location location in Locations)
            {
                numberOfRes.Add(ReservationController.CountReservationsForSpecificLocation(location.Id));
            }
            foreach(Location location in Locations2)
            {
                numberOfRes2.Add(ReservationController.CountReservationsForSpecificLocation(location.Id));
            }
            Accommodation = new ObservableCollection<Accommodation>(_accommodationController.GetAllForOwner(SignInForm.LoggedInUser.Id));
            Accommodations = new ObservableCollection<Accommodation>();
            foreach(Accommodation a in Accommodation){
                foreach(Location l in Locations2)
                {
                    if (a.IdLocation == l.Id)
                    {
                        Accommodations.Add(a);
                    }
                }
                
            }
        }
        private bool CanExecute(object param) { return true; }
        public void Button_Click_Add(object param)
        {
            NavigationService.Navigate(new AddAccommodationView(NavigationService));
        }
    }
}
