using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.ConversionHelp;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class AccommodationReservationService : IAccommodationReservationService, ISubject
    {
        private IAccommodationReservationRepository _accommodationReservationRepository;
        private IUserService _userService;
        private List<IObserver> observers;
        public AccommodationReservationService() {
        }
        public void Initialize()
        {
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            _userService = Injector.CreateInstance<IUserService>();
            observers = new List<IObserver>();

        }
        public void Create(AccommodationReservation reservation)
        {
            _accommodationReservationRepository.Create(reservation);
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservationRepository.GetAll();
        }

        public AccommodationReservation GetById(int id)
        {
            return _accommodationReservationRepository.GetById(id);
        }
        public List<AccommodationReservation> getReservationsForGuest(User loggedInUser)
        {
            List<AccommodationReservation> _accResForGuest = new List<AccommodationReservation>();

            foreach (AccommodationReservation reservation in _accommodationReservationRepository.GetAll())
            {
                if (reservation.Guest.Id == loggedInUser.Id)
                {
                    _accResForGuest.Add(reservation);
                }
            }

            return _accResForGuest;
        }

        public void Update(AccommodationReservation reservation)
        {
            AccommodationReservation oldReservation = GetById(reservation.Id);
            if (oldReservation == null)
            {
                return;
            }
            oldReservation.InitialDate = reservation.InitialDate;
            oldReservation.EndDate = reservation.EndDate;
            _accommodationReservationRepository.Save();
        }

        public bool IsReservationAvailable(AccommodationReservation accommodationReservation)
        {
            return accommodationReservation.EndDate <= DateTime.Now && accommodationReservation.EndDate.AddDays(5) >= DateTime.Now;
        }

        public List<AccommodationReservation> GetAllNotGradedReservations(int ownerId)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in _accommodationReservationRepository.GetAll())
            {
                if (reservation.Accommodation.Owner.Id != ownerId)
                {
                    continue;
                }
                if (IsReservationAvailable(reservation) == false)
                {
                    continue;
                }
                if (!(Injector.CreateInstance<IGuestGradeService>().DoesReservationHaveGrade(reservation.Id)))
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public bool CheckNumberOfGuests(Accommodation selectedAccommodation, string numberOfGuests)
        {
            return selectedAccommodation.MaxGuestNumber >= int.Parse(numberOfGuests);
        }

        public void BookAccommodation(DateTime initialDate, DateTime endDate, Accommodation selectedAccommodation)
        {
            AccommodationReservation reservation = new AccommodationReservation();
            reservation.Accommodation.Id = selectedAccommodation.Id;
            reservation.InitialDate = initialDate;
            reservation.EndDate = endDate;
            reservation.DaysToStay = (endDate - initialDate).Days;
            reservation.Guest.Id = Injector.CreateInstance<IUserService>().GetLoggedUser().Id;
            reservation.IsCancelled = false;
            _accommodationReservationRepository.Create(reservation);

        }

        public bool PermissionToRate(AccommodationReservation accommodationReservation)
        {
            DateTime today = DateTime.Now.Date;
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);
            return accommodationReservation.EndDate < todayMidnight && todayMidnight <= accommodationReservation.EndDate.AddDays(5);
        }

        public bool PermissionToCancel(AccommodationReservation accommodationReservation)
        {
            DateTime today = DateTime.Now.Date;
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);
            if (todayMidnight.AddDays(accommodationReservation.Accommodation.CancellationPeriod) <= accommodationReservation.InitialDate)
            {
                // DeleteReservationFromCSV(accommodationReservation);
                accommodationReservation.IsCancelled = true;
                List<AccommodationReservation> _reservations = _accommodationReservationRepository.GetAll();
                SaveParam(_reservations);
                Injector.CreateInstance<INotificationService>().SendNotification(accommodationReservation);
                return true;
            }

            return false;
        }
        public bool DatesOverlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 < end2 && end1 > start2;
        }

        public void RemoveCurrentReservation(List<AccommodationReservation> allReservations, RequestAccommodationReservation request)
        {
            allReservations.Remove(allReservations.Single(res => res.Id == request.AccommodationReservation.Id));
        }

        public bool IsAvailableToMove(RequestAccommodationReservation request)
        {
            List<AccommodationReservation> allReservations = _accommodationReservationRepository.GetAll().ToList();
            RemoveCurrentReservation(allReservations, request);
            foreach (var reservation in allReservations)
            {
                if (DatesOverlaps(reservation.InitialDate, reservation.EndDate, request.NewArrivalDay, request.NewDeparuteDay))
                {
                    return false;
                }
            }
            return true;
        }
        public void DeleteReservationFromCSV(AccommodationReservation accommmodationReservation)
        {
            List<AccommodationReservation> _reservations = _accommodationReservationRepository.GetAll();
            _reservations.RemoveAll(n => n.Id == accommmodationReservation.Id);
            SaveParam(_reservations);
            NotifyObservers();
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
        public void SaveParam(List<AccommodationReservation> reservations)
        {
            _accommodationReservationRepository.SaveParam(reservations);
        }

        public void Save()
        {
            _accommodationReservationRepository.Save();
        }

        public int CountReservationsForSpecificYear(int year, int accommodationId)
        {
            int number = 0;
            foreach (AccommodationReservation res in _accommodationReservationRepository.GetAll())
            {
                if (res.Accommodation.Id == accommodationId && res.EndDate.Year == year && res.IsCancelled == false)
                {
                    number++;
                }
            }
            return number;
        }

        public int CountCancelledReservationsForSpecificYear(int year, int accommodationId)
        {
            int number = 0;
            foreach (AccommodationReservation res in _accommodationReservationRepository.GetAll())
            {
                if (res.Accommodation.Id == accommodationId && res.EndDate.Year == year && res.IsCancelled == true)
                {
                    number++;
                }
            }
            return number;
        }
        public int CountReservationsForSpecificMonth(int year, int month, int accommodationId)
        {
            int number = 0;
            foreach (AccommodationReservation res in _accommodationReservationRepository.GetAll())
            {
                if (res.Accommodation.Id == accommodationId && res.EndDate.Year == year && res.EndDate.Month == month && res.IsCancelled == false)
                {
                    number++;
                }
            }
            return number;
        }
        public double FindTheOccupancyPercentageForYear(int accommodationId, int year)
        {
            int sum = 0;
            int count = 0;
            foreach(AccommodationReservation res in _accommodationReservationRepository.GetAll())
            {
                if (res.Accommodation.Id == accommodationId && res.EndDate.Year==year)
                {
                    sum += res.DaysToStay;
                    count++;
                }
            }
            return sum/count;
        }

        public int CountReservationsForSpecificLocation(int locationId)
        {
            int number = 0;
            foreach(AccommodationReservation res in _accommodationReservationRepository.GetAll())
            {
                if(res.Accommodation.Location.Id == locationId)
                {
                    number++;
                }
            }
            return number;
        }

        //public double FindMaxOccupancyPercentage(int accId)
        //{
        //    double max = 0;
        //    for(int i = 2023; i >= 2021; i--)
        //    {
        //        if()
        //    }
        //}
        //public int FindTheMostBusyYear(int accommodationId)
        //{
        //    if()
            
        //}
        public List<AccommodationReservation> GetAllNotCancelled()
        {
            List<AccommodationReservation> filtered= new List<AccommodationReservation>();
            foreach(AccommodationReservation res in _accommodationReservationRepository.GetAll())
            {
                if (res.IsCancelled == false)
                {
                    filtered.Add(res);
                }
            }
            return filtered;
        }
        public int FindTheMostBusyYear(List<AccommodationReservation> reservations)
        {
                reservations = GetAllNotCancelled();
                var groupedByYear = reservations.GroupBy(r => r.EndDate.Year);
                int mostBusyYear = 0;
                double highestBusinessRatio = 0;

                foreach (var group in groupedByYear)
                {
                    int year = group.Key;
                    int totalDaysToStay = group.Sum(r => r.DaysToStay);
                    int daysInYear = DateTime.IsLeapYear(year) ? 366 : 365;

                    double businessRatio = (double)totalDaysToStay / daysInYear;

                    if (businessRatio > highestBusinessRatio)
                    {
                        mostBusyYear = year;
                        highestBusinessRatio = businessRatio;
                    }
                }
                return mostBusyYear;
        }
        public int GetMostBusyMonth(List<AccommodationReservation> reservations, int year)
        {
            reservations = GetAllNotCancelled();
            var reservationsInYear = reservations.Where(r => r.EndDate.Year == year);

            var groupedByMonth = reservationsInYear.GroupBy(r => r.EndDate.Month);

            int mostBusyMonth = 0;
            double highestBusinessRatio = 0;

            foreach (var group in groupedByMonth)
            {
                int month = group.Key;
                int totalDaysToStay = group.Sum(r => r.DaysToStay);
                int daysInMonth = DateTime.DaysInMonth(year, month);

                double businessRatio = (double)totalDaysToStay / daysInMonth;

                if (businessRatio > highestBusinessRatio)
                {
                    mostBusyMonth = month;
                    highestBusinessRatio = businessRatio;
                }
            }

            return mostBusyMonth;
        }

        public int CountCancelledReservationsForSpecificMonth(int year, int month, int accommodationId)
        {
            int number = 0;
            foreach (AccommodationReservation res in _accommodationReservationRepository.GetAll())
            {
                if (res.Accommodation.Id == accommodationId && res.EndDate.Year == year && res.EndDate.Month == month && res.IsCancelled == true)
                {
                    number++;
                }
            }
            return number;
        }
        public List<DateTime> FindDatesThatAreNotAvailable(Accommodation selectedAccommodation)
        {
            //List<AccommodationReservation> reservations = _accommodationReservationService.GetAll();
            List<DateTime> nonAvailableDates = new List<DateTime>();

            foreach (AccommodationReservation reservation in _accommodationReservationRepository.GetAll())
            {
                if (selectedAccommodation.Id == reservation.Accommodation.Id && reservation.IsCancelled == false)
                {
                    DateTime checkIn = reservation.InitialDate;
                    DateTime checkOut = reservation.EndDate;

                    for (DateTime currentDate = checkIn; currentDate <= checkOut; currentDate = currentDate.AddDays(1))
                    {
                        nonAvailableDates.Add(currentDate);
                    }
                }
            }
            return nonAvailableDates;
        }
        public bool IsLocationVisited(Location location)
        {

            foreach (var reservation in _accommodationReservationRepository.GetAll())
            {
                if (reservation.Accommodation.Location.Id == location.Id && reservation.Guest.Id == _userService.GetLoggedUser().Id && reservation.InitialDate <= DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
    }
}