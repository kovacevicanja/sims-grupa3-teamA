using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Model.Images;
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
        public AccommodationController()
        {
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
        }

        public bool CheckType(List<String> accommodationTypes, string accType)
        {
            return _accommodationService.CheckType(accommodationTypes, accType);
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

        public Accommodation GetByID(int id)
        {
            return _accommodationService.GetByID(id);
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
    }
}
