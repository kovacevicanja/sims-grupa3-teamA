using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Model;
using BookingProject.Serializer;

namespace BookingProject.FileHandler
{
    internal class AccommodationDateHandler 
    {
        private const string FilePath = "../../Resources/Data/accommodationDates.csv";

        private readonly Serializer<AccommodationDate> _serializer;

        public List<AccommodationDate> _dates;

        public AccommodationDateHandler()
        {
            _serializer = new Serializer<AccommodationDate>();
            _dates = Load();
        }

        public List<AccommodationDate> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationDate> dates)
        {
            _serializer.ToCSV(FilePath, dates);
        }
    }
}
