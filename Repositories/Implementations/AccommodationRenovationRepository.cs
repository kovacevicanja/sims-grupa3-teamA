using BookingProject.Domain;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class AccommodationRenovationRepository : IAccommodationRenovationRepository
    {
        private const string FilePath = "../../Resources/Images/accommodationRenovations.csv";

        private Serializer<AccommodationRenovation> _serializer;

        public List<AccommodationRenovation> _renovations;

        public AccommodationRenovationRepository()
        {
            _serializer = new Serializer<AccommodationRenovation>();
            _renovations = Load();
        }

        public void Initialize() { }

        public List<AccommodationRenovation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationRenovation> renovations)
        {
            _serializer.ToCSV(FilePath, renovations);
        }
        public void SaveRenovation()
        {
            _serializer.ToCSV(FilePath, _renovations);
        }
        public List<AccommodationRenovation> GetAll()
        {
            return _renovations;
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (AccommodationRenovation renovation in _renovations)
            {
                if (renovation.Id > maxId)
                {
                    maxId = renovation.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(AccommodationRenovation renovation)
        {
            renovation.Id = GenerateId();
            _renovations.Add(renovation);
            Save(_renovations);
        }
        public AccommodationRenovation GetById(int id)
        {
            return _renovations.Find(renovation => renovation.Id == id);
        }
        public void Delete(AccommodationRenovation accommodationRenovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            AccommodationRenovation founded = _renovations.Find(c => c.Id == accommodationRenovation.Id);
            _renovations.Remove(founded);
            _serializer.ToCSV(FilePath, _renovations);
        }

        public AccommodationRenovation Update(AccommodationRenovation accommodationRenovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            AccommodationRenovation current = _renovations.Find(c => c.Id == accommodationRenovation.Id);
            int index = _renovations.IndexOf(current);
            _renovations.Remove(current);
            _renovations.Insert(index, accommodationRenovation);
            _serializer.ToCSV(FilePath, _renovations);
            return accommodationRenovation;
        }
    }
}
