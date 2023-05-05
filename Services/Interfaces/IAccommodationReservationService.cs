using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.ConversionHelp;
using BookingProject.Domain;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IAccommodationReservationService
    {
        void Initialize();
        void Create(AccommodationReservation reservation);
        List<AccommodationReservation> GetAll();
        AccommodationReservation GetById(int id);
        List<AccommodationReservation> getReservationsForGuest(User loggedInUser);
        void Update(AccommodationReservation reservation);
        bool IsReservationAvailable(AccommodationReservation accommodationReservation);
        List<AccommodationReservation> GetAllNotGradedReservations(int ownerId);
        bool CheckNumberOfGuests(Accommodation selectedAccommodation, string numberOfGuests);
        void BookAccommodation(DateTime initialDate, DateTime endDate, Accommodation selectedAccommodation);
        bool PermissionToRate(AccommodationReservation accommodationReservation);
        bool PermissionToCancel(AccommodationReservation accommodationReservation);
        bool DatesOverlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2);
        void RemoveCurrentReservation(List<AccommodationReservation> allReservations, RequestAccommodationReservation request);
        bool IsAvailableToMove(RequestAccommodationReservation request);
        void DeleteReservationFromCSV(AccommodationReservation accommmodationReservation);
        void Subscribe(IObserver observer);
        void SaveParam(List<AccommodationReservation> reservations);
        void Save();
    }
}
