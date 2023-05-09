using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ITourRepository
    {
        void Create(Tour tour);
        List<Tour> GetAll();
        Tour GetById(int id);
        void BindLastTour();
        void Initialize();

        void TourLocationBind();
        void BindTourImage();
        void TourKeyPointBind();
        void TourDateBind();
    }
}