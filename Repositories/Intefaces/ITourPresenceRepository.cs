using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ITourPresenceRepository
    {
        void Create(TourPresence tourPresence);
        List<TourPresence> GetAll();
        TourPresence GetByID(int id);

    }
}
