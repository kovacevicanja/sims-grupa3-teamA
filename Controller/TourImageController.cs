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

        private int GenerateId()
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
        }

        public void Save()
        {
            _imageHandler.Save(_images);
        }

        public void LinkToTour(int id)
        {

            foreach (TourImage image in _images)
            {
                if (image.TourId == -1)
                {
                    image.TourId = id;
                }

            }
        }

        public void CleanUnused()
        {
            _images.RemoveAll(i => i.TourId == -1);

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


