using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class AccommodationReservationHandler
    {
        private const string FilePath = "../../Resources/Data/accommodationReservations.csv";
        private readonly Serializer<AccommodationReservation> _serializer;
        public List<AccommodationReservation> _accommodationReservations;
        
        public AccommodationReservationHandler()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationReservations = Load();
        }

        public List<AccommodationReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationReservation> accommodationReservations)
        {
            _serializer.ToCSV(FilePath, accommodationReservations);
        }
    }
}
