using BookingProject.Domain.Images;
using BookingProject.FileHandler;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class AccommodationGuestImageController
    {
        private readonly List<IObserver> observers;

        private readonly AccommodationGuestImageHandler _imageHandler;

        private List<AccommodationGuestImage> _images;

        public AccommodationGuestImageController()
        {
            _imageHandler = new AccommodationGuestImageHandler();
            _images = new List<AccommodationGuestImage>();
            Load();
        }

        public void Load()
        {
            _images = _imageHandler.Load();
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
        }

        public void SaveImage()
        {
            _imageHandler.Save(_images);
        }

        public AccommodationGuestImage GetByID(int id)
        {
            return _images.Find(image => image.Id == id);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }
    }
}
