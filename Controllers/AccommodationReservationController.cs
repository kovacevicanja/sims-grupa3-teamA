﻿using BookingProject.Controllers;
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
        public AccommodationReservationController()
        {
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
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
    }
}