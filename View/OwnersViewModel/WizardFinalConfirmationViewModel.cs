using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class WizardFinalConfirmationViewModel
    {
        public Accommodation SelectedAccommodation { get; set; }
        public NavigationService NavigationService { get; set; }
        public WizardFinalConfirmationViewModel(Accommodation forwardedAcc, NavigationService navigationService) {
            NavigationService = navigationService;
            SelectedAccommodation = forwardedAcc;
        }
    }
}
