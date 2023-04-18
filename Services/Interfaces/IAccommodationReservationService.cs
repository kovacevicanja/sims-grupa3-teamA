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
        AccommodationReservation GetByID(int id);
        List<AccommodationReservation> getReservationsForGuest(User loggedInUser);
        void Update(AccommodationReservation reservation);
        bool IsReservationAvailable(AccommodationReservation accommodationReservation);
        List<AccommodationReservation> GetAllNotGradedReservations(int ownerId);
        bool CheckNumberOfGuests(Accommodation selectedAccommodation, string numberOfGuests);
        bool CheckEnteredDates(DateTime initialDate, DateTime endDate);
        bool CompareWithToday(DateTime initialDate);
        bool CheckDays(DateTime initialDate, DateTime endDate); 
        bool CheckAvailableDate(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay, string numberOfGuests);
        List<AccommodationReservation> GetReservationsForAccommodation(Accommodation accommodation);
        List<DateTime> MakeListOfReservedDates(DateTime initialDate, DateTime endDate);
        bool IsDateReserved(List<DateTime> tryDates, List<AccommodationReservation> reservations);
        List<(DateTime, DateTime)> FindAvailableDates(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay);
        bool IfDatesAreInTakenList(List<DateTime> datesToCheck, List<DateTime> takenDates);
        List<DateTime> FindTakenDates(Accommodation selectedAccommodation);
        void BookAccommodation(DateTime initialDate, DateTime endDate, Accommodation selectedAccommodation);
        bool PermissionToRate(AccommodationReservation accommodationReservation);
        bool PermissionToCancel(AccommodationReservation accommodationReservation);
        bool DatesOverlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2);
        void RemoveCurrentReservation(List<AccommodationReservation> allReservations, RequestAccommodationReservation request);
        bool IsAvailableToMove(RequestAccommodationReservation request);
        void DeleteReservationFromCSV(AccommodationReservation accommmodationReservation);
        void SendNotification(AccommodationReservation accommodationReservation);
        List<Notification> GetOwnerNotifications(User owner);
        void DeleteNotificationFromCSV(Notification notification);
        void WriteNotificationAgain(Notification n);
        void Subscribe(IObserver observer);
    }
}
