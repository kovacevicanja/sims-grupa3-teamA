using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        private const string FilePath = "../../Resources/Data/accommodationReservations.csv";
        private Serializer<AccommodationReservation> _serializer;
        public List<AccommodationReservation> _accommodationReservations;
        //private  List<IObserver> observers;
        public AccommodationReservationRepository() 
        {
            _serializer = new Serializer<AccommodationReservation>();
            //observers = new List<IObserver>();
            _accommodationReservations = Load();
        }
        public void Initialize()
        {
            AccommodationReservationBind();
            ReservationUserBind();
        }
        public List<AccommodationReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save()
        {
            _serializer.ToCSV(FilePath, _accommodationReservations);
        }
 
        public void AccommodationReservationBind()
        {
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                Accommodation accommodation = Injector.CreateInstance<IAccommodationRepository>().GetById(reservation.Accommodation.Id);
                reservation.Accommodation = accommodation;
            }
        }

        public void ReservationUserBind()
        {
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                User user = Injector.CreateInstance<IUserRepository>().GetById(reservation.Guest.Id);
                reservation.Guest = user;
            }
        }
        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservations.ToList();
        }
        public void Create(AccommodationReservation reservation)
        {
            reservation.Id = GenerateId();
            _accommodationReservations.Add(reservation);
            SaveParam(_accommodationReservations);
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach (AccommodationReservation accommodationReservation in _accommodationReservations)
            {
                if (accommodationReservation.Id > maxId)
                {
                    maxId = accommodationReservation.Id;
                }
            }
            return maxId + 1;
        }
        public AccommodationReservation GetById(int id)
        {
            return _accommodationReservations.Find(ar => ar.Id == id);
        }
       /* public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(var observer in observers)
            {
                observer.Update();
            }
        }*/

        public void SaveParam(List<AccommodationReservation> reservations)
        {
            _serializer.ToCSV(FilePath, reservations);
        }
    }
}