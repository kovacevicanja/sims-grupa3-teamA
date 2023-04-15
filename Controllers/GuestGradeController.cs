using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Images;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class GuestGradeController: ISubject
    {
        private readonly List<IObserver> observers;
        private readonly GuestGradeHandler _gradeHandler;
        private List<GuestGrade> _grades;
        public AccommodationReservationController _accommodationController { get; set; }

        public GuestGradeController()
        {
            _gradeHandler = new GuestGradeHandler();
            _grades = new List<GuestGrade>();
        }
        public void Load()
        {
            _grades = _gradeHandler.Load();
            AccommodationGradeBind();
        }
        public List<GuestGrade> GetAll()
        {
            return _grades;
        }
        public GuestGrade GetByID(int id)
        {
            return _grades.Find(grade => grade.Id == id);
        }
        public void Create(GuestGrade grade)
        {
            grade.Id = GenerateId();
            _grades.Add(grade);
        }
        public void AccommodationGradeBind()
        {
            _accommodationController.Load();
            foreach (GuestGrade grade in _grades)
            {
                AccommodationReservation accommodation = _accommodationController.GetByID(grade.AccommodationReservation.Id);
                grade.AccommodationReservation = accommodation;
            }
        }
        public void SaveGrade()
        {
            _gradeHandler.Save(_grades);
        }
        public int GenerateId()
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
        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }
        public bool DoesReservationHaveGrade(int accommodationReservationId)
        {
            foreach (GuestGrade grade in _grades)
            {
                if (grade.AccommodationReservation.Id == accommodationReservationId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
