using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookingProject.View.FindAvailableDatesForAccommodation;

namespace BookingProject.Controller
{
    public class AccommodationReservationController : ISubject 
    {
        private readonly List<IObserver> observers;
        private readonly AccommodationReservationHandler _accommodationReservationHandler;
        private List<AccommodationReservation> _accommodationReservations;
        public AccommodationController _accommodationController { get; set; }
        public GuestGradeController _guestGradeController { get; set; }

        public AccommodationReservationController()
        {
            _accommodationReservationHandler = new AccommodationReservationHandler();
            _accommodationReservations = new List<AccommodationReservation>();
            _accommodationController = new AccommodationController();
            
           Load();
        }

        public void Load()
        {
            _accommodationReservations = _accommodationReservationHandler.Load();
            AccommodationReservationBind();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservations;
        }

        public void Save()
        {
            _accommodationReservationHandler.Save(_accommodationReservations);
        }

        public void AccommodationReservationBind()
        {
            _accommodationController.Load();
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                Accommodation accommodation = _accommodationController.GetByID(reservation.Accommodation.Id);
                reservation.Accommodation = accommodation;
            }
        }

        public AccommodationReservation GetByID(int id)
        {
            return _accommodationReservations.Find(ar => ar.Id == id);
        }

        private bool IsReservationAvailable(AccommodationReservation accommodationReservation)
        {
            return accommodationReservation.EndDate <= DateTime.Now && accommodationReservation.EndDate.AddDays(5) >= DateTime.Now;
        }

        public List<AccommodationReservation> GetAllNotGradedReservations()
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                if (IsReservationAvailable(reservation) == false)
                {
                    continue;
                }
                if (!(_guestGradeController.DoesReservationHaveGrade(reservation.Id)))
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach(AccommodationReservation accommodationReservation in _accommodationReservations)
            {
                if(accommodationReservation.Id > maxId)
                {
                    maxId = accommodationReservation.Id;
                }
            }
            return maxId + 1;
        }

        public bool CheckNumberOfGuests(Accommodation selectedAccommodation, string numberOfGuests)
        {
            if(selectedAccommodation.MaxGuestNumber >= int.Parse(numberOfGuests))
            {
                return true;
            }
            return false;
        }

        public bool CheckEnteredDates(DateTime initialDate, DateTime endDate)
        {
            if(initialDate.Month > endDate.Month || endDate.Year < initialDate.Year || !CheckDays(initialDate, endDate) || !CompareWithToday(initialDate))
            {
                return false;
            }
            return true;
        }

        public bool CompareWithToday(DateTime initialDate)
        {
            DateTime today = DateTime.Now.Date;
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);
            if (initialDate < todayMidnight)
            {
                return false;
            }
            return true;
        }

        public bool CheckDays(DateTime initialDate, DateTime endDate)
        {
            if (initialDate.Month == endDate.Month)
            {
                if (initialDate.Day > endDate.Day)
                {
                    return false;
                }
            }
            return true;
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
            foreach (AccommodationReservation reservation in _accommodationReservations)
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

        /*
        private bool isDateReserved(List<DateTime> tryDates, List<AccommodationReservation> reservations)
        {
            foreach (AccommodationReservation reservation in reservations)
            {
                List<DateTime> reservedDates = makeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                if(ifDatesAreInTakenList(tryDates, reservedDates))
                {
                    return true;
                }
            }
            return false;
        }
        */

        private bool IsDateReserved(List<DateTime> tryDates, List<AccommodationReservation> reservations)
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
            List<(DateTime, DateTime)> availableDates = new List<(DateTime, DateTime)>();
            List<DateTime> takenDates = FindTakenDates(selectedAccommodation);

            takenDates.Sort();

            for (DateTime date = initialDate; date <= takenDates[takenDates.Count - 1]; date = date.AddDays(1))
            {
                List<DateTime> datesInRange = MakeListOfReservedDates(date, date.AddDays(numberOfDaysToStay));
                if (!IfDatesAreInTakenList(datesInRange, takenDates))
                {
                    availableDates.Add((datesInRange[0], datesInRange[datesInRange.Count - 1]));
                    break;
                }
            }

            if (availableDates.Count() == 0)
            {
                availableDates.Add((takenDates[takenDates.Count - 1].AddDays(1), takenDates[takenDates.Count - 1].AddDays(numberOfDaysToStay + 1)));
            }

            DateTime today = DateTime.Now.Date;
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);


            for (DateTime date = initialDate; (date.AddDays(-numberOfDaysToStay)) > todayMidnight; date = date.AddDays(-1))
            {
                List<DateTime> datesInRange = MakeListOfReservedDates(date.AddDays(-numberOfDaysToStay), date);
                if (!IfDatesAreInTakenList(datesInRange, takenDates))
                {
                    availableDates.Add((datesInRange[0], datesInRange[datesInRange.Count - 1]));
                    break;
                }
            }


            return availableDates;
        }

        public bool IfDatesAreInTakenList(List<DateTime> datesToCheck, List<DateTime> takenDates)
        {
            foreach (DateTime date in datesToCheck)
            {
                foreach (DateTime takenDate in takenDates)
                {
                    if (date == takenDate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<DateTime> FindTakenDates(Accommodation selectedAccommodation)
        {
            List<DateTime> takenDates = new List<DateTime>();
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                if (reservation.Accommodation.Id == selectedAccommodation.Id)
                {
                    List<DateTime> reservedDates = MakeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                    takenDates.AddRange(reservedDates);
                }
            }

            return takenDates;
        }

        public void BookAccommodation(DateTime initialDate, DateTime endDate, Accommodation selectedAccommodation)
        {
            AccommodationReservation reservation = new AccommodationReservation();
            reservation.Id = GenerateId();
            reservation.Accommodation.Id = selectedAccommodation.Id;
            reservation.InitialDate = initialDate;
            reservation.EndDate = endDate;
            reservation.DaysToStay = (endDate - initialDate).Days;
            _accommodationReservations.Add(reservation);
            Save();
        }

        


        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }
    }
}
