﻿using BookingProject.Controller;
using BookingProject.ConversionHelp;
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
        public NotificationController _notificationController { get; set; }

        public RequestAccommodationReservationController()
        {
            _requestsHandler = new RequestAccommodationReservationHandler();
            observers = new List<IObserver>();
            _accommodationReservationController = new AccommodationReservationController();
            _notificationController = new NotificationController();
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
        public bool PermissionToAcceptDenyRequest(RequestAccommodationReservation requestAccommodationReservation)
        {
            DateTime today = DateTime.Now.Date;
            if (requestAccommodationReservation.AccommodationReservation.InitialDate > today)
            {
                //DeleteReservationFromCSV(accommodationReservation);
                SendNotification(requestAccommodationReservation);
                NotifyObservers();
                return true;
            }

            return false;
        }
        public void SendNotification(RequestAccommodationReservation requestAccommodationReservation)
        {
            Notification notification = new Notification();
            notification.Id = _notificationController.GenerateId();
            notification.UserId = requestAccommodationReservation.AccommodationReservation.Guest.Id;
            notification.Text = "Request to move reservation from " + DateConversion.DateToStringAccommodation(requestAccommodationReservation.AccommodationReservation.InitialDate) + 
                " - " + DateConversion.DateToStringAccommodation(requestAccommodationReservation.AccommodationReservation.EndDate) + 
                " to " + DateConversion.DateToStringAccommodation(requestAccommodationReservation.NewArrivalDay) + " - "
                + DateConversion.DateToStringAccommodation(requestAccommodationReservation.NewDeparuteDay)  + " is " 
                + requestAccommodationReservation.Status;
            notification.Read = false;
            _notificationController.Create(notification);
            _notificationController.Save();
        }

        public List<Notification> GetGuest1Notifications(User guest)
        {
            List<Notification> notificationsForGuest = new List<Notification>();
            List<Notification> _notifications = _notificationController.GetAll();

            foreach (Notification notification in _notifications)
            {
                if (notification.UserId == guest.Id && notification.Read == false)
                {
                    notificationsForGuest.Add(notification);
                }
            }

            return notificationsForGuest;
        }

        public void DeleteNotificationFromCSV(Notification notification)
        {
            List<Notification> _notifications = _notificationController.GetAll();
            _notifications.RemoveAll(n => n.Id == notification.Id);
            _notificationController.Save();
        }

        public void WriteNotificationAgain(Notification n)
        {
            Notification notification = new Notification();
            notification.UserId = n.UserId;
            notification.Text = n.Text;
            notification.Read = true;
            _notificationController.Create(notification);
            _notificationController.Save();
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
        public void AcceptRequest(RequestAccommodationReservation reservationMovingRequest)
        {
            AccommodationReservation res = _accommodationReservationController.GetByID(reservationMovingRequest.AccommodationReservation.Id);
            res.InitialDate = reservationMovingRequest.NewArrivalDay;
            res.EndDate = reservationMovingRequest.NewDeparuteDay;
            _accommodationReservationController.Update(res);
        }
        public void Update(RequestAccommodationReservation reservationMovingRequest)
        {
            RequestAccommodationReservation oldRequest = GetByID(reservationMovingRequest.Id);
            if (oldRequest == null)
            {
                return;
            }
            oldRequest.OwnerComment = reservationMovingRequest.OwnerComment;
            oldRequest.NewArrivalDay = reservationMovingRequest.NewArrivalDay;
            oldRequest.NewDeparuteDay = reservationMovingRequest.NewDeparuteDay;
            oldRequest.Status = reservationMovingRequest.Status;
            SaveRequest();
        }
        public void RequestReservationBind()
        {
            _accommodationReservationController.Load();
            foreach (RequestAccommodationReservation request in _requests)
            {
                AccommodationReservation accommodation = _accommodationReservationController.GetByID(request.AccommodationReservation.Id);
                request.AccommodationReservation = accommodation;
            }
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
        public List<RequestAccommodationReservation> GetAllRequestForOwner(int ownerId)
        {
            List<RequestAccommodationReservation> requestList = new List<RequestAccommodationReservation>();
            foreach (var request in _requests)
            {
                if (request.AccommodationReservation.Accommodation.Owner.Id == ownerId && request.Status == Domain.Enums.RequestStatus.PENDING)
                {
                    requestList.Add(request);
                }
            }

            return requestList;
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
