using BookingProject.Controller;
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
        private AccommodationReservationController _reservationController;

        public RequestAccommodationReservationController()
        {
            _requestsHandler = new RequestAccommodationReservationHandler();
            _requests = new List<RequestAccommodationReservation>();
            _reservationController= new AccommodationReservationController();
            Load();
        }

        public void Load()
        {
            _requests = _requestsHandler.Load();
            RequestReservationBind();
        }

        public List<RequestAccommodationReservation> GetAll()
        {
            return _requests;
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
            AccommodationReservation res = _reservationController.GetByID(reservationMovingRequest.AccommodationReservation.Id);
            res.InitialDate = reservationMovingRequest.NewArrivalDay;
            res.EndDate = reservationMovingRequest.NewDeparuteDay;
            _reservationController.Update(res);
        }
        public void Update(RequestAccommodationReservation reservationMovingRequest)
        {
            RequestAccommodationReservation oldRequest = GetByID(reservationMovingRequest.Id);
            if (oldRequest == null)
            {
                return;
            }
            oldRequest.Comment = reservationMovingRequest.Comment;
            oldRequest.NewArrivalDay = reservationMovingRequest.NewArrivalDay;
            oldRequest.NewDeparuteDay = reservationMovingRequest.NewDeparuteDay;
            oldRequest.Status = reservationMovingRequest.Status;
            SaveRequest();
        }
        public void RequestReservationBind()
        {
            _reservationController.Load();
            foreach (RequestAccommodationReservation request in _requests)
            {
                AccommodationReservation accommodation = _reservationController.GetByID(request.AccommodationReservation.Id);
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
