using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.OwnersView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class WelcomeToBookingViewMode
    {
        public NavigationService NavigationService { get; set; }

        public RelayCommand AddAccommodationCommand { get; }
        public RelayCommand SkipCommand { get; }
        public WelcomeToBookingViewMode(NavigationService navigationService)
        {
            NavigationService = navigationService;
            AddAccommodationCommand = new RelayCommand(Button_Click_Add, CanExecute);
            SkipCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void Button_Click_Add(object param)
        {
            NavigationService.Navigate(new WizardAddAccommodationView(NavigationService));
        }
        
        private void Button_Click_Cancel(object param)
        {
            NavigationService.Navigate(new OwnerssView(NavigationService));
        }
    }
}
