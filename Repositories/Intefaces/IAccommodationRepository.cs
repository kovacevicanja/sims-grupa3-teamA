using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IAccommodationRepository
    {
        void Initialize();
        void Create(Accommodation accommodation);
        List<Accommodation> GetAllForOwner(int ownerId);
        List<Accommodation> GetAll();
        Accommodation GetByID(int id);
        void Save(List<Accommodation> accommodations);
        void SaveAccommodation();
    }
}
