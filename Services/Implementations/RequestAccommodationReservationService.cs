using BookingProject.ConversionHelp;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services
{
    public class RequestAccommodationReservationService : IRequestAccommodationReservationService
    {
        private IRequestAccommodationReservationRepository _requestRepository;
        public RequestAccommodationReservationService() { }
        public void Initialize()
        {
            _requestRepository = Injector.CreateInstance<IRequestAccommodationReservationRepository>();
        }

        public bool PermissionToAcceptDenyRequest(RequestAccommodationReservation requestAccommodationReservation)
        {
            DateTime today = DateTime.Now.Date;
            if (requestAccommodationReservation.AccommodationReservation.InitialDate > today)
            {
                //DeleteReservationFromCSV(accommodationReservation);
                SendNotification(requestAccommodationReservation);
                //NotifyObservers();
                return true;
            }

            return false;
        }
        public void Save(List<RequestAccommodationReservation> requests)
        {
            _requestRepository.Save(requests);
        }
        public void SaveRequest()
        {
            _requestRepository.SaveRequest();
        }
        public void SendNotification(RequestAccommodationReservation requestAccommodationReservation)
        {
            Notification notification = new Notification();
            //notification.Id = Injector.CreateInstance<INotificationRepository>().GenerateId();
            notification.UserId = requestAccommodationReservation.AccommodationReservation.Guest.Id;
            notification.Text = "Request to move reservation from " + DateConversion.DateToStringAccommodation(requestAccommodationReservation.AccommodationReservation.InitialDate) +
                " - " + DateConversion.DateToStringAccommodation(requestAccommodationReservation.AccommodationReservation.EndDate) +
                " to " + DateConversion.DateToStringAccommodation(requestAccommodationReservation.NewArrivalDay) + " - "
                + DateConversion.DateToStringAccommodation(requestAccommodationReservation.NewDeparuteDay) + " is "
                + requestAccommodationReservation.Status;
            notification.Read = false;
            Injector.CreateInstance<INotificationService>().Create(notification);
        }

        public void AcceptRequest(RequestAccommodationReservation reservationMovingRequest)
        {
            AccommodationReservation res = Injector.CreateInstance<IAccommodationReservationService>().GetByID(reservationMovingRequest.AccommodationReservation.Id);
            res.InitialDate = reservationMovingRequest.NewArrivalDay;
            res.EndDate = reservationMovingRequest.NewDeparuteDay;
            Injector.CreateInstance<IAccommodationReservationService>().Update(res);
        }

        public List<RequestAccommodationReservation> GetAllForUser(User guest)
        {
            List<RequestAccommodationReservation> requests = new List<RequestAccommodationReservation>();
            foreach (RequestAccommodationReservation r in _requestRepository.GetAll())
            {
                if (r.AccommodationReservation.Guest.Id == guest.Id)
                {
                    requests.Add(r);
                }
            }
            return requests;
        }

        public List<Notification> GetGuest1Notifications(User guest)
        {
            List<Notification> notificationsForGuest = new List<Notification>();
            List<Notification> _notifications = Injector.CreateInstance<INotificationService>().GetAll();

            foreach (Notification notification in _notifications)
            {
                if (notification.UserId == guest.Id && notification.Read == false)
                {
                    notificationsForGuest.Add(notification);
                }
            }

            return notificationsForGuest;
        }

        public List<RequestAccommodationReservation> GetAllRequestForOwner(int ownerId)
        {
            List<RequestAccommodationReservation> requestList = new List<RequestAccommodationReservation>();
            foreach (var request in _requestRepository.GetAll())
            {
                if (request.AccommodationReservation.Accommodation.Owner.Id == ownerId && request.Status == Domain.Enums.RequestStatus.PENDING)
                {
                    requestList.Add(request);
                }
            }

            return requestList;
        }

        public void Update(RequestAccommodationReservation reservationMovingRequest)
        {
            RequestAccommodationReservation oldRequest = _requestRepository.GetByID(reservationMovingRequest.Id);
            if (oldRequest == null)
            {
                return;
            }
            oldRequest.OwnerComment = reservationMovingRequest.OwnerComment;
            oldRequest.NewArrivalDay = reservationMovingRequest.NewArrivalDay;
            oldRequest.NewDeparuteDay = reservationMovingRequest.NewDeparuteDay;
            oldRequest.Status = reservationMovingRequest.Status;
            SaveRequest(); //-- NE RADI TI ?????????
        }

        public List<RequestAccommodationReservation> GetAll()
        {
            return _requestRepository.GetAll();
        }
        
        public void Create(RequestAccommodationReservation request)
        {
            _requestRepository.Create(request);
        }

        public RequestAccommodationReservation GetByID(int id)
        {
            return _requestRepository.GetByID(id);
        }

        public void SendRequest(AccommodationReservation SelectedReservation, String Comment, DateTime NewInitialDate, DateTime NewEndDate)
        {
            RequestAccommodationReservation request = new RequestAccommodationReservation();
            request.AccommodationReservation = SelectedReservation;
            request.GuestComment = Comment;
            request.OwnerComment = String.Empty;
            request.NewArrivalDay = NewInitialDate;
            request.NewDeparuteDay = NewEndDate;
            request.Status = Domain.Enums.RequestStatus.PENDING;

            Create(request);
            SaveRequest();
        }

    }
}
