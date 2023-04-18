using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ITourLocationRepository
    {
        void Create(Location location);
        List<Location> GetAll();
        Location GetByID(int id);
        void Initialize();
        void Save();
    }
}
