using BookingProject.FileHandler;
using BookingProject.Model.Images;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class AccommodationImageController
    {
        private readonly List<IObserver> observers;

        private readonly AccommodationImageHandler _imageHandler;

        private List<AccommodationImage> _images;

        public AccommodationImageController()
        {
            _imageHandler = new AccommodationImageHandler();
            _images = new List<AccommodationImage>();
            Load();
        }

        public void Load()
        {
            _images = _imageHandler.Load();
        }

        public List<AccommodationImage> GetAll()
        {
            return _images;
        }

        public AccommodationImage GetByID(int id)
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
