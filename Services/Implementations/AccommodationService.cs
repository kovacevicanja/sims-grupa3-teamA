using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class AccommodationService : IAccommodationService
    {
        private IAccommodationRepository _accommodationRepository;
        private IAccommodationReservationRepository _accommodationReservationRepository;
        private IAccommodationDateService _accommodationDateService;
        public AccommodationService() { }
        public void Initialize()
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            _accommodationDateService = Injector.CreateInstance<IAccommodationDateService>();
        }
        public void Save(List<Accommodation> accommodations)
        {
            _accommodationRepository.Save(accommodations);
        }
        public void SaveAccommodation()
        {
            _accommodationRepository.SaveAccommodation();
        }
        public bool CheckType(List<String> accommodationTypes, string accType)
        {
            return accommodationTypes == null || accommodationTypes.Count == 0 || accommodationTypes.Any(t => accType.Contains(t.ToLower()));
        }
        public void Delete(Accommodation acc)
        {
            _accommodationRepository.Delete(acc);
        }

        public ObservableCollection<Accommodation> Search(ObservableCollection<Accommodation> _accommodationsView, string name, string city, string state, List<string> types, string numberOfGuests, string minNumDaysOfReservation)
        {
            _accommodationsView.Clear();

            foreach (Accommodation accommodation in _accommodationRepository.GetAll())
            {
                if (AccMatched(accommodation, name, city, state, types, numberOfGuests, minNumDaysOfReservation))
                {
                    _accommodationsView.Add(accommodation);
                }
            }
            return _accommodationsView;

        }
        public List<AccommodationRenovation> GetAccommodationData(List<AccommodationRenovation> renovations)
        {
            List<Accommodation> accommodations = GetAll();

            foreach (AccommodationRenovation renovation in renovations)
            {
                renovation.Accommodation = accommodations.Find(a => a.Id == renovation.Accommodation.Id);
            }

            return renovations;
        }

        public bool AccMatched(Accommodation accommodation, string name, string city, string state, List<string> types, string numberOfGuests, string minNumDaysOfReservation)
        {
            return CityMatched(accommodation, city)
                && CountryMatched(accommodation, state)
                && NameMatched(accommodation, name)
                && TypeMatched(accommodation, types)
                && NumberOfGuestsMatched(accommodation, numberOfGuests)
                && MinNumDaysOfReservationOfGuestsMatched(accommodation, minNumDaysOfReservation);
        }

        public bool CityMatched(Accommodation accommodation, string city)
        {
            return string.IsNullOrEmpty(city) || accommodation.Location.City.ToLower().Contains(city.ToLower());
        }

        public bool CountryMatched(Accommodation accommodation, string state)
        {
            return string.IsNullOrEmpty(state) || accommodation.Location.Country.ToLower().Contains(state.ToLower());
        }

        public bool NameMatched(Accommodation accommodation, string name)
        {
            return string.IsNullOrEmpty(name) || accommodation.AccommodationName.ToLower().Contains(name.ToLower());
        }

        public bool TypeMatched(Accommodation accommodation, List<string> types)
        {
            return CheckType(types, accommodation.Type.ToString().ToLower());
        }

        public bool NumberOfGuestsMatched(Accommodation accommodation, string numberOfGuests)
        {
            return (string.IsNullOrEmpty(numberOfGuests) || int.Parse(numberOfGuests) <= accommodation.MaxGuestNumber);
        }

        public bool MinNumDaysOfReservationOfGuestsMatched(Accommodation accommodation, string minNumDaysOfReservation)
        {
            return string.IsNullOrEmpty(minNumDaysOfReservation) || int.Parse(minNumDaysOfReservation) >= accommodation.MinDays;
        }

        public void Create(Accommodation accommodation)
        {
            _accommodationRepository.Create(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return _accommodationRepository.GetAll();
        }

        public Accommodation GetById(int id)
        {
            return _accommodationRepository.GetById(id);
        }
        public List<Accommodation> GetAllForOwner(int ownerId)
        {
            List<Accommodation> accommodations = new List<Accommodation>();
            foreach (Accommodation accommodation in _accommodationRepository.GetAll())
            {
                if (accommodation.Owner.Id == ownerId)
                {
                    accommodations.Add(accommodation);
                }
            }
            return accommodations;
        }

        public bool CheckGuestsNumber(Accommodation accommodation, int numberOfGuests)
		{
            return accommodation.MaxGuestNumber >= numberOfGuests;
		}

        public bool AccommodationIsAvailable(Accommodation accommodation, int daysToStay)
		{
            if(_accommodationDateService.FindAvailableDatesQuick(accommodation, daysToStay).Count != 0)
			{
                return true;
			}
			else
			{
                return false;
			}
		}

        public bool AccommodationIsAvailableInRange(Accommodation accommodation, int daysToStay, DateTime initialDate, DateTime endDate)
		{
            if (_accommodationDateService.FindAvailableDatesQuickRanges(accommodation, daysToStay, initialDate, endDate).Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
