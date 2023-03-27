using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class TourTimeInstanceHandler
    {
        private const string FilePath = "../../Resources/Data/tourTimeInstances.csv";

        private readonly Serializer<TourTimeInstance> _serializer;

        public List<TourTimeInstance> _tourInstances;

        public TourTimeInstanceHandler()
        {
            _serializer = new Serializer<TourTimeInstance>();
            _tourInstances = Load();
        }

        public List<TourTimeInstance> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourTimeInstance> tourInstances)
        {
            _serializer.ToCSV(FilePath, tourInstances);
        }
    }
}
