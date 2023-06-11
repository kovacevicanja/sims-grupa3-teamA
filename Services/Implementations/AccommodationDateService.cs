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

        public AccommodationDate GetById(int id)
        {
            return _accommodationDateRepository.GetById(id);
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

        public List<AccommodationReservation> FindAcceptableReservations(Accommodation accommodation)
        {
            List<AccommodationReservation> allReservations = new List<AccommodationReservation>(GetReservationsForAccommodation(accommodation));
            List<AccommodationReservation> sortedReservations = allReservations.OrderBy(r => r.InitialDate).ToList();
            List<AccommodationReservation> filteredReservations = sortedReservations.Where(r => r.InitialDate > DateTime.Today).ToList();
            return filteredReservations;
        }

        public List<(DateTime, DateTime)> FindAvailableDatesQuick(Accommodation accommodation, int daysToStay)
		{
            List<AccommodationReservation> allReservations = FindAcceptableReservations(accommodation);
            List<(DateTime, DateTime)> freeDateRanges = new List<(DateTime, DateTime)>();
            int count = 0; 

            for (int i = 0; i < allReservations.Count - 1; i++)
            {
                DateTime currentEndDate = allReservations[i].EndDate;
                DateTime nextStartDate = allReservations[i + 1].InitialDate;

                if ((nextStartDate - currentEndDate).Days >= daysToStay)
                {
                    freeDateRanges.Add((currentEndDate.AddDays(1), nextStartDate.AddDays(-1)));
                    count++;

                    if (count == 3)
                        break;
                }
            }


            return freeDateRanges;
        }

        public List<(DateTime, DateTime)> FindAvailableDatesQuickRanges(Accommodation accommodation, int daysToStay, DateTime initialDate, DateTime endDate)
        {
            List<AccommodationReservation> allReservations = FindAcceptableReservations(accommodation);

            List<(DateTime, DateTime)> availableRanges = new List<(DateTime, DateTime)>();

            DateTime currentDate = initialDate;
            while (currentDate <= endDate)
            {
                bool isAvailable = IsDateAvailable(currentDate, daysToStay, allReservations);
                if (isAvailable)
                {
                    DateTime rangeEndDate = currentDate.AddDays(daysToStay - 1);
                    availableRanges.Add((currentDate, rangeEndDate));
                }

                currentDate = currentDate.AddDays(1);
            }

            return availableRanges;
        }

        public bool IsDateAvailable(DateTime date, int daysToStay, List<AccommodationReservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                if (date >= reservation.InitialDate && date <= reservation.EndDate)
                {
                    return false;  
                }

                for (int i = 1; i < daysToStay; i++)
                {
                    DateTime currentDate = date.AddDays(i);
                    if (currentDate >= reservation.InitialDate && currentDate <= reservation.EndDate)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
