using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IAccommodationRenovationService
    {
        void Create(AccommodationRenovation renovation);
        List<AccommodationRenovation> GetAll();
        AccommodationRenovation GetById(int id);
        void Initialize();
        void Save(List<AccommodationRenovation> renovations);
        void SaveRenovation();
        List<AccommodationRenovation> GetRenovationsInPast();
        List<AccommodationRenovation> GetRenovationsInFuture();
        List<Accommodation> FillRenovationData(List<Accommodation> accommodations);
        List<Tuple<DateTime, DateTime>> FindAvailableDates(DateTime startDate, DateTime endDate, int duration, Accommodation selectedAccommodation);
        void CheckDatePairExistence(List<DateTime> availableDates, List<Tuple<DateTime, DateTime>> availableDatesPair, int duration);
        Tuple<DateTime, DateTime> FindLastRenovation(Accommodation accommodation);
        List<AccommodationRenovation> FindRenovationsForAccommodationId(int id);
        List<DateTime> FindDatesThatAreNotAvailable(Accommodation selectedAccommodation);
        List<DateTime> FindRenovationDates(Accommodation selectedAccommodation);
        void CheckIfDatesAreAvailable(List<DateTime> dates, List<DateTime> reservationDates, List<DateTime> renovationDates, DateTime date);
        void Delete(AccommodationRenovation accommodationRenovation);
        AccommodationRenovation Update(AccommodationRenovation accommodationRenovation);
    }
}
