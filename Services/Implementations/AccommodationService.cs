﻿using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
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
        public AccommodationService() { }
        public void Initialize()
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }
        public bool CheckType(List<String> accommodationTypes, string accType)
        {
            return accommodationTypes == null || accommodationTypes.Count == 0 || accommodationTypes.Any(t => accType.Contains(t.ToLower()));
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

        public Accommodation GetByID(int id)
        {
            return _accommodationRepository.GetByID(id);
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
    }
}
