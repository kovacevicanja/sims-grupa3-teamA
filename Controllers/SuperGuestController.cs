using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
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
        public void CheckIfGuestIsSuper(User guest)
        {
            superGuestService.CheckIfGuestIsSuper(guest);
        }
        public void CheckRequirements(User guest)
        {
            superGuestService.CheckRequirements(guest);
        }
        public void CheckNumberOfReservationsAgain(User guest)
        {
            superGuestService.CheckNumberOfReservationsAgain(guest);
        }
        public void UpdateSuperGuest(User guest)
        {
            superGuestService.UpdateSuperGuest(guest);
        }
        public void CheckConditions(User guest)
        {
            superGuestService.CheckConditions(guest);
        }
        public void MakeSuperGuest(User guest, int numberOfReservations)
        {
            superGuestService.MakeSuperGuest(guest, numberOfReservations);
        }
        public DateTime SetStartDateForGuest(User guest)
        {
            return superGuestService.SetStartDateForGuest(guest);
        }
        public int FindNumberOfReservations(User guest)
        {
            return superGuestService.FindNumberOfReservations(guest);
        }
        public List<AccommodationReservation> FindAllReservationsForGuest(User guest)
        {
            return superGuestService.FindAllReservationsForGuest(guest);
        }
        public List<AccommodationReservation> FindReservationsForGuestForLastYear(User guest)
        {
            return superGuestService.FindReservationsForGuestForLastYear(guest);
        }
        public void ReduceBonusPoints(User guest)
        {
            superGuestService.ReduceBonusPoints(guest);
        }
    }
}
