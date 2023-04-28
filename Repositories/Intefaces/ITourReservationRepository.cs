using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ITourReservationRepository
    {
        void Initialize();
        List<TourReservation> GetAll();
        TourReservation GetById(int id);
        void ReservationGuestBind(int id);
        List<TourReservation> GetUserReservations(int guestId);
        void SaveReservationToFile(Tour choosenTour, string numberOfGuests, DateTime selectedDate, User guest);
        void SaveSameReservationToFile(Tour chosenTour, TourReservation tourReservation, string numberOfGuests, DateTime selectedDate, User guest);
    }
}