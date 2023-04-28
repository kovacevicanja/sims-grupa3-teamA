using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class GuestGradeController 
    {
        private readonly IGuestGradeService _guestGradeService;
        public GuestGradeController()
        {
            _guestGradeService = Injector.CreateInstance<IGuestGradeService>();
        }
        public void Create(GuestGrade grade)
        {
            _guestGradeService.Create(grade);
        }
        public List<GuestGrade> GetAll()
        {
            return _guestGradeService.GetAll(); 
        }
        public void Save(List<GuestGrade> grades)
        {
            _guestGradeService.Save(grades);
        }
        public GuestGrade GetById(int id)
        {
            return _guestGradeService.GetById(id);
        }
        public bool DoesReservationHaveGrade(int accommodationReservationId)
        {
            return _guestGradeService.DoesReservationHaveGrade(accommodationReservationId);
        }
        public bool ExistsGuestGradeForAccommodationId(int accomomodationId)
        {
            return _guestGradeService.ExistsGuestGradeForAccommodationId(accomomodationId);
        }
        public int CountGradesForAccommodationAndUser(int accommodationId, int userId)
        {
            return _guestGradeService.CountGradesForAccommodationAndUser(accommodationId, userId);
        }
    }
}
