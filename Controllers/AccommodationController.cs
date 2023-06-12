using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Services.Implementations;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class AccommodationController 
    {
        private readonly IAccommodationService _accommodationService;
        public AccommodationImageController AccommodationImageController { get; set; }
        public AccommodationLocationController AccommodationLocationController { get; set; }
        public AccommodationController()
        {
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            AccommodationLocationController = new AccommodationLocationController();
            AccommodationImageController = new AccommodationImageController();
        }
        public bool CheckType(List<String> accommodationTypes, string accType)
        {
            return _accommodationService.CheckType(accommodationTypes, accType);
        }
        public void SaveAccommodation()
        {
            _accommodationService.SaveAccommodation();
        }
        public ObservableCollection<Accommodation> Search(ObservableCollection<Accommodation> _accommodationsView, string name, string city, string state, List<string> types, string numberOfGuests, string minNumDaysOfReservation)
        {
            return _accommodationService.Search(_accommodationsView, name, city, state, types, numberOfGuests, minNumDaysOfReservation);
        }
        public bool AccMatched(Accommodation accommodation, string name, string city, string state, List<string> types, string numberOfGuests, string minNumDaysOfReservation)
        {
            return _accommodationService.AccMatched(accommodation, name, city, state, types, numberOfGuests, minNumDaysOfReservation);
        }
        public bool CityMatched(Accommodation accommodation, string city)
        {
            return _accommodationService.CityMatched(accommodation, city);
        }
        public bool CountryMatched(Accommodation accommodation, string state)
        {
            return _accommodationService.CountryMatched(accommodation, state);
        }
        public bool NameMatched(Accommodation accommodation, string name)
        {
            return _accommodationService.NameMatched(accommodation, name);
        }
        public bool TypeMatched(Accommodation accommodation, List<string> types)
        {
            return _accommodationService.TypeMatched(accommodation, types);
        }
        public bool NumberOfGuestsMatched(Accommodation accommodation, string numberOfGuests)
        {
            return _accommodationService.NumberOfGuestsMatched(accommodation, numberOfGuests);
        }
        public bool MinNumDaysOfReservationOfGuestsMatched(Accommodation accommodation, string minNumDaysOfReservation)
        {
            return _accommodationService.MinNumDaysOfReservationOfGuestsMatched(accommodation, minNumDaysOfReservation);
        }
        public void Create(Accommodation accommodation)
        {
            _accommodationService.Create(accommodation);
        }
        public List<Accommodation> GetAll()
        {
            return _accommodationService.GetAll();
        }
        public Accommodation GetById(int id)
        {
            return _accommodationService.GetById(id);
        }
        public List<AccommodationImage> GetAccommodationImages(int accId)
        {
            List<AccommodationImage> images = new List<AccommodationImage>();
            foreach(AccommodationImage i in AccommodationImageController.GetAll())
            {
                if(i.AccommodationId==accId) images.Add(i);
            }
            return images;
        }
        public void Delete(Accommodation accommodation)
        {
            _accommodationService.Delete(accommodation);
            foreach(AccommodationImage i in GetAccommodationImages(accommodation.Id))
            {
                AccommodationImageController.Delete(i);
            }
            AccommodationLocationController.Delete(accommodation.Location);
        }
        public void Save(List<Accommodation> accommodations)
        {
            _accommodationService.Save(accommodations);
        }
        /* public void SaveAccommodation()
         {
             _accommodationHandler.Save(_accommodations);
         }*/

        /*public void NotifyObservers()
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
        }*/
        public List<Accommodation> GetAllForOwner(int ownerId)
        {
           return _accommodationService.GetAllForOwner(ownerId);
        }
        public List<AccommodationRenovation> GetAccommodationData(List<AccommodationRenovation> renovations)
        {
            return _accommodationService.GetAccommodationData(renovations);
        }
        public bool AccommodationIsAvailable(Accommodation accommodation, int daysToStay)
		{
            return _accommodationService.AccommodationIsAvailable(accommodation, daysToStay);
		}
        public bool CheckGuestsNumber(Accommodation accommodation, int numberOfGuests)
		{
            return _accommodationService.CheckGuestsNumber(accommodation, numberOfGuests);
		}
        public bool AccommodationIsAvailableInRange(Accommodation accommodation, int daysToStay, DateTime initialDate, DateTime endDate)
        {
            return _accommodationService.AccommodationIsAvailableInRange(accommodation, daysToStay, initialDate, endDate);
        }
    }
}