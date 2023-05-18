using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IAccommodationRenovationService
    {
        void Create(AccommodationRenovation renovation);
        List<AccommodationRenovation> GetAll();
        AccommodationRenovation GetById(int id);
        void Initialize();
        void Save(List<AccommodationRenovation> renovations);
        void SaveRenovation();
    }
}
