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

        public List<TourImage> GetAll()
        {
            return _images;
        }
        private void SaveImage()
        {
            _imageHandler.Save(_images);
        }
        public AccommodationImage UpdateImage(AccommodationImage image)
        {
            AccommodationImage oldImage = GetByID(image.Id);
            if (oldImage == null) return null;

            oldImage.Url = image.Url;
            oldImage.AccommodationId = image.AccommodationId;

            SaveImage();
            NotifyObservers();
            return oldImage;
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


