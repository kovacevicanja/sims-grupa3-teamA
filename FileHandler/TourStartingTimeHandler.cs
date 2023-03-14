using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class TourStartingTimeHandler
    {
        private const string FilePath = "../../Resources/Data/tourStartingTime.csv";

        private readonly Serializer<TourDateTime> _serializer;

        public List<TourDateTime> _dates;

        public TourStartingTimeHandler()
        {
            _serializer = new Serializer<TourDateTime>();
            _dates = Load();
        }

        public List<TourDateTime> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourDateTime> dates)
        {
            _serializer.ToCSV(FilePath, dates);
        }
    }
}
