using BookingProject.DependencyInjection;
using BookingProject.Domain.Images;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class AccommodationOwnerGradeRepository : IAccommodationOwnerGradeRepository
    {
        private const string FilePath = "../../Resources/Data/accommodationOwnerGrades.csv";

        private Serializer<AccommodationOwnerGrade> _serializer;

        public List<Accommodation> _accommodations;

        public List<AccommodationOwnerGrade> _grades;

        public AccommodationOwnerGradeRepository()
        {
            _serializer = new Serializer<AccommodationOwnerGrade>();
            _grades = Load();
        }
        public void Initialize()
        {
            GradeUserBind();
            GradeAccommodationBind();
            GradeReservationBind();
            AccommodationImagesBind();
        }

        public void AccommodationImagesBind()
        {
            IAccommodationGuestImageRepository accommodationImageRepository = Injector.CreateInstance<IAccommodationGuestImageRepository>();
            foreach (AccommodationGuestImage image in accommodationImageRepository.GetAll())
            {
                AccommodationOwnerGrade grade = GetById(image.Grade.Id);
                grade.guestImages.Add(image);
            }
        }

        public List<AccommodationOwnerGrade> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationOwnerGrade> grades)
        {
            _serializer.ToCSV(FilePath, grades);
        }

        public int GenerateId()
        {
            int maxId = 0;
            foreach (AccommodationOwnerGrade grade in _grades)
            {
                if (grade.Id > maxId)
                {
                    maxId = grade.Id;
                }
            }
            return maxId + 1;
        }

        public void GradeUserBind()
        {

            foreach (AccommodationOwnerGrade grade in _grades)
            {
                User user = Injector.CreateInstance<IUserRepository>().GetById(grade.User.Id);
                grade.User = user;
            }
        }
        public void GradeAccommodationBind()
        {

            foreach (AccommodationOwnerGrade grade in _grades)
            {
                Accommodation accommodation = Injector.CreateInstance<IAccommodationRepository>().GetById(grade.Accommodation.Id);
                grade.Accommodation = accommodation;
            }
        }
        public void GradeReservationBind()
        {

            foreach (AccommodationOwnerGrade grade in _grades)
            {
                AccommodationReservation accommodation = Injector.CreateInstance<IAccommodationReservationRepository>().GetById(grade.AccommodationReservation.Id);
                grade.AccommodationReservation = accommodation;
            }
        }

        public void Create(AccommodationOwnerGrade grade)
        {
            grade.Id = GenerateId();
            _grades.Add(grade);
            Save(_grades);
        }

        public List<AccommodationOwnerGrade> GetAll()
        {
            return _grades;
        }

        public AccommodationOwnerGrade GetById(int id)
        {
            return _grades.Find(grade => grade.Id == id);
        }
    }
}
