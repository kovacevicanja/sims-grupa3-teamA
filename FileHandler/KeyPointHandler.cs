using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class KeyPointHandler
    {
        private const string FilePath = "../../Resources/Data/keyPoints.csv";

        private readonly Serializer<KeyPoint> _serializer;

        public List<KeyPoint> _keyPoints;

        public KeyPointHandler()
        {
            _serializer = new Serializer<KeyPoint>();
            _keyPoints = Load();
        }

        public List<KeyPoint> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<KeyPoint> keyPoints)
        {
            _serializer.ToCSV(FilePath, keyPoints);
        }
    }
}
