using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class AccommodationReservationController : ISubject 
    {
        private readonly List<IObserver> observers;
        private readonly AccommodationReservationHandler _accommodationReservationHandler;
        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationController()
        {
            _accommodationReservationHandler = new AccommodationReservationHandler();
            _accommodationReservations = new List<AccommodationReservation>();
            Load();
        }

        public void Load()
        {
            _accommodationReservations = _accommodationReservationHandler.Load();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservations;
        }

        public void Save()
        {
            _accommodationReservationHandler.Save(_accommodationReservations);
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

        public bool isValidBook(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, string numberOfDaysToStay)
        {
            if(int.Parse(numberOfDaysToStay) < selectedAccommodation.MinDays || !checkDate(initialDate, endDate) || !checkAvailableDate(selectedAccommodation, initialDate, endDate, numberOfDaysToStay))
            {
                return false;
            }
            return true;
        }

        public bool checkDate(DateTime initialDate, DateTime endDate)
        {
            if(initialDate.Month > endDate.Month || endDate.Year < initialDate.Year || !checkDays(initialDate, endDate) || !checkTodayDate(initialDate))
            {
                return false;
            }
            return true;
        }

        public bool checkTodayDate(DateTime initialDate)
        {
            DateTime today = DateTime.Now.Date;
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);
            if (initialDate < todayMidnight)
            {
                
                return false;
            }
            return true;
        }

        public bool checkDays(DateTime initialDate, DateTime endDate)
        {
            if(initialDate.Month == endDate.Month)
            {
                if(initialDate.Day > endDate.Day)
                {
                    return false;
                }
            }
            return true;
        }

        public bool checkAvailableDate(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, string numberOfDaysToStay) 
        {
            
            foreach(AccommodationReservation reservation in _accommodationReservations)
            {
                if(reservation.Accommodation.Id == selectedAccommodation.Id)
                {
                    List<DateTime> reservedDates = makeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                    foreach (DateTime date in reservedDates)
                    {
                        if(initialDate == date || endDate == date)
                        {
                            return false;
                        }
                    }
                }
            }
            AccommodationReservation accommodationReservation = new AccommodationReservation(GenerateId(), selectedAccommodation, initialDate, endDate, int.Parse(numberOfDaysToStay));
            _accommodationReservations.Add(accommodationReservation);
            Save();

            return true;
        }

        public List<DateTime> makeListOfReservedDates(DateTime initialDate, DateTime endDate)
        {
            List<DateTime> reservedDates = new List<DateTime>();
            for (DateTime date = initialDate; date <= endDate; date = date.AddDays(1))
            {
                reservedDates.Add(date);
            }
            return reservedDates;
        }

        public List<(DateTime, DateTime)> findAvailableDates(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, string numberOfDaysToStay)
        {
            List<(DateTime, DateTime)> availableDates = new List<(DateTime, DateTime)>();
            List<DateTime> takenDates = findTakenDates(selectedAccommodation);

            takenDates.Sort();

            for(DateTime date = initialDate; date <= takenDates[takenDates.Count - 1]; date = date.AddDays(1))
            {
                List<DateTime> datesInRange = makeListOfReservedDates(date, date.AddDays(int.Parse(numberOfDaysToStay)));
                if(!ifDatesAreInTakenList(datesInRange, takenDates))
                {
                    availableDates.Add((datesInRange[0], datesInRange[datesInRange.Count - 1]));
                    break;
                }
            }

            if(availableDates.Count() == 0)
            {
                availableDates.Add((takenDates[takenDates.Count - 1].AddDays(1), takenDates[takenDates.Count - 1].AddDays(int.Parse(numberOfDaysToStay) + 1)));
            }

            DateTime today = DateTime.Now.Date; 
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);


            for (DateTime date = initialDate; (date.AddDays(-int.Parse(numberOfDaysToStay))) > todayMidnight; date = date.AddDays(-1))
            {
                List<DateTime> datesInRange = makeListOfReservedDates(date.AddDays(-int.Parse(numberOfDaysToStay)), date);
                if(!ifDatesAreInTakenList(datesInRange, takenDates))
                {
                    availableDates.Add((datesInRange[0], datesInRange[datesInRange.Count - 1]));
                    break;
                }
            }


            return availableDates;
        }

        public bool ifDatesAreInTakenList(List<DateTime> datesToCheck, List<DateTime> takenDates)
        {
            foreach(DateTime date in datesToCheck)
            {
                foreach(DateTime takenDate in takenDates)
                {
                    if(date == takenDate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<DateTime> findTakenDates(Accommodation selectedAccommodation)
        {
            List<DateTime> takenDates = new List<DateTime>();
            foreach(AccommodationReservation reservation in _accommodationReservations)
            {
                if(reservation.Accommodation.Id == selectedAccommodation.Id)
                {
                    List<DateTime> reservedDates = makeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                    takenDates.AddRange(reservedDates);
                }
            }

            return takenDates;
        }

        public AccommodationReservation GetByID(int id)
        {
            return _accommodationReservations.Find(ar => ar.Id == id);
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
