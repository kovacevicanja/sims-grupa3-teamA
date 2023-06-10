using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IAccommodationDateService
    {
        void Initialize();
        List<AccommodationDate> GetAll();
        AccommodationDate GetById(int id);
        void Save(List<AccommodationDate> dates);
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
        List<(DateTime, DateTime)> FindAvailableDatesQuick(Accommodation accommodation, int daysToStay);
        List<(DateTime, DateTime)> FindAvailableDatesQuickRanges(Accommodation accommodation, int daysToStay, DateTime initialDate, DateTime endDate);
        bool IsDateAvailable(DateTime date, int daysToStay, List<AccommodationReservation> reservations);
    }
}
