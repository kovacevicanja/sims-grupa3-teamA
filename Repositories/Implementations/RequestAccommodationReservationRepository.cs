using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class RequestAccommodationReservationRepository : IRequestAccommodationReservationRepository
    {
        private const string FilePath = "../../Resources/Data/accommodationReservationRequests.csv";

        private Serializer<RequestAccommodationReservation> _serializer;

        public List<RequestAccommodationReservation> _requests;
        
        public RequestAccommodationReservationRepository() { }

        public void Initialize()
        {
            _serializer = new Serializer<RequestAccommodationReservation>();
            _requests = Load();
            RequestReservationBind();
        }

        public List<RequestAccommodationReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<RequestAccommodationReservation> requests)
        {
            _serializer.ToCSV(FilePath, requests);
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

        public void Create(RequestAccommodationReservation request)
        {
            request.Id = GenerateId();
            _requests.Add(request);
        }


        public RequestAccommodationReservation GetByID(int id)
        {
            return _requests.Find(image => image.Id == id);
        }

        public void RequestReservationBind()
        {

            foreach (RequestAccommodationReservation request in _requests)
            {
                AccommodationReservation accommodation = Injector.CreateInstance<IAccommodationReservationRepository>().GetByID(request.AccommodationReservation.Id); // DODATI KAD PULUJEM OD ANJE
                request.AccommodationReservation = accommodation;
            }
        }

        /*public void DeleteNotificationFromCSV(Notification notification)
        {
            List<Notification> _notifications = _notificationController.GetAll();
            _notifications.RemoveAll(n => n.Id == notification.Id);
            _notificationController.Save();
        } -- PITATI

        public void WriteNotificationAgain(Notification n)
        {
            Notification notification = new Notification();
            notification.UserId = n.UserId;
            notification.Text = n.Text;
            notification.Read = true;
            _notificationController.Create(notification);
            _notificationController.Save();
        }*/
    }
}
