using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class AccommodationController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly AccommodationHandler _accommodationHandler;

        private List<Accommodation> _accommodations;

        public LocationController _locationController;
        public ImageController _imageController;
        private Serializer<Accommodation> serializer;
        public AccommodationController()
        {
            serializer= new Serializer<Accommodation>();
            _accommodationHandler = new AccommodationHandler();
            _accommodations = new List<Accommodation>();
            _locationController = new LocationController();
            _imageController = new ImageController();
            Load();
        }

        public void Load()
        {
            _accommodations = _accommodationHandler.Load();
            AccommodationLocationBind();
            AccommodationImageBind();
        }

        public Accommodation UpdateAccommodation(Accommodation accommodation)
        {
            Accommodation oldAccommodation = GetByID(accommodation.Id);
            if (oldAccommodation == null) return null;

            oldAccommodation.AccommodationName = accommodation.AccommodationName;
            oldAccommodation.IdLocation = accommodation.IdLocation;
            oldAccommodation.Type = accommodation.Type;
            oldAccommodation.MaxGuestNumber= accommodation.MaxGuestNumber;
            oldAccommodation.MinDays = accommodation.MinDays;
            oldAccommodation.CancellationPeriod = accommodation.CancellationPeriod;

            SaveAccommodation();
            NotifyObservers();
            return oldAccommodation;
        }

        public void AccommodationImageBind()
        {
            List<AccommodationImage> images = new List<AccommodationImage>();
            ImageHandler imageHandler = new ImageHandler();
            images = imageHandler.Load();

            foreach (Accommodation accommodation in _accommodations)
            {
                foreach (AccommodationImage image in images)
                {

                    if (accommodation.Id == image.AccommodationId)
                    {
                        accommodation.Images.Add(image);
                    }

                }
            }
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }

        public void Create(Accommodation accommodation)
        {
            AddAccommodation(accommodation);
        }
        public Accommodation AddAccommodation(Accommodation accommodation)
        {
            accommodation.Id = GenerateId();
            _accommodations.Add(accommodation);
            SaveAccommodation();
            NotifyObservers();
            return accommodation;
        }

        private void SaveAccommodation()
        {
            _accommodationHandler.Save(_accommodations);
        }
        public void AddImageToAccommodation(Accommodation accommodation, AccommodationImage image)
        {
            accommodation.Images.Add(image);
            image.AccommodationId = accommodation.Id;
            UpdateAccommodation(accommodation);
            _imageController.UpdateImage(image);
        }

        private int GenerateId()
        {
            int maxId = 0;
            foreach (Accommodation accommodation in _accommodations)
            {
                if (accommodation.Id > maxId)
                {
                    maxId = accommodation.Id;
                }
            }
            return maxId + 1;
        }

        public Accommodation GetByID(int id)
        {
            return _accommodations.Find(accommodation => accommodation.Id == id);
        }

        public void AccommodationLocationBind()
        {
            _locationController.Load();
            foreach(Accommodation accommodation in _accommodations)
            {
                Location location = _locationController.GetByID(accommodation.IdLocation);
                accommodation.Location = location;
            }
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
