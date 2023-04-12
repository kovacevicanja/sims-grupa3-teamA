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
    public class TourEvaluationImageHandler
    {
        private const string FilePath = "../../Resources/Data/tourEvaluationImages.csv";

        private readonly Serializer<TourEvaluationImage> _serializer;

        public List<TourEvaluationImage> _images;

        public TourEvaluationImageHandler()
        {
            _serializer = new Serializer<TourEvaluationImage>();
            _images = Load();
        }

        public List<TourEvaluationImage> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourEvaluationImage> images)
        {
            _serializer.ToCSV(FilePath, images);
        }
    }
}
