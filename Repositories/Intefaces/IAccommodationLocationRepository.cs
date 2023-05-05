using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IAccommodationLocationRepository
    {
        void Create(Location location);
        List<Location> GetAll();
        Location GetById(int id);
        void Initialize();
        void Save(List<Location> locations);
        void SaveLocation();
    }
}
