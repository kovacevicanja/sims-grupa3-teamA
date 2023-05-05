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
    public class NotificationController 
    {
        private readonly INotificationService _notificationService;
        public NotificationController()
        {
            _notificationService = Injector.CreateInstance<INotificationService>();
        }
        public void SendNotification(AccommodationReservation accommodationReservation)
        {
            _notificationService.SendNotification(accommodationReservation);
        }
        public List<Notification> GetOwnerNotifications(User owner)
        {
            return _notificationService.GetOwnerNotifications(owner);
        }
        public void WriteNotificationAgain(Notification n)
        {
            _notificationService.WriteNotificationAgain(n);
        }
        public void Create(Notification notification)
        {
            _notificationService.Create(notification);
        }
        public List<Notification> GetAll()
        {
            return _notificationService.GetAll();
        }
        public Notification GetById(int id)
        {
            return _notificationService.GetById(id);
        }
        public void Save()
        {
            _notificationService.Save();
        }
        public void DeleteNotificationFromCSV(Notification notification)
        {
            _notificationService.DeleteNotificationFromCSV(notification);
        }
    }
}