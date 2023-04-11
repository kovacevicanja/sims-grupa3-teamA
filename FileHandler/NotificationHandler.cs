using BookingProject.Domain;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class NotificationHandler
    {
        private const string FilePath = "../../Resources/Data/notifications.csv";

        private readonly Serializer<Notification> _serializer;

        public List<Notification> _notifications;

        public NotificationHandler()
        {
            _serializer = new Serializer<Notification>();
            _notifications = Load();
        }

        public List<Notification> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Notification> notifications)
        {
            _serializer.ToCSV(FilePath, notifications);
        }
    }
}
