using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.Controller
{
    public class AccommodationOwnerGradeController 
    {
        private readonly IAccommodationOwnerGradeService _accommodationOwnerGradeService;
        public AccommodationOwnerGradeController()
        {
            _accommodationOwnerGradeService = Injector.CreateInstance<IAccommodationOwnerGradeService>();
        }
        public void Save(List<AccommodationOwnerGrade> grades)
        {
            _accommodationOwnerGradeService.Save(grades);
        }
        public bool ExistsAccommodationGradeForAccommodationId(int accomomodationId)
        {
            return _accommodationOwnerGradeService.ExistsAccommodationGradeForAccommodationId(accomomodationId);
        }
        public List<AccommodationOwnerGrade> GetGradesForAccAndUserLastN(int accId, int userId, int n)
        {
            return _accommodationOwnerGradeService.GetGradesForAccAndUserLastN(accId, userId, n);
        }
        public bool ExistsAlreadyAccommodationAndUser(int accId, int userId, List<AccommodationOwnerGrade> grades)
        {
            return _accommodationOwnerGradeService.ExistsAlreadyAccommodationAndUser(accId, userId, grades);
        }
        public List<AccommodationOwnerGrade> GradesGradedByBothSidesForOwner(int ownerId)
        {
            return _accommodationOwnerGradeService.GradesGradedByBothSidesForOwner(ownerId);
        }
        public bool IsOwnerSuperOwner(int ownerId)
        {
            return _accommodationOwnerGradeService.IsOwnerSuperOwner(ownerId);
        }
        public void Create(AccommodationOwnerGrade grade)
        {
            _accommodationOwnerGradeService.Create(grade);
        }
        public List<AccommodationOwnerGrade> GetAll()
        {
            return _accommodationOwnerGradeService.GetAll();
        }
        public AccommodationOwnerGrade GetById(int id)
        {
            return _accommodationOwnerGradeService.GetById(id);
        }

        public void MakeGrade(AccommodationOwnerGrade grade, AccommodationReservation _selectedReservation, int chosenCleanliness, int chosenCorectness, String Comment)
        {
           _accommodationOwnerGradeService.MakeGrade(grade, _selectedReservation, chosenCleanliness, chosenCorectness, Comment);
        }
    }
}
