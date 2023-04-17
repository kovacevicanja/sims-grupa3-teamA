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

        private AccommodationLocationController _locationController;

        private AccommodationImageController _imageController;
        private UserController _userController;


        public AccommodationController()
        {
            _accommodationHandler = new AccommodationHandler();
            _accommodations = new List<Accommodation>();
            _locationController = new AccommodationLocationController();
            _imageController = new AccommodationImageController();
            _userController = new UserController();
            Load();
        }

        public void Load()
        {
            _accommodations = _accommodationHandler.Load();
            AccommodationLocationBind();
            AccommodationImagesBind();
            AccommodationOwnerBind();
            AccommodationUserBind();
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }
        public List<Accommodation> GetAllForOwner(int ownerId)
        {
            List<Accommodation> accommodations= new List<Accommodation>();
            foreach(Accommodation accommodation in _accommodations)
            {
                if (accommodation.Owner.Id == ownerId)
                {
                    accommodations.Add(accommodation);
                }
            }
            return accommodations;
        }

        public void Create(Accommodation accommodation)
        {
            accommodation.Id = GenerateId();
            _accommodations.Add(accommodation);
        }

        public void SaveAccommodation()
        {
            _accommodationHandler.Save(_accommodations);
        }
        public void AddImageToAccommodation(Accommodation accommodation, AccommodationImage image)
        {
            accommodation.Images.Add(image);
            image.AccommodationId = accommodation.Id;
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
            foreach (Accommodation accommodation in _accommodations)
            {
                Location location = _locationController.GetByID(accommodation.IdLocation);
                accommodation.Location = location;
            }
        }
        public void AccommodationUserBind()
        {

            foreach (Accommodation accommodation in _accommodations)
            {
                User user = _userController.GetByID(accommodation.Owner.Id);
                accommodation.Owner = user;
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

        public void AccommodationOwnerBind()
        {
            _userController.Load();
            foreach(Accommodation accommodation in _accommodations)
            {
                User owner = _userController.GetByID(accommodation.Owner.Id);
                accommodation.Owner = owner;
            }
        }

        public bool CheckType(List<String> accommodationTypes, string accType)
        {
            return accommodationTypes == null || accommodationTypes.Count == 0 || accommodationTypes.Any(t => accType.Contains(t.ToLower()));
        }

        public ObservableCollection<Accommodation> Search(ObservableCollection<Accommodation> _accommodationsView, string name, string city, string state, List<string> types, string numberOfGuests, string minNumDaysOfReservation)
        {
            _accommodationsView.Clear();

            foreach (Accommodation accommodation in _accommodations)
            {
                if (AccMatched(accommodation, name, city, state, types, numberOfGuests, minNumDaysOfReservation))
                {
                    _accommodationsView.Add(accommodation);
                }
            }
            return _accommodationsView;

        }

        public bool AccMatched(Accommodation accommodation, string name, string city, string state, List<string> types, string numberOfGuests, string minNumDaysOfReservation)
        {
            if (CityMatched(accommodation, city)
                && CountryMatched(accommodation, state)
                && NameMatched(accommodation, name)
                && TypeMatched(accommodation, types)
                && NumberOfGuestsMatched(accommodation, numberOfGuests)
                && MinNumDaysOfReservationOfGuestsMatched(accommodation, minNumDaysOfReservation)) { return true; }
            else { return false; }
        }

        public bool CityMatched(Accommodation accommodation, string city)
        {
            if (string.IsNullOrEmpty(city) || accommodation.Location.City.ToLower().Contains(city.ToLower())) { return true; }
            else { return false; }
        }

        public bool CountryMatched(Accommodation accommodation, string state)
        {
            if (string.IsNullOrEmpty(state) || accommodation.Location.Country.ToLower().Contains(state.ToLower())) { return true; }
            else { return false; }
        }

        public bool NameMatched(Accommodation accommodation, string name)
        {
            if (string.IsNullOrEmpty(name) || accommodation.AccommodationName.ToLower().Contains(name.ToLower())) { return true; }
            else { return false; }
        }

        public bool TypeMatched(Accommodation accommodation, List<string> types)
        {
            if (CheckType(types, accommodation.Type.ToString().ToLower())) { return true; }
            else { return false; }
        }

        public bool NumberOfGuestsMatched(Accommodation accommodation, string numberOfGuests)
        {
            if ((string.IsNullOrEmpty(numberOfGuests) || int.Parse(numberOfGuests) <= accommodation.MaxGuestNumber)) { return true; }
            else { return false; }
        }
        public bool MinNumDaysOfReservationOfGuestsMatched(Accommodation accommodation, string minNumDaysOfReservation)
        {
            if (string.IsNullOrEmpty(minNumDaysOfReservation) || int.Parse(minNumDaysOfReservation) >= accommodation.MinDays) { return true; }
            else { return false; }
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
