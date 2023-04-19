using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IRequestAccommodationReservationService
    {
        void Initialize();
        bool PermissionToAcceptDenyRequest(RequestAccommodationReservation requestAccommodationReservation);
        void SendNotification(RequestAccommodationReservation requestAccommodationReservation);
        void AcceptRequest(RequestAccommodationReservation reservationMovingRequest);
        List<RequestAccommodationReservation> GetAllForUser(User guest);
        List<Notification> GetGuest1Notifications(User guest);
        List<RequestAccommodationReservation> GetAllRequestForOwner(int ownerId);
        void Update(RequestAccommodationReservation reservationMovingRequest);
        List<RequestAccommodationReservation> GetAll();
        void Create(RequestAccommodationReservation request);
        RequestAccommodationReservation GetByID(int id);
        void Save(List<RequestAccommodationReservation> requests);
        void SaveRequest();
        void SendRequest(AccommodationReservation SelectedReservation, String Comment, DateTime NewInitialDate, DateTime NewEndDate);
    }
}
