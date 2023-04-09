using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class Guest2Handler
    {
        private const string FilePath = "../../Resources/Data/guest2.csv";

        private readonly Serializer<Guest2> _serializer;

        public List<Guest2> _guests2;

        public Guest2Handler()
        {
            _serializer = new Serializer<Guest2>();
            _guests2 = Load();
        }

        public List<Guest2> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guest2> guests2)
        {
            _serializer.ToCSV(FilePath, guests2);
        }
    }
}
