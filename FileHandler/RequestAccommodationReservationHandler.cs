using BookingProject.Domain;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class RequestAccommodationReservationHandler
    {
        private const string FilePath = "../../Resources/Data/accommodationReservationRequests.csv";

        private readonly Serializer<RequestAccommodationReservation> _serializer;

        public List<RequestAccommodationReservation> _requests;

        public RequestAccommodationReservationHandler()
        {
            _serializer = new Serializer<RequestAccommodationReservation>();
            _requests = Load();
        }

        public List<RequestAccommodationReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<RequestAccommodationReservation> requests)
        {
            _serializer.ToCSV(FilePath, requests);
        }
    }
}
