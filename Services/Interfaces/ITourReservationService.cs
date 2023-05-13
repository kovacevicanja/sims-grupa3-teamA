using BookingProject.Model;
using BookingProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Navigation;

namespace BookingProject.Services.Interfaces
{
    public interface ITourReservationService
    {
        void Initialize();
        bool BookingSuccess(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest, NavigationService navigationService);
        bool GoThroughReservations(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest, NavigationService navigationService);
        bool TryReservation(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest);
        void TryToBook(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest, NavigationService navigationService);
        void FullyBookedTours(Tour choosenTour, DateTime selectedDate, User guest, NavigationService navigationService);
        void SuccessfulReservationMessage(string numberOfGuests, User guest, Tour chosenTour, NavigationService navigationService);
        void FreePlaceMessage(int maxGuests);
        List<TourReservation> GetAll();
        TourReservation GetById(int id);
        List<TourReservation> GetUserReservations(int guestId);
    }
}