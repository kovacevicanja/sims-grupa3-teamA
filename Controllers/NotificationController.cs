using BookingProject.Domain;
using BookingProject.FileHandler;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class NotificationController : ISubject
    {
        private readonly List<IObserver> observers;
        private readonly NotificationHandler _notificationHandler;
        private List<Notification> _notifications;

        public NotificationController()
        {
            observers = new List<IObserver>();
            _notificationHandler = new NotificationHandler();
            Load();
        }

        public void Load()
        {
            _notifications = _notificationHandler.Load();
        }

        public int GenerateId()
        {
            int maxId = 0;
            foreach (Notification notification in _notifications)
            {
                if (notification.Id > maxId)
                {
                    maxId = notification.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(Notification notification)
        {
            notification.Id = GenerateId();
            _notifications.Add(notification);
            NotifyObservers();
        }

        public void Save()
        {
            _notificationHandler.Save(_notifications);
            NotifyObservers();
        }

        public List<Notification> GetAll()
        {
            return _notifications;
        }

        public Notification GetByID(int id)
        {
            return _notifications.Find(notification => notification.Id == id);
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