using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class ReservationMovingRequestHandler
    {
        private const string FilePath = "../../Resources/Data/movingRequests.csv";
        private readonly Serializer<ReservationMovingRequest> _serializer;
        public List<ReservationMovingRequest> _movingRequests;

        public ReservationMovingRequestHandler()
        {
            _serializer = new Serializer<ReservationMovingRequest>();
            _movingRequests = Load();
        }

        public List<ReservationMovingRequest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<ReservationMovingRequest> movingRequests)
        {
            _serializer.ToCSV(FilePath, movingRequests);
        }
    }
}
