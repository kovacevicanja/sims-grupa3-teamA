using BookingProject.Controller;
using BookingProject.ConversionHelp;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
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
        private readonly IRequestAccommodationReservationService _requestsService;
        public RequestAccommodationReservationController()
        {
            _requestsService = Injector.CreateInstance<IRequestAccommodationReservationService>();
        }
        public void Initialize()
        {
            _requestsService.Initialize();
        }
        public void Save(List<RequestAccommodationReservation> requests)
        {
            _requestsService.Save(requests);
        }
        public void SaveRequest()
        {
            _requestsService.SaveRequest();
        }
        public bool PermissionToAcceptDenyRequest(RequestAccommodationReservation requestAccommodationReservation)
        {
            return _requestsService.PermissionToAcceptDenyRequest(requestAccommodationReservation);
        }
        public void SendNotification(RequestAccommodationReservation requestAccommodationReservation)
        {
            _requestsService.SendNotification(requestAccommodationReservation);
        }
        public void AcceptRequest(RequestAccommodationReservation reservationMovingRequest)
        {
            _requestsService.AcceptRequest(reservationMovingRequest);
        }
        public List<RequestAccommodationReservation> GetAllForUser(User guest)
        {
            return _requestsService.GetAllForUser(guest);
        }
        public List<Notification> GetGuest1Notifications(User guest)
        {
            return _requestsService.GetGuest1Notifications(guest);
        }
        public List<RequestAccommodationReservation> GetAllRequestForOwner(int ownerId)
        {
            return GetAllRequestForOwner(ownerId);
        }
        public void Update(RequestAccommodationReservation reservationMovingRequest)
        {
            _requestsService.Update(reservationMovingRequest);
        }
        public List<RequestAccommodationReservation> GetAll()
        {
            return _requestsService.GetAll();
        }
        public void Create(RequestAccommodationReservation request)
        {
            _requestsService.Create(request);
        }
        public RequestAccommodationReservation GetByID(int id)
        {
            return _requestsService.GetByID(id);
        }
    }
}