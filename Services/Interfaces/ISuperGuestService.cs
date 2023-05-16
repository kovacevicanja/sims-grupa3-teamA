using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ISuperGuestService
    {
        void Initialize();
        void Create(SuperGuest guest);
        void Save(List<SuperGuest> guests);
        List<SuperGuest> GetAll();
        SuperGuest GetById(int id);
    }
}
