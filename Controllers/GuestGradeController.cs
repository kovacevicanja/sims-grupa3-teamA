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
    public class GuestGradeController : ISubject
    {

        private readonly List<IObserver> observers;
        private readonly GuestGradeHandler _gradeHandler;

        private List<GuestGrade> _grades;
        public AccommodationReservationController _accommodationController { get; set; }
        public UserController _userController;

        public GuestGradeController()
        {
            _gradeHandler = new GuestGradeHandler();
            _grades = new List<GuestGrade>();
            _userController = new UserController();
        }


        public void Load()
        {
            _grades = _gradeHandler.Load();
            AccommodationGradeBind();
            GradeUserBind();
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
        public void GradeUserBind()
        {

            foreach (GuestGrade grade in _grades)
            {
                User user = _userController.GetByID(grade.Guest.Id);
                grade.Guest = user;
            }
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

        public bool ExistsGuestGradeForAccommodationId(int accomomodationId)
        {
            foreach (GuestGrade grade in _grades)
            {
                if (grade.AccommodationReservation.Accommodation.Id == accomomodationId)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
