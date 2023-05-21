using BookingProject.Domain;
using BookingProject.Model;
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
        void CheckIfGuestIsSuper(User guest);
        void CheckRequirements(User guest);
        void CheckNumberOfReservationsAgain(User guest);
        void UpdateSuperGuest(User guest);
        void CheckConditions(User guest);
        void MakeSuperGuest(User guest, int numberOfReservations);
        DateTime SetStartDateForGuest(User guest);
        int FindNumberOfReservations(User guest);
        List<AccommodationReservation> FindAllReservationsForGuest(User guest);
        List<AccommodationReservation> FindReservationsForGuestForLastYear(User guest);
        void ReduceBonusPoints(User guest);
    }
}
