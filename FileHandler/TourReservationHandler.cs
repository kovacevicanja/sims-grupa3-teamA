using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Model;
using BookingProject.Serializer;

namespace BookingProject.FileHandler
{
    internal class TourReservationHandler
    {
        private const string FilePath = "../../Resources/Data/tourReservations.csv";

        private readonly Serializer<TourReservation> _serializer;

        public List<TourReservation> _reservations;

        public TourReservationHandler()
        {
            _serializer = new Serializer<TourReservation>();
            _reservations = Load();
        }

        public List<TourReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourReservation> dates)
        {
            _serializer.ToCSV(FilePath, dates);
        }
    }
}
