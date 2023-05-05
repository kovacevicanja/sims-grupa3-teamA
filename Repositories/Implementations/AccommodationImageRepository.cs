using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class AccommodationImageRepository : IAccommodationImageRepository
    {
        private const string FilePath = "../../Resources/Images/accommodationimages.csv";

        private Serializer<AccommodationImage> _serializer;

        public List<AccommodationImage> _images;

        public AccommodationImageRepository()
        {
            _serializer = new Serializer<AccommodationImage>();
            _images = Load();
        }

        public void Initialize() { }

        public List<AccommodationImage> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationImage> images)
        {
            _serializer.ToCSV(FilePath, images);
        }
        public void SaveImage()
        {
            _serializer.ToCSV(FilePath, _images);
        }
        public List<AccommodationImage> GetAll()
        {
            return _images;
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (AccommodationImage image in _images)
            {
                if (image.Id > maxId)
                {
                    maxId = image.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(AccommodationImage image)
        {
            image.Id = GenerateId();
            _images.Add(image);
            Save(_images);
        }
        public AccommodationImage GetById(int id)
        {
            return _images.Find(image => image.Id == id);
        }
    }
}
