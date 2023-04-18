using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourImageService
    {
        void CleanUnused();
        void Create(TourImage image);
        void LinkToTour(int id);
        List<TourImage> GetAll();
        TourImage GetByID(int id);
        void Initialize();
        void Save();
    }
}
