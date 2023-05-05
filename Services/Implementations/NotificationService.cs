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
    internal class NotificationService : INotificationService
    {
        public INotificationRepository _notificationRepository { get; set; }
        public NotificationService() { }
        public void Initialize()
        {
            _notificationRepository = Injector.CreateInstance<INotificationRepository>();
        }

        public void SendNotification(AccommodationReservation accommodationReservation)
        {
            Notification notification = new Notification();
            //notification.Id = Injector.CreateInstance<INotificationService>().GenerateId();
            notification.UserId = accommodationReservation.Accommodation.Owner.Id;
            notification.Text = "Reservation for your accommodation " + accommodationReservation.Accommodation.AccommodationName + ", from " + DateConversion.DateToStringAccommodation(accommodationReservation.InitialDate) + ", to " + DateConversion.DateToStringAccommodation(accommodationReservation.EndDate) + " was cancelled!";
            notification.Read = false;
            Injector.CreateInstance<INotificationService>().Create(notification);
            //_notificationService.Save();
        }

        public List<Notification> GetOwnerNotifications(User owner)
        {
            List<Notification> notificationsForOwner = new List<Notification>();
            List<Notification> _notifications = Injector.CreateInstance<INotificationService>().GetAll();

            foreach (Notification notification in _notifications)
            {
                if (notification.UserId == owner.Id && notification.Read == false)
                {
                    notificationsForOwner.Add(notification);
                }
            }
            return notificationsForOwner;
        }
        public void WriteNotificationAgain(Notification n)
        {
            Notification notification = new Notification();
            notification.UserId = n.UserId;
            notification.Text = n.Text;
            notification.Read = true;
            Injector.CreateInstance<INotificationService>().Create(notification);
            //_notificationService.Save();
        }
        public void Create(Notification notification)
        {
            _notificationRepository.Create(notification);
        }

        public List<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public Notification GetById(int id)
        {
            return _notificationRepository.GetById(id);    
        }
        public void DeleteNotificationFromCSV(Notification notification)
        {
            _notificationRepository.DeleteNotificationFromCSV(notification);
        }
        public void Save()
        {
            _notificationRepository.Save();
        }
    }
}