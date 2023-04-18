using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourStartingTimeService
    {
        void LinkToTour(int id);
        void CleanUnused();
        void Create(TourDateTime date);
        List<TourDateTime> GetAll();
        TourDateTime GetByID(int id);
        void Initialize();
        void Save();
    }
}
