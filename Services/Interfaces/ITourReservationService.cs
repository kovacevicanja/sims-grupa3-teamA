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

namespace BookingProject.Services.Interfaces
{
    public interface ITourReservationService
    {
        void Initialize();
        bool BookingSuccess(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest);
        bool GoThroughReservations(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest);
        bool TryReservation(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest);
        void TryToBook(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest);
        void FullyBookedTours(Tour choosenTour, DateTime selectedDate, User guest);
        void SuccessfulReservationMessage(string numberOfGuests, User guest, Tour chosenTour);
        void FreePlaceMessage(int maxGuests);
        List<Tour> GetFilteredTours(Location location, DateTime selectedDate);
        void GoThroughTourDates(Tour tour, DateTime selectedDate);
        void GoThroughBookedToursDates(Tour tour, DateTime selectedDate, TourDateTime tdt);
        List<TourReservation> GetAll();
        TourReservation GetByID(int id);
        List<TourReservation> GetUserReservations(int guestId);
    }
}