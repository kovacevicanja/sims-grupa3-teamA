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
    public class ChoseAccommodationForStatisticsViewModel
    {
        public NavigationService NavigationService { get; set; }
        private AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public ChoseAccommodationForStatisticsViewModel(NavigationService navigationService)
        {

            NavigationService = navigationService;
            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllForOwner(SignInForm.LoggedInUser.Id));
        }
    }
}
