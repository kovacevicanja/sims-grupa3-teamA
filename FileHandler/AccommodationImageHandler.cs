using BookingProject.Model.Images;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    internal class AccommodationImageHandler
    {
        private const string FilePath = "../../Resources/Images/accommodationimages.csv";

        private readonly Serializer<AccommodationImage> _serializer;

        public List<AccommodationImage> _images;

        public AccommodationImageHandler()
        {
            _serializer = new Serializer<AccommodationImage>();
            _images = Load();
        }

        public List<AccommodationImage> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationImage> images)
        {
            _serializer.ToCSV(FilePath, images);
        }
    }
}
