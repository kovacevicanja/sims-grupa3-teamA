using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IAccommodationDateService
    {
        void Initialize();
        List<AccommodationDate> GetAll();
        AccommodationDate GetByID(int id);
    }
}
