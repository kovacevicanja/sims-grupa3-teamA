using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class StartingDateHandler
    {
        private const string FilePath = "../../Resources/Data/dates.csv";

        private readonly Serializer<StartingDate> _serializer;

        public List<StartingDate> _dates;

        public StartingDateHandler()
        {
            _serializer = new Serializer<StartingDate>();
            _dates = Load();
        }

        public List<StartingDate> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<StartingDate> dates)
        {
            _serializer.ToCSV(FilePath, dates);
        }
    }
}
