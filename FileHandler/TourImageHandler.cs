using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class TourImageHandler
    {
        private const string FilePath = "../../Resources/Data/tourImages.csv";

        private readonly Serializer<TourImage> _serializer;

        public List<TourImage> _images;

        public TourImageHandler()
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
