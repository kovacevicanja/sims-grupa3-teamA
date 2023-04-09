using BookingProject.Domain;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class ToursGuestsHandler
    {
        private const string FilePath = "../../Resources/Data/toursGuests.csv";

        private readonly Serializer<ToursGuests> _serializer;

        public List<ToursGuests> _toursGuests;

        public ToursGuestsHandler()
        {
            _serializer = new Serializer<ToursGuests>();
            _toursGuests = Load();
        }

        public List<ToursGuests> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<ToursGuests> toursGuests)
        {
            _serializer.ToCSV(FilePath, toursGuests);
        }
    }
}
