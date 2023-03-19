using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class TourGuestHandler
    {

        private const string FilePath = "../../Resources/Data/tourGuests.csv";

        private readonly Serializer<TourGuest> _serializer;

        public List<TourGuest> _guests;

        public TourGuestHandler()
        {
            _serializer = new Serializer<TourGuest>();
            _guests = Load();
        }

        public List<TourGuest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourGuest> guests)
        {
            _serializer.ToCSV(FilePath, guests);
        }
    }
}
