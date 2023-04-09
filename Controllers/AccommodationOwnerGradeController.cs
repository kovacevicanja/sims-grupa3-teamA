using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class AccommodationOwnerGradeController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly AccommodationOwnerGradeHandler _accommodationOwnerGradeHandler;

        private List<AccommodationOwnerGrade> _grades;

        public AccommodationOwnerGradeController()
        {
            _accommodationOwnerGradeHandler = new AccommodationOwnerGradeHandler();
            observers = new List<IObserver>();
            Load();
        }

        public void Load()
        {
            _grades = _accommodationOwnerGradeHandler.Load();
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

        public void Create(AccommodationOwnerGrade grade)
        { 
            grade.Id = GenerateId();
            _grades.Add(grade);
            NotifyObservers();
        }

        public void Save()
        {
            _accommodationOwnerGradeHandler.Save(_grades);
            NotifyObservers();
        }


        public List<AccommodationOwnerGrade> GetAll()
        {
            return _grades;
        }

        public AccommodationOwnerGrade GetByID(int id)
        {
            return _grades.Find(grade => grade.Id == id);
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

    }
}
