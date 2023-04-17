﻿using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class RequestAccommodationReservationController
    {
        private readonly List<IObserver> observers;

        private readonly RequestAccommodationReservationHandler _requestsHandler;

        private List<RequestAccommodationReservation> _requests;

        private AccommodationReservationController _accommodationReservationController;

        public RequestAccommodationReservationController()
        {
            _requestsHandler = new RequestAccommodationReservationHandler();
            _accommodationReservationController = new AccommodationReservationController();
            _requests = new List<RequestAccommodationReservation>();
            Load();
        }

        public void Load()
        {
            _requests = _requestsHandler.Load();
            ReservationRequestsBind();
        }

        public void ReservationRequestsBind()
        {
            _accommodationReservationController.Load();
                foreach (RequestAccommodationReservation request in _requests)
                {
                    AccommodationReservation reservation = _accommodationReservationController.GetByID(request.AccommodationReservation.Id);
                    request.AccommodationReservation = reservation;
                }
            
        }

        public List<RequestAccommodationReservation> GetAll()
        {
            return _requests;
        }
        public List<RequestAccommodationReservation> GetAllForUser(User guest)
        {
            List<RequestAccommodationReservation> requests = new List<RequestAccommodationReservation>();
            foreach(RequestAccommodationReservation r in _requests)
            {
                if(r.AccommodationReservation.Guest.Id == guest.Id)
                {
                    requests.Add(r);
                }
            }
            return requests;
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach (RequestAccommodationReservation image in _requests)
            {
                if (image.Id > maxId)
                {
                    maxId = image.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(RequestAccommodationReservation request)
        {
            request.Id = GenerateId();
            _requests.Add(request);
        }

        public void SaveRequest()
        {
            _requestsHandler.Save(_requests);
        }

        public RequestAccommodationReservation GetByID(int id)
        {
            return _requests.Find(image => image.Id == id);
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