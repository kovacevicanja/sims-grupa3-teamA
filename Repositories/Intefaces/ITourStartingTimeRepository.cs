using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ITourStartingTimeRepository
    {
        void Create(TourDateTime date);
        List<TourDateTime> GetAll();
        TourDateTime GetByID(int id);
    }
}
