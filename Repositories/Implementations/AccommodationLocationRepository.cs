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
    public class AccommodationLocationRepository : IAccommodationLocationRepository
    {
        private const string FilePath = "../../Resources/Data/accommodationLocations.csv";

        private Serializer<Location> _serializer;

        public List<Location> _locations;

        public AccommodationLocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = Load();
        }

        public void Initialize() { }

        public List<Location> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Location> locations)
        {
            _serializer.ToCSV(FilePath, locations);
        }
        public void SaveLocation()
        {
            _serializer.ToCSV(FilePath, _locations);
        }
        public List<Location> GetAll()
        {
            return _locations;
        }

        public Location GetById(int id)
        {
            return _locations.Find(location => location.Id == id);
        }

        public void Create(Location location)
        {
            location.Id = GenerateId();
            _locations.Add(location);
            Save(_locations);
        }

        public int GenerateId()
        {
            int maxId = 0;
            foreach (Location location in _locations)
            {
                if (location.Id > maxId)
                {
                    maxId = location.Id;
                }
            }
            return maxId + 1;
        }
    }
}
