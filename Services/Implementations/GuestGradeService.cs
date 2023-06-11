using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class GuestGradeService : IGuestGradeService
    {
        private IGuestGradeRepository _guestGradeRepository;
        private IUserService _userService;
        private IAccommodationOwnerGradeRepository _accOwnerGradeRepository;
        public GuestGradeService() { }
        public void Initialize()
        {
            _guestGradeRepository = Injector.CreateInstance<IGuestGradeRepository>();
            _userService = Injector.CreateInstance<IUserService>();
            _accOwnerGradeRepository = Injector.CreateInstance<IAccommodationOwnerGradeRepository>();
        }
        public void Create(GuestGrade grade)
        {
            _guestGradeRepository.Create(grade);
        }
        public void Save(List<GuestGrade> grades)
        {
            _guestGradeRepository.Save(grades);
        }

        public List<GuestGrade> GetAll()
        {
            return _guestGradeRepository.GetAll();
        }

        public GuestGrade GetById(int id)
        {
            return _guestGradeRepository.GetById(id);
        }
        public bool DoesReservationHaveGrade(int accommodationReservationId)
        {
            foreach (GuestGrade grade in _guestGradeRepository.GetAll())
            {
                if (grade.AccommodationReservation.Id == accommodationReservationId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ExistsGuestGradeForAccommodationId(int accomomodationId)
        {
            foreach (GuestGrade grade in _guestGradeRepository.GetAll())
            {
                if (grade.AccommodationReservation.Accommodation.Id == accomomodationId)
                {
                    return true;
                }
            }
            return false;
        }
        public int CountGradesForAccommodationAndUser(int accommodationId, int userId)
        {
            int count = 0;
            foreach (GuestGrade grade in _guestGradeRepository.GetAll())
            {
                if (grade.AccommodationReservation.Accommodation.Id == accommodationId && grade.AccommodationReservation.Guest.Id == userId)
                {
                    count++;
                }
            }
            return count;
        }
        public List<GuestGrade> GetSeeableGrades()
        {
            List<GuestGrade> _seeableGrades = new List<GuestGrade>();
            foreach (var g in _guestGradeRepository.GetAll())
            {
                if (g.AccommodationReservation.Guest.Id == _userService.GetLoggedUser().Id)
                {
                    foreach (var g1 in _accOwnerGradeRepository.GetAll())
                    {
                        if (g.AccommodationReservation.Id == g1.AccommodationReservation.Id)
                        {
                            _seeableGrades.Add(g);
                        }
                    }
                }
            }
            return _seeableGrades.Distinct().ToList();
        }
    }
}
