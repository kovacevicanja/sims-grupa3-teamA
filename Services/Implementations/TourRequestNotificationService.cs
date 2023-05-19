using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class TourRequestNotificationService : ITourRequestNotificationService
    {
        private INotificationService _notificationService;
        private ITourRequestService _tourRequestService;

        public TourRequestNotificationService() { }

        public void Initialize()
        {
            _notificationService = Injector.CreateInstance<INotificationService>();
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
        }

        public void SendNotification(User guest, Tour createdTour)
        {
            Notification notification = new Notification();
            notification.UserId = guest.Id;
            notification.Text = "A tour you requested was created, go search it out, first instance of this tour will be held on  " + createdTour.StartingTime[0].StartingDateTime + "!";
            notification.Read = false;
            notification.RelatedTo = "Creating a tour on demand";
            _notificationService.Create(notification);
        }

        public void SystemSendingNotification(int guestId)
        {
            foreach (int id in _tourRequestService.GuestsForNotification().ToList())
            {
                if (guestId == id)
                {
                    Notification notification = new Notification();
                    notification.UserId = id;
                    notification.Text = "The tour you always wanted was made. View the offer in newly created tours.";
                    notification.Read = false;
                    notification.RelatedTo = "System notification about new tours";
                    _notificationService.Create(notification);
                }
            }
        }
    }
}
