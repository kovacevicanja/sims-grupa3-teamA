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

        private LocationController _locationController;

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
            AccommodationImagesBind();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
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
                Location location = _locationController.GetByID(accommodation.Location.Id);
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
                    && (name.Equals("") ||  accommodation.Name.ToLower().Contains(name.ToLower()))
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
