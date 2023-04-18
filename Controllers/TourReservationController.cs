﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.Model;
using BookingProject.View;
using OisisiProjekat.Observer;
using BookingProject.ConversionHelp;
using BookingProject.Controllers;
using BookingProject.Domain;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using BookingProject.Repositories.Intefaces;
using BookingProject.DependencyInjection;
using BookingProject.Services.Interfaces;
using BookingProject.Repositories.Implementations;

namespace BookingProject.Controller
{
    public class TourReservationController
    {
        private ITourReservationService _tourReservationService;
        public TourReservationController()
        {
            _tourReservationService = Injector.CreateInstance<ITourReservationService>();
        }
        public bool BookingSuccess(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            return _tourReservationService.BookingSuccess(chosenTour, numberOfGuests, selectedDate, guest);
        }
        public bool GoThroughReservations(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            return _tourReservationService.GoThroughReservations(chosenTour, numberOfGuests, selectedDate, guest);
        }
        public bool TryReservation(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            return _tourReservationService.TryReservation(chosenTour, numberOfGuests, selectedDate, guest);
        }
        public void TryToBook(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            _tourReservationService.TryToBook(chosenTour, numberOfGuests, selectedDate, guest);
        }
        public void FullyBookedTours(Tour chosenTour, DateTime selectedDate, User guest)
        {
            _tourReservationService.FullyBookedTours(chosenTour, selectedDate, guest);
        }
<<<<<<< HEAD
        public void SuccessfulReservationMessage(string numberOfGuests, User guest, Tour chosenTour) 
=======
        public void SuccessfulReservationMessage(string numberOfGuests, User guest, Tour chosenTour)
>>>>>>> d2627674c824f45bb58aa62a6de4d98d327c1446
        {
            _tourReservationService.SuccessfulReservationMessage(numberOfGuests, guest, chosenTour);
        }
        public void FreePlaceMessage(int maxGuests)
        {
            _tourReservationService.FreePlaceMessage(maxGuests);
        }
        public List<Tour> GetFilteredTours(Location location, DateTime selectedDate)
        {
            return _tourReservationService.GetFilteredTours(location, selectedDate);   
        }
        public void GoThroughTourDates(Tour tour, DateTime selectedDate)
        {
            _tourReservationService.GoThroughTourDates(tour, selectedDate);
        }
        public void GoThroughBookedToursDates(Tour tour, DateTime selectedDate, TourDateTime tdt)
        {
            _tourReservationService.GoThroughBookedToursDates(tour, selectedDate, tdt);
        }
        public List<TourReservation> GetAll()
        {
            return _tourReservationService.GetAll();
        }
        public TourReservation GetByID(int id)
        {
            return _tourReservationService.GetByID(id);
        }
        public List<TourReservation> GetUserReservations(int guestId)
        {
            return _tourReservationService.GetUserReservations(guestId);
        }
    }
}