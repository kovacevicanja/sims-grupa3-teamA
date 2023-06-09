using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
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
    public class ChoseAccommodationForRenovationViewModel
    {
        public NavigationService NavigationService { get; set; }
        public RelayCommand BackCommand { get; set; }
        private AccommodationController _accommodationController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public ChoseAccommodationForRenovationViewModel(NavigationService navigationService)
        {
            
            NavigationService = navigationService;
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            _accommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllForOwner(SignInForm.LoggedInUser.Id));
        }
        private bool CanExecute(object param) { return true; }

        private void Button_Click_Back(object param)
        {
            NavigationService.GoBack();
        }
    }
}
