using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private const string FilePath = "../../Resources/Data/notifications.csv";

        private Serializer<Notification> _serializer;

        public List<Notification> _notifications;

        public NotificationRepository()
        {
            _serializer = new Serializer<Notification>();
            _notifications = Load();
        }
        public void Initialize() { }

        public List<Notification> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save()
        {
            _serializer.ToCSV(FilePath, _notifications);
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
            Save();
        }

        public List<Notification> GetAll()
        {
            return _notifications.ToList();
        }

        public Notification GetById(int id)
        {
            return _notifications.Find(notification => notification.Id == id);
        }
        public void DeleteNotificationFromCSV(Notification notification)
        {
            _notifications.RemoveAll(n => n.Id == notification.Id);
            Save();
        }
    }
}