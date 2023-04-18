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
        public void Save(List<AccommodationReservation> reservations)
        {
            _accommodationReservationRepository.Save(reservations);
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservationRepository.GetAll();
        }

        public AccommodationReservation GetByID(int id)
        {
            return _accommodationReservationRepository.GetByID(id);
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
            AccommodationReservation oldReservation = GetByID(reservation.Id);
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
            if (selectedAccommodation.MaxGuestNumber >= int.Parse(numberOfGuests))
            {
                return true;
            }
            return false;
        }

        public bool CheckEnteredDates(DateTime initialDate, DateTime endDate)
        {
            if (initialDate.Month > endDate.Month || endDate.Year < initialDate.Year || !CheckDays(initialDate, endDate) || !CompareWithToday(initialDate))
            {
                return false;
            }
            return true;
        }

        public bool CompareWithToday(DateTime initialDate)
        {
            DateTime today = DateTime.Now.Date;
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);
            if (initialDate < todayMidnight)
            {
                return false;
            }
            return true;
        }

        public bool CheckDays(DateTime initialDate, DateTime endDate)
        {
            if (initialDate.Month == endDate.Month)
            {
                if (initialDate.Day > endDate.Day)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckAvailableDate(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay, string numberOfGuests)
        {
            List<DateTime> tryDates = MakeListOfReservedDates(initialDate, endDate);
            List<AccommodationReservation> reservations = GetReservationsForAccommodation(selectedAccommodation);
            return !IsDateReserved(tryDates, reservations);
        }

        public List<AccommodationReservation> GetReservationsForAccommodation(Accommodation accommodation)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation reservation in _accommodationReservationRepository.GetAll())
            {
                if (reservation.Accommodation.Id == accommodation.Id)
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public List<DateTime> MakeListOfReservedDates(DateTime initialDate, DateTime endDate)
        {
            List<DateTime> reservedDates = new List<DateTime>();
            for (DateTime date = initialDate; date <= endDate; date = date.AddDays(1))
            {
                reservedDates.Add(date);
            }
            return reservedDates;
        }

        /*
        private bool isDateReserved(List<DateTime> tryDates, List<AccommodationReservation> reservations)
        {
            foreach (AccommodationReservation reservation in reservations)
            {
                List<DateTime> reservedDates = makeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                if(ifDatesAreInTakenList(tryDates, reservedDates))
                {
                    return true;
                }
            }
            return false;
        }
        */

        public bool IsDateReserved(List<DateTime> tryDates, List<AccommodationReservation> reservations)
        {
            foreach (AccommodationReservation reservation in reservations)
            {
                List<DateTime> reservedDates = MakeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                if (IfDatesAreInTakenList(tryDates, reservedDates))
                {
                    return true;
                }
            }
            return false;
        }

        public List<(DateTime, DateTime)> FindAvailableDates(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay)
        {
            List<(DateTime, DateTime)> availableDates = new List<(DateTime, DateTime)>();
            List<DateTime> takenDates = FindTakenDates(selectedAccommodation);

            takenDates.Sort();

            for (DateTime date = initialDate; date <= takenDates[takenDates.Count - 1]; date = date.AddDays(1))
            {
                List<DateTime> datesInRange = MakeListOfReservedDates(date, date.AddDays(numberOfDaysToStay));
                if (!IfDatesAreInTakenList(datesInRange, takenDates))
                {
                    availableDates.Add((datesInRange[0], datesInRange[datesInRange.Count - 1]));
                    break;
                }
            }

            if (availableDates.Count() == 0)
            {
                availableDates.Add((takenDates[takenDates.Count - 1].AddDays(1), takenDates[takenDates.Count - 1].AddDays(numberOfDaysToStay + 1)));
            }

            DateTime today = DateTime.Now.Date;
            DateTime todayMidnight = today.AddHours(0).AddMinutes(0).AddSeconds(0);


            for (DateTime date = initialDate; (date.AddDays(-numberOfDaysToStay)) > todayMidnight; date = date.AddDays(-1))
            {
                List<DateTime> datesInRange = MakeListOfReservedDates(date.AddDays(-numberOfDaysToStay), date);
                if (!IfDatesAreInTakenList(datesInRange, takenDates))
                {
                    availableDates.Add((datesInRange[0], datesInRange[datesInRange.Count - 1]));
                    break;
                }
            }


            return availableDates;
        }

        public bool IfDatesAreInTakenList(List<DateTime> datesToCheck, List<DateTime> takenDates)
        {
            foreach (DateTime date in datesToCheck)
            {
                foreach (DateTime takenDate in takenDates)
                {
                    if (date == takenDate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<DateTime> FindTakenDates(Accommodation selectedAccommodation)
        {
            List<DateTime> takenDates = new List<DateTime>();
            foreach (AccommodationReservation reservation in _accommodationReservationRepository.GetAll())
            {
                if (reservation.Accommodation.Id == selectedAccommodation.Id)
                {
                    List<DateTime> reservedDates = MakeListOfReservedDates(reservation.InitialDate, reservation.EndDate);
                    takenDates.AddRange(reservedDates);
                }
            }

            return takenDates;
        }

        public void BookAccommodation(DateTime initialDate, DateTime endDate, Accommodation selectedAccommodation)
        {
            AccommodationReservation reservation = new AccommodationReservation();
            //reservation.Id = Injector.CreateInstance<IAccommodationReservationRepository>().GenerateId();
            reservation.Accommodation.Id = selectedAccommodation.Id;
            reservation.InitialDate = initialDate;
            reservation.EndDate = endDate;
            reservation.DaysToStay = (endDate - initialDate).Days;
            reservation.Guest.Id = Injector.CreateInstance<IUserService>().GetLoggedUser().Id;
            Injector.CreateInstance<IAccommodationReservationService>().GetAll().Add(reservation);
            //Save();
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
                SendNotification(accommodationReservation);
                //NotifyObservers();
                return true;
            }

            return false;
        }
        //public void NotifyObservers()
        //{
        //    foreach (var observer in observers)
        //    {
        //        observer.Update();
        //    }
        //}
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
            List<AccommodationReservation> _reservations = GetAll();
            _reservations.RemoveAll(n => n.Id == accommmodationReservation.Id);
            //Save();
        }

        public void SendNotification(AccommodationReservation accommodationReservation)
        {
            Notification notification = new Notification();
            //notification.Id = Injector.CreateInstance<INotificationService>().GenerateId();
            notification.UserId = accommodationReservation.Accommodation.Owner.Id;
            notification.Text = "Reservation for your accommodation " + accommodationReservation.Accommodation.AccommodationName + ", from " + DateConversion.DateToStringAccommodation(accommodationReservation.InitialDate) + ", to " + DateConversion.DateToStringAccommodation(accommodationReservation.EndDate) + " was cancelled!";
            notification.Read = false;
            Injector.CreateInstance<INotificationService>().Create(notification);
            //_notificationService.Save();
        }

        public List<Notification> GetOwnerNotifications(User owner)
        {
            List<Notification> notificationsForOwner = new List<Notification>();
            List<Notification> _notifications = Injector.CreateInstance<INotificationService>().GetAll();

            foreach (Notification notification in _notifications)
            {
                if (notification.UserId == owner.Id && notification.Read == false)
                {
                    notificationsForOwner.Add(notification);
                }
            }
            return notificationsForOwner;
        }

        public void DeleteNotificationFromCSV(Notification notification)
        {
            List<Notification> _notifications = Injector.CreateInstance<INotificationService>().GetAll();
            _notifications.RemoveAll(n => n.Id == notification.Id);
            //_notificationService.Save();
        }

        public void WriteNotificationAgain(Notification n)
        {
            Notification notification = new Notification();
            notification.UserId = n.UserId;
            notification.Text = n.Text;
            notification.Read = true;
            Injector.CreateInstance<INotificationService>().Create(notification);
            //_notificationService.Save();
        }

        public void Subscribe(IObserver observer)
        {
            _accommodationReservationRepository.Subscribe(observer);
        }
    }
}
