using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class TourHandler
    {
        private const string FilePath = "../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        public List<Tour> _tours;

        public TourHandler()
        {
            _serializer = new Serializer<Tour>();
            _tours = Load();
        }

        public List<Tour> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Tour> tours)
        {
            _serializer.ToCSV(FilePath, tours);
        }
    }
}
