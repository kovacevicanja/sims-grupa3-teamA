using BookingProject.Model;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IAccommodationImageRepository
    {
        void Create(AccommodationImage image);
        List<AccommodationImage> GetAll();
        AccommodationImage GetById(int id);
        void Initialize();
        void Save(List<AccommodationImage> images);
        void SaveImage();
        void Delete(AccommodationImage image);
    }
}
