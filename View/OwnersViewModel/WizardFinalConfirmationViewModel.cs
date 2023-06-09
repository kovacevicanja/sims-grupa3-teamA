using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
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
        public RelayCommand FinishComand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public OwnerCustomMessageBox OwnerCustomMessageBox { get; set; }

        public WizardFinalConfirmationViewModel(Accommodation forwardedAcc, NavigationService navigationService) {
            NavigationService = navigationService;
            SelectedAccommodation = forwardedAcc;
            AccommodationController = new AccommodationController();
            FinishComand = new RelayCommand(Button_Click_Finish, CanExecute);
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            OwnerCustomMessageBox = new OwnerCustomMessageBox();
        }
        private bool CanExecute(object param) { return true; }
        private void Button_Click_Finish(object param)
        {           
                AccommodationController.Create(SelectedAccommodation);
                OwnerCustomMessageBox.ShowCustomMessageBox("You have succesfully added new accommodation");
                //var view = new OwnerssView();
                //view.Show();
                NavigationService.Navigate(new OwnerssView(NavigationService));
        }
        private void Button_Click_Back(object param)
        {
            NavigationService.GoBack();
        }
    }
}
