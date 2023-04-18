using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model.Images;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class TourPresenceRepository : ITourPresenceRepository
    {
        private const string FilePath = "../../Resources/Data/tourPresence.csv";

        private readonly Serializer<TourPresence> _serializer;

        public List<TourPresence> _presences;

        public TourPresenceRepository()
        {
            _serializer = new Serializer<TourPresence>();
            _presences = Load();
        }

        public List<TourPresence> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourPresence> presences)
        {
            _serializer.ToCSV(FilePath, presences);
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
            Save(_presences);
        }

        public List<TourPresence> GetAll()
        {
            return _presences.ToList();
        }
        public TourPresence GetByID(int id)
        {
            return _presences.Find(presence => presence.Id == id);
        }

    }
}
