using BookingProject.Domain.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IAccommodationGuestImageRepository
    {
        void Initialize();
        List<AccommodationGuestImage> GetAll();
        int GenerateId();
        void Create(AccommodationGuestImage image);
        AccommodationGuestImage GetByID(int id);
    }
}
