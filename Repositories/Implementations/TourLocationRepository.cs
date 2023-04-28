using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class TourLocationRepository : ITourLocationRepository
    {
        private const string FilePath = "../../Resources/Data/tourLocations.csv";
        private Serializer<Location> _serializer;
        public List<Location> _locations;

        public TourLocationRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = Load();
        }
        public void Initialize() { }
        public List<Location> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _locations);
        }
        private int GenerateId()
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
        public void Create(Location location)
        {
            location.Id = GenerateId();
            _locations.Add(location);
            Save();
        }
        public List<Location> GetAll()
        {
            return _locations.ToList();
        }
        public Location GetById(int id)
        {
            return _locations.Find(location => location.Id == id);
        }
    }
}