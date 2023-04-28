using BookingProject.Domain.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class AccommodationGuestImageRepository : IAccommodationGuestImageRepository
    {
        private const string FilePath = "../../Resources/Images/accommodationguestimages.csv";

        private Serializer<AccommodationGuestImage> _serializer;

        public List<AccommodationGuestImage> _images;

        public AccommodationGuestImageRepository()
        {
            _serializer = new Serializer<AccommodationGuestImage>();
            _images = Load();
        }
        public void Initialize() { }

        public List<AccommodationGuestImage> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationGuestImage> images)
        {
            _serializer.ToCSV(FilePath, images);
        }
        public void SaveImage()
        {
            _serializer.ToCSV(FilePath, _images);
        }
        public List<AccommodationGuestImage> GetAll()
        {
            return _images;
        }

        public int GenerateId()
        {
            int maxId = 0;
            foreach (AccommodationGuestImage image in _images)
            {
                if (image.Id > maxId)
                {
                    maxId = image.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(AccommodationGuestImage image)
        {
            image.Id = GenerateId();
            _images.Add(image);
            Save(_images);
        }

        public AccommodationGuestImage GetById(int id)
        {
            return _images.Find(image => image.Id == id);
        }
    }
}
