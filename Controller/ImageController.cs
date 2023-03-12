using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{

    public class ImageController: ISubject
    {
        private readonly List<IObserver> observers;

        private readonly ImageHandler _imageHandler;

        private List<TourImage> _images;

        public ImageController()
        {
            _imageHandler = new ImageHandler();
            _images = new List<TourImage>();
            Load();
        }

        public void Load()
        {
            _images = _imageHandler.Load();
        }

        public void Create(TourImage image) 
        {
            _images.Add(image);
        }

        public void Save()
        {
            _imageHandler.Save(_images);
        }

        public List<TourImage> GetAll()
        {
            return _images;
        }

        public TourImage GetByID(int id)
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


