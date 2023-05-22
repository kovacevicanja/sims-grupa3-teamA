using BookingProject.DependencyInjection;
using BookingProject.Domain;
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
    public class AccommodationRenovationService : IAccommodationRenovationService
    {
        private IAccommodationRenovationRepository _renovationRepository;
        //public IAccommodationReservationService _accommodationReservationService;

        public AccommodationRenovationService() {
        }

        public void Initialize()
        {
            _renovationRepository = Injector.CreateInstance<IAccommodationRenovationRepository>();
            //_accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
        }
        public AccommodationRenovation Save(AccommodationRenovation accommodationRenovation)
        {
            return _renovationRepository.Save(accommodationRenovation);
        }

        public void Create(AccommodationRenovation renovation)
        {
            _renovationRepository.Create(renovation);
        }

        public List<AccommodationRenovation> GetAll()
        {
            return _renovationRepository.GetAll();
        }
        public AccommodationRenovation GetById(int id)
        {
            return _renovationRepository.GetById(id);
        }
        public void Save(List<AccommodationRenovation> renovations)
        {
            _renovationRepository.Save(renovations);
        }
        public void Delete(AccommodationRenovation accommodationRenovation)
        {
            _renovationRepository.Delete(accommodationRenovation);
        }

        public AccommodationRenovation Update(AccommodationRenovation accommodationRenovation)
        {
            return _renovationRepository.Update(accommodationRenovation);
        }
        public List<AccommodationRenovation> GetRenovationsInPast()
        {
            List<AccommodationRenovation> renovations = _renovationRepository.GetAll();

            List<AccommodationRenovation> passedRenovations = new List<AccommodationRenovation>();

            foreach (AccommodationRenovation renovation in renovations)
            {
                if (renovation.StartDate < DateTime.Today)
                    passedRenovations.Add(renovation);
            }

            return passedRenovations;
        }
        public List<AccommodationRenovation> GetRenovationsInFuture()
        {
            List<AccommodationRenovation> renovations = _renovationRepository.GetAll();

            List<AccommodationRenovation> futureRenovations = new List<AccommodationRenovation>();

            foreach (AccommodationRenovation renovation in renovations)
            {
                if (renovation.StartDate > DateTime.Today)
                    futureRenovations.Add(renovation);
            }

            return futureRenovations;
        }
        public List<Accommodation> FillRenovationData(List<Accommodation> accommodations)
        {
            foreach (Accommodation accommodation in accommodations)
            {
                Tuple<DateTime, DateTime> lastRenovation = FindLastRenovation(accommodation);
                TimeSpan dayDifference = DateTime.Today - lastRenovation.Item2;
                if (dayDifference.Days <= 365)
                {
                    accommodation.IsRecentlyRenovated = true;
                }
                else
                {
                    accommodation.IsRecentlyRenovated = false;
                }
            }

            return accommodations;
        }
        //public List<Tuple<DateTime, DateTime>> FindAvailableDates(DateTime startDate, DateTime endDate, int duration, Accommodation selectedAccommodation)
        //{
        //    List<DateTime> reservedDates = FindDatesThatAreNotAvailable(selectedAccommodation);
        //    List<DateTime> renovationDates = FindRenovationDates(selectedAccommodation);
        //    List<DateTime> availableDates = new List<DateTime>();
        //    List<Tuple<DateTime, DateTime>> availableDatesPair = new List<Tuple<DateTime, DateTime>>();

        //    for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        //    {
        //        CheckIfDatesAreAvailable(availableDates, reservedDates, renovationDates, date);
        //        CheckDatePairExistence(availableDates, availableDatesPair, duration);
        //    }
        //    return availableDatesPair;
        //}
        public void CheckDatePairExistence(List<DateTime> availableDates, List<Tuple<DateTime, DateTime>> availableDatesPair, int duration)
        {
            if (availableDates.Count == duration)
            {
                availableDatesPair.Add(Tuple.Create(availableDates[0].Date, availableDates[availableDates.Count - 1].Date));
                availableDates.RemoveAt(0);
            }
        }
        public Tuple<DateTime, DateTime> FindLastRenovation(Accommodation accommodation)
        {
            List<AccommodationRenovation> accommodationRenovations = FindRenovationsForAccommodationId(accommodation.Id);

            Tuple<DateTime, DateTime> lastRenovation = Tuple.Create(accommodationRenovations[0].StartDate, accommodationRenovations[0].EndDate);

            foreach (AccommodationRenovation renovation in accommodationRenovations)
            {
                if (renovation.EndDate > lastRenovation.Item2)
                    lastRenovation = Tuple.Create(renovation.StartDate, renovation.EndDate);
            }

            return lastRenovation;
        }
        public List<AccommodationRenovation> FindRenovationsForAccommodationId(int id)
        {
            List<AccommodationRenovation> renovations = GetAll();
            List<AccommodationRenovation> accommodationRenovations = new List<AccommodationRenovation>();

            foreach (AccommodationRenovation renovation in renovations)
            {
                if (renovation.Accommodation.Id == id)
                    accommodationRenovations.Add(renovation);
            }

            return accommodationRenovations;
        }
        //public List<DateTime> FindDatesThatAreNotAvailable(Accommodation selectedAccommodation)
        //{
        //    List<AccommodationReservation> reservations = _accommodationReservationService.GetAll();
        //    List<DateTime> nonAvailableDates = new List<DateTime>();

        //    foreach (AccommodationReservation reservation in reservations)
        //    {
        //        if (selectedAccommodation.Id == reservation.Accommodation.Id && reservation.IsCancelled == false)
        //        {
        //            DateTime checkIn = reservation.InitialDate;
        //            DateTime checkOut = reservation.EndDate;

        //            for (DateTime currentDate = checkIn; currentDate <= checkOut; currentDate = currentDate.AddDays(1))
        //            {
        //                nonAvailableDates.Add(currentDate);
        //            }
        //        }
        //    }
        //    return nonAvailableDates;
        //}
        public List<DateTime> FindRenovationDates(Accommodation selectedAccommodation)
        {
            List<AccommodationRenovation> renovations = GetAll();
            List<DateTime> renovationDates = new List<DateTime>();

            foreach (AccommodationRenovation renovation in renovations)
            {
                if (selectedAccommodation.Id == renovation.Accommodation.Id)
                {
                    DateTime startDate = renovation.StartDate;
                    DateTime endDate = renovation.EndDate;

                    for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
                    {
                        renovationDates.Add(currentDate);
                    }
                }
            }
            return renovationDates;
        }
        public void CheckIfDatesAreAvailable(List<DateTime> dates, List<DateTime> reservationDates, List<DateTime> renovationDates, DateTime date)
        {
            if (!reservationDates.Contains(date) && !renovationDates.Contains(date))
            {
                dates.Add(date);
            }
            else
            {
                dates.Clear();
            }
        }
    }
}
