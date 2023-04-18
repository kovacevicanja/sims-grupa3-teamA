using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IGuestGradeService
    {
        void Initialize();
        void Create(GuestGrade guestGrade);
        List<GuestGrade> GetAll();
        GuestGrade GetByID(int id);
        bool DoesReservationHaveGrade(int accommodationReservationId);
        bool ExistsGuestGradeForAccommodationId(int accomomodationId);
        int CountGradesForAccommodationAndUser(int accommodationId, int userId);
    }
}
