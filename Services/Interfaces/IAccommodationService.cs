using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IAccommodationService
    {
        void Initialize();
        bool CheckType(List<String> accommodationTypes, string accType);
        ObservableCollection<Accommodation> Search(ObservableCollection<Accommodation> _accommodationsView, string name, string city, string state, List<string> types, string numberOfGuests, string minNumDaysOfReservation);
        bool AccMatched(Accommodation accommodation, string name, string city, string state, List<string> types, string numberOfGuests, string minNumDaysOfReservation);
        bool CityMatched(Accommodation accommodation, string city);
        bool CountryMatched(Accommodation accommodation, string state);
        bool NameMatched(Accommodation accommodation, string name);
        bool TypeMatched(Accommodation accommodation, List<string> types);
        bool NumberOfGuestsMatched(Accommodation accommodation, string numberOfGuests);
        bool MinNumDaysOfReservationOfGuestsMatched(Accommodation accommodation, string minNumDaysOfReservation);
        void Create(Accommodation accommodation);
        List<Accommodation> GetAll();
        Accommodation GetById(int id);
        void SaveAccommodation();
        List<Accommodation> GetAllForOwner(int ownerId);
        void Save(List<Accommodation> accommodations);
        List<AccommodationRenovation> GetAccommodationData(List<AccommodationRenovation> renovations);
        void Delete(Accommodation acc);
    }
}
