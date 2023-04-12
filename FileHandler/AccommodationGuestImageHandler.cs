using BookingProject.Domain.Images;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class AccommodationGuestImageHandler
    {
        private const string FilePath = "../../Resources/Images/accommodationguestimages.csv";

        private readonly Serializer<AccommodationGuestImage> _serializer;

        public List<AccommodationGuestImage> _images;

        public AccommodationGuestImageHandler()
        {
            _serializer = new Serializer<AccommodationGuestImage>();
            _images = Load();
        }

        public List<AccommodationGuestImage> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationGuestImage> images)
        {
            _serializer.ToCSV(FilePath, images);
        }
    }
}
