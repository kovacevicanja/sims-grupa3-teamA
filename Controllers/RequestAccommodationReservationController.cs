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
            return _requestsService.GetAllRequestForOwner(ownerId);
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
        public RequestAccommodationReservation GetById(int id)
        {
            return _requestsService.GetById(id);
        }

        public void SendRequest(AccommodationReservation SelectedReservation, String Comment, DateTime NewInitialDate, DateTime NewEndDate)
        {
            _requestsService.SendRequest(SelectedReservation, Comment, NewInitialDate, NewEndDate); 
        }
        public int CountRescheduledReservationsForSpecificYear(int year, int accId)
        {
            return _requestsService.CountRescheduledReservationForSpecificYear(year, accId);
        }
        public int CountRescheduledReservationsForSpecificMonth(int year,int month, int accId)
        {
            return _requestsService.CountRescheduledReservationForSpecificMonth(year, month, accId);
        }
        public List<RequestAccommodationReservation> GetAllForAccId(int id)
        {
            List<RequestAccommodationReservation> res = new List<RequestAccommodationReservation>();
            foreach (RequestAccommodationReservation reservation in GetAll())
            {
                if (reservation.AccommodationReservation.Accommodation.Id == id && reservation.Status==Domain.Enums.RequestStatus.APPROVED)
                {
                    res.Add(reservation);
                }
            }
            return res;
        }
        public int CountResForAcc(int accId)
        {
            int res = 0;
            foreach (RequestAccommodationReservation reservation in GetAll())
            {
                if (reservation.AccommodationReservation.Accommodation.Id == accId && reservation.Status==Domain.Enums.RequestStatus.APPROVED)
                {
                    res++;
                }
            }
            return res;
        }
        public List<RequestAccommodationReservation> GetAllForAccIdAndYear(int id,int year)
        {
            List<RequestAccommodationReservation> res = new List<RequestAccommodationReservation>();
            foreach (RequestAccommodationReservation reservation in GetAll())
            {
                if (reservation.AccommodationReservation.Accommodation.Id == id && reservation.Status==Domain.Enums.RequestStatus.APPROVED && reservation.NewDeparuteDay.Year==year)
                {
                    res.Add(reservation);
                }
            }
            return res;
        }
        public int CountResForAccAndYear(int accId, int year)
        {
            int res = 0;
            foreach (RequestAccommodationReservation reservation in GetAll())
            {
                if (reservation.AccommodationReservation.Accommodation.Id == accId && reservation.Status==Domain.Enums.RequestStatus.APPROVED && reservation.NewDeparuteDay.Year == year)
                {
                    res++;
                }
            }
            return res;
        }

    }
}