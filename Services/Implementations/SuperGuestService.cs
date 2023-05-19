using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class SuperGuestService : ISuperGuestService
    {
        private ISuperGuestRepository _superGuestRepository;
        private IAccommodationReservationRepository _accommodationReservationRepository;
        private IUserRepository _userRepository;
        public SuperGuestService() { }
        public void Initialize()
        {
            _superGuestRepository = Injector.CreateInstance<ISuperGuestRepository>();
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            _userRepository = Injector.CreateInstance<IUserRepository>();
        }
        public void Create(SuperGuest guest)
        {
            _superGuestRepository.Create(guest);
        }
        public void Save(List<SuperGuest> guests)
        {
            _superGuestRepository.Save(guests);
        }

        public List<SuperGuest> GetAll()
        {
            return _superGuestRepository.GetAll();
        }

        public SuperGuest GetById(int id)
        {
            return _superGuestRepository.GetById(id);
        }

        public void CheckIfGuestIsSuper(User guest)
        {
            if (guest.IsSuper)
            {
                CheckRequirements(guest); // ako je vec super gost, moramo se pitati da li i dalje ispunjava uslove
            }
            else
            {
                CheckConditions(guest); // ako nije super gost gledamo da li ispunjava uslove da to postane
            }
        }

        public void CheckRequirements(User guest)
        {
            SuperGuest superGuest = _superGuestRepository.GetById(guest.Id);
            DateTime todayMidnight = DateTime.Now.AddHours(0).AddMinutes(0).AddSeconds(0);
            if (superGuest.StartDate.AddYears(1) <= todayMidnight 
            {
                CheckNumberOfReservationsAgain(guest);
            }
        }

        public void CheckNumberOfReservationsAgain(User guest)
        {
            int numberOfReservations = FindNumberOfReservations(guest);
            SuperGuest superGuest = _superGuestRepository.GetById(guest.Id);
            if (numberOfReservations >= 10)
            {
                UpdateSuperGuest(guest);
            }
            else
            {
                _superGuestRepository.Delete(superGuest);
                guest.IsSuper = false;
                _userRepository.Update(guest);
            }
        }


        public void UpdateSuperGuest(User guest)
        {
            SuperGuest superGuest = _superGuestRepository.GetById(guest.Id);
            superGuest.BonusPoints = 5;
            superGuest.StartDate = SetStartDateForGuest(guest);
            _superGuestRepository.Update(superGuest);
        }

        public void CheckConditions(User guest)
        {
            int numberOfReservations = FindNumberOfReservations(guest);
            if(numberOfReservations >= 10)
            {
                MakeSuperGuest(guest, numberOfReservations);
            }
        }

        public void MakeSuperGuest(User guest, int numberOfReservations)
        {
            SuperGuest superGuest = new SuperGuest();
            superGuest.Guest = guest;
            superGuest.NumberOfReservations = numberOfReservations;
            superGuest.BonusPoints = 5;
            superGuest.StartDate = SetStartDateForGuest(guest);
            guest.IsSuper = true;
            _userRepository.Update(guest);
            _superGuestRepository.Create(superGuest);
        }

        public DateTime SetStartDateForGuest(User guest)
        {
            List<AccommodationReservation> _guestReservations = new List<AccommodationReservation>(FindReservationsForGuestForLastYear(guest));
            _guestReservations = _guestReservations.OrderBy(r => r.EndDate).ToList();
            return _guestReservations[0].EndDate;
        }

        public int FindNumberOfReservations(User guest)
        {
            int numOfRes = 0;
            List<AccommodationReservation> _allReservations = new List<AccommodationReservation>(FindReservationsForGuestForLastYear(guest));
            foreach (AccommodationReservation reservation in _allReservations)
            {
                numOfRes++;
            }
            return numOfRes;
        }

        public List<AccommodationReservation> FindAllReservationsForGuest(User guest)
        {
            List<AccommodationReservation> _guestReservations = new List<AccommodationReservation>();
            List<AccommodationReservation> _allReservations = new List<AccommodationReservation>(_accommodationReservationRepository.GetAll());
            foreach(AccommodationReservation reservation in _allReservations)
            {
                if(guest.Id == reservation.Guest.Id)
                {
                    _guestReservations.Add(reservation);
                }
            }
            return _guestReservations;
        }

        public List<AccommodationReservation> FindReservationsForGuestForLastYear(User guest)
        {
            List<AccommodationReservation> _allReservations = new List<AccommodationReservation>(FindAllReservationsForGuest(guest));
            List<AccommodationReservation> _allResForLastYear = new List<AccommodationReservation>();
            DateTime todayMidnight = DateTime.Now.AddHours(0).AddMinutes(0).AddSeconds(0);

            foreach(AccommodationReservation reservation in _allReservations)
            {
                if(reservation.EndDate < todayMidnight && reservation.EndDate.AddYears(1) < todayMidnight)
                {
                    _allReservations.Add(reservation);
                }
            }

            return _allResForLastYear;
        }
    }
}
