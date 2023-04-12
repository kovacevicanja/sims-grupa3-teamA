using BookingProject.FileHandler;
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

namespace BookingProject.Controller
{
    public class TourPresenceController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly TourPresenceHandler _tourPresenceHandler;

        public NotificationController _notificationController { get; set; }

        private List<TourPresence> _presences;
        private TourTimeInstanceHandler _timeInstanceHandler { get; set; }
        public TourController TourController {get; set;}

        public TourPresenceController()
        {
            _tourPresenceHandler = new TourPresenceHandler();
            _presences = new List<TourPresence>();
            observers = new List<IObserver>();
            _notificationController = new NotificationController();
            _timeInstanceHandler = new TourTimeInstanceHandler();
            TourController = new TourController();
            Load();
        }

        public void Load()
        {
            _presences = _tourPresenceHandler.Load();
        }


        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourPresence tourPresence in _presences)
            {
                if (tourPresence.Id > maxId)
                {
                    maxId = tourPresence.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(TourPresence presence)
        {
            presence.Id = GenerateId();
            _presences.Add(presence);
            NotifyObservers();
        }

        public void Save()
        {
            _tourPresenceHandler.Save(_presences);
            NotifyObservers();
        }

        public List<TourPresence> GetAll()
        {
            return _presences;
        }

        public TourPresence GetByID(int id)
        {
            return _presences.Find(presence => presence.Id == id);
        }


        public void SendNotification(User guest)
        {
            Notification notification = new Notification();
            notification.Id = _notificationController.GenerateId();
            notification.UserId = guest.Id;
            notification.Text = "Presence check !!! ";
            notification.Read = false;
            _notificationController.Create(notification);
            _notificationController.Save();
        }

        public List<Notification> GetGuestNotifications(User guest)
        {
            List<Notification> notificationsForGuest = new List<Notification>();
            List<Notification> _notifications = _notificationController.GetAll();

            foreach (Notification notification in _notifications)
            {
                if (notification.UserId == guest.Id && notification.Read == false)
                {
                    notificationsForGuest.Add(notification);
                }
            }

            return notificationsForGuest;
        }

        public List<Tour> FindAttendedTours (User guest)
        {
            List <int> tourInstanceIds = new List<int>();
            List<TourReservation> attendedTours = new List<TourReservation>();
            List<TourTimeInstance> tourTimeInstances = _timeInstanceHandler.Load();
            List <Tour> tours = new List<Tour>();
            List<Tour> potentialTours = new List<Tour>();

            foreach (TourPresence tp in _presences)
            {
                if (tp.UserId == guest.Id && tp.KeyPointId != -1)
                {
                    tourInstanceIds.Add(tp.TourId);
                }
            }

            foreach (TourTimeInstance tti in tourTimeInstances)
            {
                foreach (int id in tourInstanceIds)
                {
                    if (id == tti.Id)
                    {
                        Tour tour = new Tour();
                        tour = TourController.GetByID(tti.TourId);
                        tours.Add(tour);
                    }
                }
            }
            return tours.Distinct().ToList();
        }

        public void DeleteNotificationFromCSV(Notification notification)
        {
            List<Notification> _notifications = _notificationController.GetAll();
            _notifications.RemoveAll(n => n.Id == notification.Id);
            _notificationController.Save();
        }

        public void WriteNotificationAgain(Notification n)
        {
            Notification notification = new Notification();
            notification.UserId = n.UserId;
            notification.Text = n.Text;
            notification.Read = true;
            _notificationController.Create(notification);
            _notificationController.Save();
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

    }
}
