using BookingProject.Domain;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IAccommodationRenovationRepository
    {
        void Create(AccommodationRenovation renovation);
        List<AccommodationRenovation> GetAll();
        AccommodationRenovation GetById(int id);
        void Initialize();
        void Save(List<AccommodationRenovation> renovations);
        AccommodationRenovation Save(AccommodationRenovation accommodationRenovation);
        AccommodationRenovation Update(AccommodationRenovation accommodationRenovation);
        void Delete(AccommodationRenovation renovation);
    }
}
