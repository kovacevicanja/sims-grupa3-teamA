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
    public class AccommodationReservationService : IAccommodationReservationService
    {
        private IAccommodationReservationRepository _accommodationReservationRepository;
        public AccommodationReservationService() { }
        public void Initialize()
        {
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
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
            Injector.CreateInstance<IAccommodationReservationService>().GetAll().Add(reservation);
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
                
                DeleteReservationFromCSV(accommodationReservation);
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
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationReservationRepository.Subscribe(observer);
        }
        public void SaveParam(List<AccommodationReservation> reservations)
        {
            _accommodationReservationRepository.SaveParam(reservations);
        }

        public void Save()
        {
            _accommodationReservationRepository.Save();
        }
    }
}