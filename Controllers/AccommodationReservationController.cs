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
        public AccommodationRenovationController _renovationController { get; set; }
        public UserController _userController { get; set; }
        public AccommodationReservationController()
        {
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _renovationController = new AccommodationRenovationController();
            _userController = new UserController();
        }
        public List<Tuple<DateTime, DateTime>> FindAvailableDates(DateTime startDate, DateTime endDate, int duration, Accommodation selectedAccommodation)
        {
            List<DateTime> reservedDates = FindDatesThatAreNotAvailable(selectedAccommodation);
            List<DateTime> renovationDates = _renovationController.FindRenovationDates(selectedAccommodation);
            List<DateTime> availableDates = new List<DateTime>();
            List<Tuple<DateTime, DateTime>> availableDatesPair = new List<Tuple<DateTime, DateTime>>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                _renovationController.CheckIfDatesAreAvailable(availableDates, reservedDates, renovationDates, date);
                _renovationController.CheckDatePairExistence(availableDates, availableDatesPair, duration);
            }
            return availableDatesPair;
        }
        public List<AccommodationReservation> GetAllForOwner(int ownerId)
        {
            List<AccommodationReservation> res = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in GetAll())
            {
                if (reservation.Accommodation.Owner.Id == ownerId)
                {
                    res.Add(reservation);
                }
            }
            return res;
        }
        public List<Location> GetPopularLocations()
        {
            List<AccommodationReservation> reservations = GetAllForOwner(_userController.GetLoggedUser().Id);
            // Group reservations by location and calculate the number of reservations and occupancy percentage for each location
            var locationStats = reservations
                .GroupBy(r => r.Accommodation.Location)
                .Select(g => new
                {
                    Location = g.Key,
                    ReservationCount = g.Count()
                })
                .OrderByDescending(ls => ls.ReservationCount);

            // Retrieve the locations with the highest number of reservations
            var popularLocations = locationStats
                .Where(ls => ls.ReservationCount == locationStats.First().ReservationCount)
                .Select(ls => ls.Location)
                .ToList();

            return popularLocations;
        }
        public List<Location> GetUnPopularLocations()
        {
            List<AccommodationReservation> reservations = GetAllForOwner(_userController.GetLoggedUser().Id);
            // Group reservations by location and calculate the number of reservations and occupancy percentage for each location
            var locationStats = reservations
                .GroupBy(r => r.Accommodation.Location)
                .Select(g => new
                {
                    Location = g.Key,
                    ReservationCount = g.Count()
                })
                .OrderBy(ls => ls.ReservationCount);

            // Retrieve the locations with the highest number of reservations
            var popularLocations = locationStats
                .Where(ls => ls.ReservationCount == locationStats.First().ReservationCount)
                .Select(ls => ls.Location)
                .ToList();

            return popularLocations;
        }
        public void Create(AccommodationReservation reservation)
        {
            _accommodationReservationService.Create(reservation);
        }
        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservationService.GetAll();
        }
        public AccommodationReservation GetById(int id)
        {
            return _accommodationReservationService.GetById(id);
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
        public void Subscribe(IObserver observer)
        {
            _accommodationReservationService.Subscribe(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            _accommodationReservationService.Unsubscribe(observer);
        }

        public void NotifyObservers()
        {
            _accommodationReservationService.NotifyObservers();
        }
        public void SaveParam(List<AccommodationReservation> reservations)
        {
            _accommodationReservationService.SaveParam(reservations);
        }
        public void Save()
        {
            _accommodationReservationService.Save();
        }
        public int CountReservationsForSpecificYear(int year, int accommodationId)
        {
            return _accommodationReservationService.CountReservationsForSpecificYear(year, accommodationId);
        }
        public int CountCancelledReservationsForSpecificYear(int year, int accommodationId)
        {
            return _accommodationReservationService.CountCancelledReservationsForSpecificYear(year, accommodationId);
        }
        public int CountReservationsForSpecificMonth(int year,int month, int accommodationId)
        {
            return _accommodationReservationService.CountReservationsForSpecificMonth(year,month, accommodationId);
        }
        public int CountCancelledReservationsForSpecificMonth(int year,int month, int accommodationId)
        {
            return _accommodationReservationService.CountCancelledReservationsForSpecificMonth(year,month, accommodationId);
        }
        public int FindTheMostBusyYear(List<AccommodationReservation> reservations)
        {
            return _accommodationReservationService.FindTheMostBusyYear(reservations);
        }
        public List<AccommodationReservation> GetAllNotCancelled()
        {
            return _accommodationReservationService.GetAllNotCancelled();
        }
        public int GetMostBusyMonth(List<AccommodationReservation> reservations, int year)
        {
            return _accommodationReservationService.GetMostBusyMonth(reservations,year);
        }
        public List<DateTime> FindDatesThatAreNotAvailable(Accommodation selectedAccommodation)
        {
            return _accommodationReservationService.FindDatesThatAreNotAvailable(selectedAccommodation);
        }
        public int CountReservationsForSpecificLocation(int locationId)
        {
            return _accommodationReservationService.CountReservationsForSpecificLocation(locationId);
        }
    }
}