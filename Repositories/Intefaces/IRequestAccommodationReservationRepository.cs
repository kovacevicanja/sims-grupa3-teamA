using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IRequestAccommodationReservationRepository
    {
        void Initialize();
        List<RequestAccommodationReservation> GetAll();
        void Create(RequestAccommodationReservation request);
        RequestAccommodationReservation GetByID(int id);
        void Save(List<RequestAccommodationReservation> requests);
        void SaveRequest();
    }
}
