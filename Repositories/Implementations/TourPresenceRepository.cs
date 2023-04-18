using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model.Images;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Controllers;

namespace BookingProject.Repositories.Implementations
{
    public class TourPresenceRepository : ITourPresenceRepository
    {
        private const string FilePath = "../../Resources/Data/tourPresence.csv";
        private Serializer<TourPresence> _serializer;
        public List<TourPresence> _presences;

        public INotificationRepository _notificationRepository { get; set; }
        public TourPresenceRepository()
        {
            _serializer = new Serializer<TourPresence>();
            _presences = Load();
        }
        public void Initialize() 
        {
            _notificationRepository = Injector.CreateInstance<INotificationRepository>();
        }
        public List<TourPresence> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _presences);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourPresence presence in _presences)
            {
                if (presence.Id > maxId)
                {
                    maxId = presence.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(TourPresence presence)
        {
            presence.Id = GenerateId();
            _presences.Add(presence);
            Save();
        }
        public List<TourPresence> GetAll()
        {
            return _presences.ToList();
        }
        public TourPresence GetByID(int id)
        {
            return _presences.Find(presence => presence.Id == id);
        }
        public void DeleteNotificationFromCSV(Notification notification)
        {
            _notificationRepository.DeleteNotificationFromCSV(notification);
        }
    }
}
