using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class TourPresenceHandler
    {
        private const string FilePath = "../../Resources/Data/tourPresence.csv";

        private readonly Serializer<TourPresence> _serializer;

        public List<TourPresence> _tourPresences;

        public TourPresenceHandler()
        {
            _serializer = new Serializer<TourPresence>();
            _tourPresences = Load();
        }

        public List<TourPresence> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourPresence> tourPresences)
        {
            _serializer.ToCSV(FilePath, tourPresences);
        }
    }
}
