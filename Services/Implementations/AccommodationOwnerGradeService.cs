using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace BookingProject.Services.Implementations
{
    public class AccommodationOwnerGradeService : IAccommodationOwnerGradeService
    {
        private IAccommodationOwnerGradeRepository _accommodationOwnerGradeRepository;
        public AccommodationOwnerGradeService() { }
        public void Initialize()
        {
            _accommodationOwnerGradeRepository = Injector.CreateInstance<IAccommodationOwnerGradeRepository>();
        }
        public void Save(List<AccommodationOwnerGrade> grades)
        {
            _accommodationOwnerGradeRepository.Save(grades);
        }

        public bool ExistsAccommodationGradeForAccommodationId(int accomomodationId)
        {
            foreach (AccommodationOwnerGrade grade in _accommodationOwnerGradeRepository.GetAll())
            {
                if (grade.Accommodation.Id == accomomodationId)
                {
                    return true;
                }
            }
            return false;
        }

        public List<AccommodationOwnerGrade> GetGradesForAccAndUserLastN(int accId, int userId, int n)
        {
            List<AccommodationOwnerGrade> gradesForUser = _accommodationOwnerGradeRepository.GetAll().Where(grad => grad.Accommodation.Id == accId && grad.User.Id == userId).ToList();
            return gradesForUser.Take(Math.Max(0, n)).ToList();
        }

        public bool ExistsAlreadyAccommodationAndUser(int accId, int userId, List<AccommodationOwnerGrade> grades)
        {
            foreach (AccommodationOwnerGrade grade in grades)
            {
                if (grade.Accommodation.Id == accId && grade.User.Id == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public List<AccommodationOwnerGrade> GradesGradedByBothSidesForOwner(int ownerId)
        {
            List<AccommodationOwnerGrade> accommodationOwnerGrades = new List<AccommodationOwnerGrade>();
            foreach (AccommodationOwnerGrade accommodationOwnerGrade in _accommodationOwnerGradeRepository.GetAll())
            {
                if (accommodationOwnerGrade.Accommodation.Owner.Id != ownerId)
                {
                    continue;
                }
                int gradedForAccUser = Injector.CreateInstance<IGuestGradeService>().CountGradesForAccommodationAndUser(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id);
                List<AccommodationOwnerGrade> latestGrades = GetGradesForAccAndUserLastN(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id, gradedForAccUser);
                bool exists = ExistsAlreadyAccommodationAndUser(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id, accommodationOwnerGrades);
                if (exists)
                {
                    continue;
                }
                accommodationOwnerGrades.AddRange(latestGrades);
            }

            return accommodationOwnerGrades;
        }


        public bool IsOwnerSuperOwner(int ownerId)
        {
            int counter = 0;
            double sum = 0;
            foreach (AccommodationOwnerGrade grade in _accommodationOwnerGradeRepository.GetAll())
            {
                Accommodation accommodation = Injector.CreateInstance<IAccommodationRepository>().GetById(grade.Accommodation.Id);
                if (accommodation.Owner.Id == ownerId)
                // if (grade.User.Id == ownerId)
                {
                    counter++;
                    sum += (double)(grade.Cleanliness + grade.OwnerCorectness) / 2;
                }
            }
            double average = sum / counter;
            return counter > 5 && average > 4.5;
        }

        public void MakeGrade(AccommodationOwnerGrade grade, AccommodationReservation _selectedReservation, int chosenCleanliness, int chosenCorectness, String Comment, String Reccommendation)
        {
            grade.Accommodation.Id = _selectedReservation.Accommodation.Id;
            grade.Cleanliness = chosenCleanliness;
            grade.OwnerCorectness = chosenCorectness;
            grade.AdditionalComment = Comment;
            grade.Reccommendation = Reccommendation;
            grade.User.Id = Injector.CreateInstance<IUserService>().GetLoggedUser().Id;
            Create(grade);
        }

        public void Create(AccommodationOwnerGrade grade)
        {
            _accommodationOwnerGradeRepository.Create(grade);
        }

        public List<AccommodationOwnerGrade> GetAll()
        {
            return _accommodationOwnerGradeRepository.GetAll();
        }

        public AccommodationOwnerGrade GetById(int id)
        {
            return _accommodationOwnerGradeRepository.GetById(id);
        }
    }
}