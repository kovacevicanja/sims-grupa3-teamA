using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourPresenceService
    {
        void Create(TourPresence presence);
        List<TourPresence> GetAll();
        TourPresence GetByID(int id);
        void Initialize();
        void SendNotification(User guest);
        List<Notification> GetGuestNotifications(User guest);
        List<Tour> FindAttendedTours(User guest);
        void DeleteNotificationFromCSV(Notification notification);
        void WriteNotificationAgain(Notification n);
    }
}
