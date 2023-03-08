using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class AccommodationHandler
    {
        private const string FilePath = "../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;

        public List<Accommodation> _accommodations;

        public AccommodationHandler()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = Load();
        }

        public List<Accommodation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Accommodation> accommodations)
        {
            _serializer.ToCSV(FilePath, accommodations);
        }
    }
}
