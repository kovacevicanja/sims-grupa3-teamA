using BookingProject.Controllers;
using BookingProject.ConversionHelp;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookingProject.View.FindAvailableDatesForAccommodation;

namespace BookingProject.Controller
{
    public class AccommodationReservationController 
    {
        private readonly IAccommodationReservationService _accommodationReservationService;
        public AccommodationReservationController()
        {
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
        }
        public void Create(AccommodationReservation reservation)
        {
            _accommodationReservationService.Create(reservation);
        }
        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservationService.GetAll();
        }
        public AccommodationReservation GetByID(int id)
        {
            return _accommodationReservationService.GetByID(id);
        }
        public List<AccommodationReservation> getReservationsForGuest(User loggedInUser)
        {
            return _accommodationReservationService.getReservationsForGuest(loggedInUser);
        }
        public void Update(AccommodationReservation reservation)
        {
            _accommodationReservationService.Update(reservation);
        }
        public bool IsReservationAvailable(AccommodationReservation accommodationReservation)
        {
            return _accommodationReservationService.IsReservationAvailable(accommodationReservation);
        }
        public List<AccommodationReservation> GetAllNotGradedReservations(int ownerId)
        {
            return _accommodationReservationService.GetAllNotGradedReservations(ownerId);
        }
        public bool CheckNumberOfGuests(Accommodation selectedAccommodation, string numberOfGuests)
        {
            return _accommodationReservationService.CheckNumberOfGuests(selectedAccommodation,numberOfGuests);
        }
        public bool CheckEnteredDates(DateTime initialDate, DateTime endDate)
        {
            return _accommodationReservationService.CheckEnteredDates(initialDate, endDate);
        }
        public bool CompareWithToday(DateTime initialDate)
        {
            return _accommodationReservationService.CompareWithToday(initialDate);
        }
        public bool CheckDays(DateTime initialDate, DateTime endDate)
        {
            return _accommodationReservationService.CheckDays(initialDate, endDate);
        }
        public bool CheckAvailableDate(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay, string numberOfGuests)
        {
            return _accommodationReservationService.CheckAvailableDate(selectedAccommodation, initialDate, endDate, numberOfDaysToStay, numberOfGuests);
        }
        public List<AccommodationReservation> GetReservationsForAccommodation(Accommodation accommodation)
        {
            return _accommodationReservationService.GetReservationsForAccommodation(accommodation);
        }
        public List<DateTime> MakeListOfReservedDates(DateTime initialDate, DateTime endDate)
        {
            return _accommodationReservationService.MakeListOfReservedDates(initialDate, endDate);
        }
        public bool IsDateReserved(List<DateTime> tryDates, List<AccommodationReservation> reservations)
        {
            return _accommodationReservationService.IsDateReserved(tryDates,reservations);
        }
        public List<(DateTime, DateTime)> FindAvailableDates(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay)
        {
            return _accommodationReservationService.FindAvailableDates(selectedAccommodation,initialDate,endDate,numberOfDaysToStay);
        }
        public bool IfDatesAreInTakenList(List<DateTime> datesToCheck, List<DateTime> takenDates)
        {
            return _accommodationReservationService.IfDatesAreInTakenList(datesToCheck,takenDates);
        }
        public List<DateTime> FindTakenDates(Accommodation selectedAccommodation)
        {
            return _accommodationReservationService.FindTakenDates(selectedAccommodation);
        }
        public void BookAccommodation(DateTime initialDate, DateTime endDate, Accommodation selectedAccommodation)
        {
            _accommodationReservationService.BookAccommodation(initialDate,endDate, selectedAccommodation);
        }
        public bool PermissionToRate(AccommodationReservation accommodationReservation)
        {
            return _accommodationReservationService.PermissionToRate(accommodationReservation);
        }
        public bool PermissionToCancel(AccommodationReservation accommodationReservation)
        {
            return _accommodationReservationService.PermissionToCancel(accommodationReservation);
        }
        public bool DatesOverlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return _accommodationReservationService.DatesOverlaps(start1, end1, start2, end2);
        }
        public void RemoveCurrentReservation(List<AccommodationReservation> allReservations, RequestAccommodationReservation request)
        {
            _accommodationReservationService.RemoveCurrentReservation(allReservations, request);
        }
        public bool IsAvailableToMove(RequestAccommodationReservation request)
        {
            return _accommodationReservationService.IsAvailableToMove(request);
        }
        public void DeleteReservationFromCSV(AccommodationReservation accommmodationReservation)
        {
            _accommodationReservationService.DeleteReservationFromCSV(accommmodationReservation);
        }
        public void SendNotification(AccommodationReservation accommodationReservation)
        {
            _accommodationReservationService.SendNotification(accommodationReservation);
        }
        public List<Notification> GetOwnerNotifications(User owner)
        {
            return _accommodationReservationService.GetOwnerNotifications(owner);
        }
        public void DeleteNotificationFromCSV(Notification notification)
        {
            _accommodationReservationService.DeleteNotificationFromCSV(notification);
        }
        public void WriteNotificationAgain(Notification n)
        {
            _accommodationReservationService.WriteNotificationAgain(n);
        }
        public void Subscribe(IObserver observer)
        {
            _accommodationReservationService.Subscribe(observer);
        }
        public void SaveParam(List<AccommodationReservation> reservations)
        {
            _accommodationReservationService.SaveParam(reservations);
        }
        public void Save()
        {
            _accommodationReservationService.Save();
        }
    }
}
