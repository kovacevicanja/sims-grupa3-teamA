using BookingProject.DependencyInjection;
using BookingProject.Model;
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

        public AccommodationOwnerGradeRepository() { }
        public void Initialize()
        {
            _serializer = new Serializer<AccommodationOwnerGrade>();
            _grades = Load();
            GradeUserBind();
            GradeAccommodationBind();
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
                User user = Injector.CreateInstance<IUserRepository>().GetByID(grade.User.Id);
                grade.User = user;
            }
        }
        public void GradeAccommodationBind()
        {

            foreach (AccommodationOwnerGrade grade in _grades)
            {
                Accommodation accommodation = Injector.CreateInstance<IAccommodationRepository>().GetByID(grade.Accommodation.Id);
                grade.Accommodation = accommodation;
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

        public AccommodationOwnerGrade GetByID(int id)
        {
            return _grades.Find(grade => grade.Id == id);
        }
    }
}
