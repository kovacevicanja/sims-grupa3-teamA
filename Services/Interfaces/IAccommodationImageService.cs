using BookingProject.Model;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IAccommodationImageService
    {
        void Initialize();
        void Create(AccommodationImage image);
        List<AccommodationImage> GetAll();
        AccommodationImage GetByID(int id);
        void LinkToAccommodation(int id);
        void DeleteUnused();
        void Save(List<AccommodationImage> images);
        void SaveImage();
    }
}
