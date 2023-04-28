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
    public class AccommodationDateRepository : IAccommodationDateRepository
    {
        private const string FilePath = "../../Resources/Data/accommodationDates.csv";

        private Serializer<AccommodationDate> _serializer;

        public List<AccommodationDate> _dates;

        public AccommodationDateRepository() 
        {
            _serializer = new Serializer<AccommodationDate>();
            _dates = Load();
        }

        public void Initialize() { }

        public List<AccommodationDate> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationDate> dates)
        {
            _serializer.ToCSV(FilePath, dates);
        }

        public List<AccommodationDate> GetAll()
        {
            return _dates;
        }

        public AccommodationDate GetById(int id)
        {
            return _dates.Find(date => date.Id == id);
        }
    }
}
