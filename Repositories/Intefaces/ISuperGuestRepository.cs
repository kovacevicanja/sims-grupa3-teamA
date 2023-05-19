using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ISuperGuestRepository
    {
        void Create(SuperGuest guest);
        List<SuperGuest> GetAll();
        SuperGuest GetById(int id);
        void Initialize();
        void Save(List<SuperGuest> guests);
        void Delete(SuperGuest guest);
        void Update(SuperGuest guest);
    }
}
