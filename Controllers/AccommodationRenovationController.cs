using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class AccommodationRenovationController
    {
        private readonly IAccommodationRenovationService _accommodationRenovationService;
        public AccommodationRenovationController()
        {
            _accommodationRenovationService = Injector.CreateInstance<IAccommodationRenovationService>();
        }
        public void Create(AccommodationRenovation renovation)
        {
            _accommodationRenovationService.Create(renovation);
        }
        public List<AccommodationRenovation> GetAll()
        {
            return _accommodationRenovationService.GetAll();
        }
        public AccommodationRenovation GetById(int id)
        {
            return _accommodationRenovationService.GetById(id);
        }
        public void Save(List<AccommodationRenovation> renovations)
        {
            _accommodationRenovationService.Save(renovations);
        }
        public void SaveRenovation()
        {
            _accommodationRenovationService.SaveRenovation();
        }
        public List<AccommodationRenovation> GetRenovationsInPast()
        {
            return _accommodationRenovationService.GetRenovationsInPast();
        }
        public List<AccommodationRenovation> GetRenovationsInFuture()
        {
            return _accommodationRenovationService.GetRenovationsInFuture();
        }
        public List<Accommodation> FillRenovationData(List<Accommodation> accommodations)
        {
            return _accommodationRenovationService.FillRenovationData(accommodations);
        }
        public List<Tuple<DateTime, DateTime>> FindAvailableDates(DateTime startDate, DateTime endDate, int duration, Accommodation selectedAccommodation)
        {
            return _accommodationRenovationService.FindAvailableDates(startDate, endDate, duration, selectedAccommodation);
        }
        public void CheckDatePairExistence(List<DateTime> availableDates, List<Tuple<DateTime, DateTime>> availableDatesPair, int duration)
        {
            _accommodationRenovationService.CheckDatePairExistence(availableDates, availableDatesPair, duration);
        }
        public Tuple<DateTime, DateTime> FindLastRenovation(Accommodation accommodation)
        {
            return _accommodationRenovationService.FindLastRenovation(accommodation);
        }
        public List<AccommodationRenovation> FindRenovationsForAccommodationId(int id)
        {
            return _accommodationRenovationService.FindRenovationsForAccommodationId(id);
        }
        public List<DateTime> FindDatesThatAreNotAvailable(Accommodation selectedAccommodation)
        {
            return _accommodationRenovationService.FindDatesThatAreNotAvailable(selectedAccommodation);
        }
        public List<DateTime> FindRenovationDates(Accommodation selectedAccommodation)
        {
            return _accommodationRenovationService.FindRenovationDates(selectedAccommodation);
        }
        public void CheckIfDatesAreAvailable(List<DateTime> dates, List<DateTime> reservationDates, List<DateTime> renovationDates, DateTime date)
        {
            _accommodationRenovationService.CheckIfDatesAreAvailable(dates, reservationDates, renovationDates, date);
        }
        public void Delete(AccommodationRenovation accommodationRenovation)
        {
            _accommodationRenovationService.Delete(accommodationRenovation);
        }
        public AccommodationRenovation Update(AccommodationRenovation accommodationRenovation)
        {
            return _accommodationRenovationService.Update(accommodationRenovation);
        }
    }
}
