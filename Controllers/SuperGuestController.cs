using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class SuperGuestController
    {
        private readonly ISuperGuestService superGuestService;
        public SuperGuestController()
        {
            superGuestService = Injector.CreateInstance<ISuperGuestService>();
        }
        public void Create(SuperGuest guest)
        {
            superGuestService.Create(guest);
        }
        public List<SuperGuest> GetAll()
        {
            return superGuestService.GetAll();
        }
        public void Save(List<SuperGuest> guests)
        {
            superGuestService.Save(guests);
        }
        public SuperGuest GetById(int id)
        {
            return superGuestService.GetById(id);
        }
    }
}
