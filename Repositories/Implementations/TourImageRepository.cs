using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class TourImageRepository : ITourImageRepository
    {
        private const string FilePath = "../../Resources/Data/tourImages.csv";
        private Serializer<TourImage> _serializer;
        public List<TourImage> _images;

        public TourImageRepository()
        {
            _serializer = new Serializer<TourImage>();
            _images = Load();
        }
        public void Initialize() { }

        public List<TourImage> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _images);
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach (TourImage image in _images)
            {
                if (image.Id > maxId)
                {
                    maxId = image.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(TourImage image)
        {
            image.Id = GenerateId();
            _images.Add(image);
            Save();
        }
        public void LinkToTour(int id)
        {
            foreach (TourImage image in _images)
            {
                if (image.Tour.Id == -1)
                {
                    image.Tour.Id = id;
                }
            }
        }
        public List<TourImage> GetAll()
        {
            return _images;
        }
        public TourImage GetById(int id)
        {
            return _images.Find(image => image.Id == id);
        }
    }
}
