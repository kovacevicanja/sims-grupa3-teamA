using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class AccommodationDateService : IAccommodationDateService
    {
        private IAccommodationDateRepository _accommodationDateRepository;
        public AccommodationDateService() { }
        public void Initialize()
        {
            _accommodationDateRepository = Injector.CreateInstance<IAccommodationDateRepository>();
        }

        public List<AccommodationDate> GetAll()
        {
            return _accommodationDateRepository.GetAll();
        }

        public AccommodationDate GetByID(int id)
        {
            return _accommodationDateRepository.GetByID(id);
        }
        public void Save(List<AccommodationDate> dates)
        {
            _accommodationDateRepository.Save(dates);
        }
        public bool CheckEnteredDates(DateTime initialDate, DateTime endDate)
        {
            return initialDate.Month > endDate.Month || endDate.Year < initialDate.Year || !CheckDays(initialDate, endDate) || !CompareWithToday(initialDate);
        }

        public bool CompareWithToday(DateTime initialDate)
        {
            DateTime today = DateTime.Now.Date;
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);
            return initialDate < todayMidnight;
        }

        public bool CheckDays(DateTime initialDate, DateTime endDate)
        {
            return initialDate.Month == endDate.Month && initialDate.Day > endDate.Day;
        }

        public bool CheckAvailableDate(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay, string numberOfGuests)
        {
            List<DateTime> tryDates = MakeListOfReservedDates(initialDate, endDate);
            List<AccommodationReservation> reservations = GetReservationsForAccommodation(selectedAccommodation);
            return !IsDateReserved(tryDates, reservations);
        }

        public List<AccommodationReservation> GetReservationsForAccommodation(Accommodation accommodation)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in Injector.CreateInstance<IAccommodationReservationRepository>().GetAll())
            {
                if (reservation.Accommodation.Id == accommodation.Id)
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public List<DateTime> MakeListOfReservedDates(DateTime initialDate, DateTime endDate)
        {
            List<DateTime> reservedDates = new List<DateTime>();
            for (DateTime date = initialDate; date <= endDate; date = date.AddDays(1))
            {
                reservedDates.Add(date);
            }
            return reservedDates;
        }

        public bool IsDateReserved(List<DateTime> tryDates, List<AccommodationReservation> reservations)
        {
            foreach (AccommodationReservation reservation in reservations)
            {
                List<DateTime> reservedDates = MakeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                if (IfDatesAreInTakenList(tryDates, reservedDates))
                {
                    return true;
                }
            }
            return false;
        }

        public List<(DateTime, DateTime)> FindAvailableDates(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay)
        {
            var takenDates = FindTakenDates(selectedAccommodation).OrderBy(d => d).ToList();
            var availableDates = new List<(DateTime, DateTime)>();
            var lastAvailableDate = endDate.AddDays(-numberOfDaysToStay);

            for (var date = initialDate; date <= lastAvailableDate; date = date.AddDays(1))
            {
                if (!takenDates.Any(d => d >= date && d < date.AddDays(numberOfDaysToStay)))
                {
                    availableDates.Add((date, date.AddDays(numberOfDaysToStay - 1)));
                }
            }

            if (!availableDates.Any())
            {
                var nextAvailableDate = takenDates.Last().AddDays(1);
                availableDates.Add((nextAvailableDate, nextAvailableDate.AddDays(numberOfDaysToStay - 1)));
            }

            return availableDates;
        }


        public bool IfDatesAreInTakenList(List<DateTime> datesToCheck, List<DateTime> takenDates)
        {
            return datesToCheck.Intersect(takenDates).Any();
        }

        public List<DateTime> FindTakenDates(Accommodation selectedAccommodation)
        {
            List<DateTime> takenDates = new List<DateTime>();
            foreach (AccommodationReservation reservation in Injector.CreateInstance<IAccommodationReservationRepository>().GetAll())
            {
                if (reservation.Accommodation.Id == selectedAccommodation.Id)
                {
                    List<DateTime> reservedDates = MakeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                    takenDates.AddRange(reservedDates);
                }
            }

            return takenDates;
        }
    }
}
