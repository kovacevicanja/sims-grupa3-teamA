using BookingProject.Controller;
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
    public class GuestGradeRepository : IGuestGradeRepository
    {
        private const string FilePath = "../../Resources/Data/guestGrades.csv";

        private Serializer<GuestGrade> _serializer;

        public List<GuestGrade> _grades;
        public GuestGradeRepository()
        {
            _serializer = new Serializer<GuestGrade>();
            _grades = Load();
        }
        public void Initialize() {
            AccommodationGradeBind();
        }
        public List<GuestGrade> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<GuestGrade> grades)
        {
            _serializer.ToCSV(FilePath, grades);
        }
        public List<GuestGrade> GetAll()
        {
            return _grades.ToList();
        }
        public GuestGrade GetById(int id)
        {
            return _grades.Find(grade => grade.Id == id);
        }
        public void Create(GuestGrade grade)
        {
            grade.Id = GenerateId();
            _grades.Add(grade);
            Save(_grades);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (GuestGrade grade in _grades)
            {
                if (grade.Id > maxId)
                {
                    maxId = grade.Id;
                }
            }
            return maxId + 1;
        }
        public void AccommodationGradeBind()
        {
            foreach (GuestGrade grade in _grades)
            {
                AccommodationReservation accommodation = Injector.CreateInstance<IAccommodationReservationRepository>().GetById(grade.AccommodationReservation.Id);
                grade.AccommodationReservation = accommodation;
            }
        }
    }
}
