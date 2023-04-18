using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IAccommodationDateRepository
    {
        void Initialize();
        List<AccommodationDate> GetAll();
        AccommodationDate GetByID(int id);
        void Save(List<AccommodationDate> dates);
    }
}
