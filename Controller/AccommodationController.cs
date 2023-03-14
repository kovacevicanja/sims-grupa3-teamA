using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Images;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public AccommodationLocationController _locationController;

        public AccommodationImageController _imageController;

        public AccommodationController()
        {
            _accommodationHandler = new AccommodationHandler();
            _accommodations = new List<Accommodation>();
            _locationController = new AccommodationLocationController();
            Load();
        }

        public void Load()
        {
            _accommodations = _accommodationHandler.Load();
            AccommodationLocationBind();
            AccommodationImagesBind();
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
            //UpdateAccommodation(accommodation);
            //_imageController.UpdateImage(image);
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

        public void AccommodationImagesBind()
        {
            List<AccommodationImage> images = new List<AccommodationImage>();
            AccommodationImageHandler accommodationImageHandler = new AccommodationImageHandler();
            images = accommodationImageHandler.Load();

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

        public ObservableCollection<Accommodation> Search(ObservableCollection<Accommodation> accommodationsView, string name, string city, string state, string type, string numberOfGuests, string minNumDaysOfReservation)
        {
            accommodationsView.Clear();

            foreach (Accommodation accommodation in _accommodations)
            {
                string typeEnum = accommodation.Type.ToString().ToLower();

                bool isSearched = (city.Equals("") || accommodation.Location.City.ToLower().Contains(city.ToLower()))
                    && (state.Equals("") || accommodation.Location.Country.ToLower().Contains(state.ToLower()))
                    && (name.Equals("") ||  accommodation.AccommodationName.ToLower().Contains(name.ToLower()))
                    && (type.Equals("") || typeEnum.Equals(type.ToLower()))
                    && (numberOfGuests.Equals("") || int.Parse(numberOfGuests) <= accommodation.MaxGuestNumber)
                    && (minNumDaysOfReservation.Equals("") || int.Parse(minNumDaysOfReservation) >= accommodation.MinDays);

                if (isSearched)
                {
                    accommodationsView.Add(accommodation);
                }
            }
            return accommodationsView;

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
