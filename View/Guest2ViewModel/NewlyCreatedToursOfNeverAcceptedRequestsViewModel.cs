using BookingProject.Controller;
using BookingProject.Model;
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
        public NewlyCreatedToursOfNeverAcceptedRequestsViewModel(int guestId, NavigationService navigationService)
        {
            GuestId = guestId;
            NavigationService = navigationService;
            TourController = new TourController(); 
            NewlyCreatedTours = new ObservableCollection<Tour>(TourController.FindToursCreatedByStatistcisForGuest(GuestId).Distinct());
        }
    }
}