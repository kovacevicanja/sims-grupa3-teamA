using BookingProject.Domain;
using BookingProject.View;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using BookingProject.DependencyInjection;
using BookingProject.Services;

namespace BookingProject.Controller
{
    public class TourPresenceController 
    {
        private readonly ITourPresenceService _tourPresenceService;
        public TourPresenceController()
        {
            _tourPresenceService = Injector.CreateInstance<ITourPresenceService>();
        }

        public void Create(TourPresence presence)
        {
            _tourPresenceService.Create(presence);
        }

        public List<TourPresence> GetAll()
        {
            return _tourPresenceService.GetAll();
        }

        public TourPresence GetByID(int id)
        {
            return _tourPresenceService.GetByID(id);
        }


        public void SendNotification(User guest)
        {
            _tourPresenceService.SendNotification(guest);
        }

        public List<Notification> GetGuestNotifications(User guest)
        {
            return _tourPresenceService.GetGuestNotifications(guest);
        }

        public List<Tour> FindAttendedTours (User guest)
        {
            return _tourPresenceService.FindAttendedTours(guest);
        }

        public void DeleteNotificationFromCSV(Notification notification)
        {
            _tourPresenceService.DeleteNotificationFromCSV(notification);
        }

        public void WriteNotificationAgain(Notification n)
        {
            _tourPresenceService.WriteNotificationAgain(n);
        }

        public void Save()
        {
            _tourPresenceService.Save();
        }


    }
}
