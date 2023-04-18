using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IAccommodationReservationRepository
    {
        void Create(AccommodationReservation reservation);
        List<AccommodationReservation> GetAll();
        AccommodationReservation GetByID(int id);
        void Initialize();
        void Subscribe(IObserver observer);
        void SaveParam(List<AccommodationReservation> reservations);
        void Save();
    }
}