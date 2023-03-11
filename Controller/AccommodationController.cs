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

        private LocationController _locationController;
        private Serializer<Accommodation> serializer;
        private readonly string fileName = "../../Resources/Data/accommodations.csv";
        public AccommodationController()
        {
            _accommodationHandler = new AccommodationHandler();
            _accommodations = new List<Accommodation>();
            _locationController = new LocationController();
            Load();
        }

        public void Load()
        {
            _accommodations = _accommodationHandler.Load();
            AccommodationLocationBind();
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
            serializer.ToCSV(fileName, _accommodations);
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
            foreach(var observer in observers)
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
