using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using BookingProject.View;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using BookingProject.DependencyInjection;

namespace BookingProject.Repositories.Implementations
{
    public class TourReservationRepository : ITourReservationRepository
    {
        private const string FilePath = "../../Resources/Data/tourReservations.csv";

        private  Serializer<TourReservation> _serializer;

        public List<TourReservation> _reservations;

        public TourReservationRepository()
        {
        }

        public void Initialize()
        {
            _serializer = new Serializer<TourReservation>();
            _reservations = Load();
            TourReservationBind();
        }

        public List<TourReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourReservation> dates)
        {
            _serializer.ToCSV(FilePath, dates);
        }

        public List<TourReservation> GetAll()
        {
            return _reservations.ToList();
        }

        public TourReservation GetByID(int id)
        {
            return _reservations.Find(date => date.Id == id);
        }
       
        public int GenerateId()
        {
            if (_reservations.Count == 0) return 0;
            return _reservations.Max(s => s.Id) + 1;

        }
        public void TourReservationBind()
        {
            foreach (TourReservation reservation in _reservations)
            {
                Tour tour = Injector.CreateInstance<ITourRepository>().GetByID(reservation.Tour.Id);
                reservation.Tour = tour;
            }
        }
        public void ReservationGuestBind(int id)
        {
            foreach (TourReservation reservation in GetAll())
            {
                if (reservation.Guest.Id == -1)
                {
                    reservation.Guest.Id = id;
                }
            }
        }
        public List<TourReservation> GetUserReservations(int guestId)
        {
            List<TourReservation> guestsTours = new List<TourReservation>();
            foreach (TourReservation tr in GetAll())
            {
                if (tr.Guest.Id == guestId)
                {
                    guestsTours.Add(tr);
                }
            }
            return guestsTours;
        }
        public void SaveReservationToFile(Tour choosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            TourReservation reservation = new TourReservation(GenerateId(), choosenTour, choosenTour.MaxGuests - int.Parse(numberOfGuests), selectedDate, guest);

            guest.MyTours = GetUserReservations(guest.Id);

            ReservationGuestBind(guest.Id);
            guest.MyTours.Add(reservation);
            _reservations.Add(reservation);
            Save(_reservations);
        }
        public void SaveSameReservationToFile(Tour chosenTour, TourReservation tourReservation, string numberOfGuests, DateTime selectedDate, User guest)
        {
            foreach (TourReservation tr in GetAll())
            {
                if (tr.Id == tourReservation.Id)
                {
                    tr.GuestsNumberPerReservation -= int.Parse(numberOfGuests);
                    Save(_reservations);
                }
            }
            List<TourReservation> reservationsCopy = new List<TourReservation>(_reservations);
            List<TourReservation> reservationsToRemove = new List<TourReservation>();

            foreach (TourReservation tr in reservationsCopy)
            {
                if (tr.Guest.Id == guest.Id && tr.Id == tourReservation.Id && selectedDate == tr.ReservationStartingTime)
                {
                    reservationsToRemove.Add(tr);
                }
            }

            foreach (TourReservation tr in reservationsToRemove)
            {
                reservationsCopy.Remove(tr);
            }

            TourReservation newReservation = new TourReservation(tourReservation.Id, chosenTour, tourReservation.GuestsNumberPerReservation, selectedDate, guest);
            reservationsCopy.Add(newReservation);
            Save(reservationsCopy);
        }
    }
}