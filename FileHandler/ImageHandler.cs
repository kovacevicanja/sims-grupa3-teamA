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

        private readonly Serializer<TourImage> _serializer;

        public List<TourImage> _images;

        public ImageHandler()
        {
            _serializer = new Serializer<TourImage>();
            _images = Load();
        }

        public List<TourImage> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourImage> images)
        {
            _serializer.ToCSV(FilePath, images);
        }
    }
}
