using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(AccommodationReservation accommodationReservation);
        List<Notification> GetOwnerNotifications(User owner);
        void WriteNotificationAgain(Notification n);
        void Create(Notification notification);
        List<Notification> GetAll();
        Notification GetById(int id);
        void Initialize();
        void Save();
        void DeleteNotificationFromCSV(Notification notification);
    }
}
