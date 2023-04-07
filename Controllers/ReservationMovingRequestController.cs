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
    public class ReservationMovingRequestController: ISubject
    {
        private readonly List<IObserver> observers;
        private readonly ReservationMovingRequestHandler _reservationMovingRequestHandler;
        private List<ReservationMovingRequest> _reservationMovingRequest;
        private AccommodationReservationController _reservationController;

        public ReservationMovingRequestController()
        {
            _reservationMovingRequestHandler = new ReservationMovingRequestHandler();
            _reservationMovingRequest = new List<ReservationMovingRequest>();
            _reservationController = new AccommodationReservationController();

            Load();
        }

        public void Load()
        {
            _reservationMovingRequest = _reservationMovingRequestHandler.Load();
            RequestReservationBind();
        }

        public List<ReservationMovingRequest> GetAll()
        {
            return _reservationMovingRequest;
        }

        public void Save()
        {
            _reservationMovingRequestHandler.Save(_reservationMovingRequest);
        }

        public void RequestReservationBind()
        {
            _reservationController.Load();
            foreach (ReservationMovingRequest request in _reservationMovingRequest)
            {
                AccommodationReservation accommodation = _reservationController.GetByID(request.Reservation.Id);
                request.Reservation = accommodation;
            }
        }
        

        public ReservationMovingRequest GetByID(int id)
        {
            return _reservationMovingRequest.Find(ar => ar.Id == id);
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach (ReservationMovingRequest request in _reservationMovingRequest)
            {
                if (request.Id > maxId)
                {
                    maxId = request.Id;
                }
            }
            return maxId + 1;
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

        public List<ReservationMovingRequest> GetAllRequestForOwner(int ownerId)
        {
            List<ReservationMovingRequest> requestList = new List<ReservationMovingRequest>();
            foreach(var request in _reservationMovingRequest)
            {
                if(request.Reservation.Accommodation.Owner.Id == ownerId && request.Status == Domain.Enums.RequestStatus.ON_WAIT)
                {
                    requestList.Add(request);
                }
            }

            return requestList;
        }
    }
}
