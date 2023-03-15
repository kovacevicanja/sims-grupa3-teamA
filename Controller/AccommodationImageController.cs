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
        }



        //public AccommodationImage UpdateImage(AccommodationImage image)
        //{
        //    AccommodationImage oldImage = GetByID(image.Id);
        //    if (oldImage == null) return null;

        //    oldImage.Url = image.Url;
        //    oldImage.AccommodationId = image.AccommodationId;

        //    SaveImage();
        //    NotifyObservers();
        //    return oldImage;
        //}

        public void LinkToAccommodation(int id)
        {

            foreach (AccommodationImage image in _images)
            {
                if (image.AccommodationId == -1)
                {
                    image.AccommodationId = id;
                }

            }
        }

        public void DeleteUnused()
        {
            _images.RemoveAll(i => i.AccommodationId == -1);

        }

        public void SaveImage()
        {
            _imageHandler.Save(_images);
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
