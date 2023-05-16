using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class SuperGuestService : ISuperGuestService
    {
        private ISuperGuestRepository _superGuestRepository;
        public SuperGuestService() { }
        public void Initialize()
        {
            _superGuestRepository = Injector.CreateInstance<ISuperGuestRepository>();
        }
        public void Create(SuperGuest guest)
        {
            _superGuestRepository.Create(guest);
        }
        public void Save(List<SuperGuest> guests)
        {
            _superGuestRepository.Save(guests);
        }

        public List<SuperGuest> GetAll()
        {
            return _superGuestRepository.GetAll();
        }

        public SuperGuest GetById(int id)
        {
            return _superGuestRepository.GetById(id);
        }
    }
}
