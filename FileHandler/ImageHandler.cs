using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class ImageHandler
    {
        private const string FilePath = "../../Resources/Data/images.csv";

        private readonly Serializer<AccommodationImage> _serializer;

        public List<AccommodationImage> _images;

        public ImageHandler()
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
