using BookingProject.Domain.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IAccommodationGuestImageService
    {
        void Initialize();
        List<AccommodationGuestImage> GetAll();
        void Create(AccommodationGuestImage image);
        AccommodationGuestImage GetByID(int id);
    }
}
