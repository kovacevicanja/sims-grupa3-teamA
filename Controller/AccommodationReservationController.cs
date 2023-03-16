﻿using BookingProject.FileHandler;
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
            if(initialDate.Day >= endDate.Day || initialDate.Month > endDate.Month || endDate.Year < initialDate.Year)
            {
                return false;
            }
            return true;
        }

        public bool checkAvailableDate(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, string numberOfDaysToStay) 
        {
            
            foreach(AccommodationReservation reservation in _accommodationReservations)
            {
                List<DateTime> reservedDates = makeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                if(reservation.Accommodation.Id == selectedAccommodation.Id)
                {
                    foreach(DateTime date in reservedDates)
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
