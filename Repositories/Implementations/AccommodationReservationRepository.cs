using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
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
        private readonly Serializer<AccommodationReservation> _serializer;
        public List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationReservations = Load();
            AccommodationReservationBind();
            ReservationUserBind();
        }

        public List<AccommodationReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationReservation> accommodationReservations)
        {
            _serializer.ToCSV(FilePath, accommodationReservations);
        }
        public void AccommodationReservationBind()
        {
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                //Accommodation accommodation = Injector.CreateInstance<IAccommodationRepository>().GetBuId(reservation.Accommodation.Id);
                //reservation.Accommodation = accommodation;
            }
        }

        public void ReservationUserBind()
        {

            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                User user = Injector.CreateInstance<IUserRepository>().GetByID(reservation.Guest.Id);
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
            Save(_accommodationReservations);
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
        public AccommodationReservation GetByID(int id)
        {
            return _accommodationReservations.Find(ar => ar.Id == id);
        }
        
    }
}
