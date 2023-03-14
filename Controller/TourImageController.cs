using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Images;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{

    public class TourImageController: ISubject
    {
        private readonly List<IObserver> observers;

        private readonly TourImageHandler _imageHandler;

        private List<TourImage> _images;

        public TourImageController()
        {
            _imageHandler = new TourImageHandler();
            _images = new List<TourImage>();
            Load();
        }

        public void Load()
        {
            _images = _imageHandler.Load();
        }

        public List<TourImage> GetAll()
        {
            return _images;
        }
        private void SaveImage()
        {
            _imageHandler.Save(_images);
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


