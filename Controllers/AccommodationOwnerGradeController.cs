using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class AccommodationOwnerGradeController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly AccommodationOwnerGradeHandler _accommodationOwnerGradeHandler;
        private AccommodationController _accommodationController;

        private List<AccommodationOwnerGrade> _grades;
        private UserController _userController;

        public AccommodationOwnerGradeController()
        {
            _accommodationOwnerGradeHandler = new AccommodationOwnerGradeHandler();
            _accommodationController = new AccommodationController();
            observers = new List<IObserver>();
            _userController = new UserController();
            Load();
        }

        public void Load()
        {
            _grades = _accommodationOwnerGradeHandler.Load();
            GradeUserBind();
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
        public bool ExistsAccommodationGradeForAccommodationId(int accomomodationId)
        {
            foreach (AccommodationOwnerGrade grade in _grades)
            {
                if (grade.Accommodation.Id == accomomodationId)
                {
                    return true;
                }
            }
            return false;
        }

        public void GradeUserBind()
        {

            foreach (AccommodationOwnerGrade grade in _grades)
            {
                User user = _userController.GetByID(grade.User.Id);
                grade.User = user;
            }
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
        public bool IsOwnerSuperOwner(int ownerId)
        {
            int counter = 0;
            double sum = 0;
            foreach (AccommodationOwnerGrade grade in _grades)
            {
                Accommodation accommodation = _accommodationController.GetByID(grade.Accommodation.Id);
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
    }
}
