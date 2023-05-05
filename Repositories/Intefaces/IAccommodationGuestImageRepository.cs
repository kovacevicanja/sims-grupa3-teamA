using BookingProject.Domain.Images;
using BookingProject.Model;
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
        AccommodationGuestImage GetById(int id);
        void Save(List<AccommodationGuestImage> images);
        void SaveImage();
    }
}
