using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        private GuestGradeController _guestGradeController;

        public AccommodationOwnerGradeController()
        {
            _accommodationOwnerGradeHandler = new AccommodationOwnerGradeHandler();
            _accommodationController = new AccommodationController();
            observers = new List<IObserver>();
            _userController = new UserController();
            _guestGradeController= new GuestGradeController();
            Load();
        }

        public void Load()
        {
            _grades = _accommodationOwnerGradeHandler.Load();
            GradeUserBind();
            GradeAccommodationBind();
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
        //public List<AccommodationOwnerGrade> GetGradesForAccAndUserSkipN(int accId, int userId, int skip)
        //{
        //    int skipped = 0;
        //    List<AccommodationOwnerGrade> accommodationOwnerGrades = new List<AccommodationOwnerGrade>();
        //    foreach (AccommodationOwnerGrade accommodationOwnerGrade in _grades)
        //    {
        //        if(accommodationOwnerGrade.Accommodation.Id==accId && accommodationOwnerGrade.User.Id == userId)
        //        {
        //            if (skipped < skip) 
        //            {
        //                skipped++;
        //                continue;
        //            }
        //            accommodationOwnerGrades.Add(accommodationOwnerGrade);
        //        }
        //    }
        //    return accommodationOwnerGrades;
        //}
        //private bool ExistsAlreadyAccommodationAndUser(int accId,int userId,List<AccommodationOwnerGrade> grades)
        //{
        //    foreach(AccommodationOwnerGrade grade in grades)
        //    {
        //        if(grade.Accommodation.Id==accId && grade.User.Id == userId)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        ////public List<AccommodationOwnerGrade> GradesGradedByBothSides()
        ////{
        ////    List<AccommodationOwnerGrade> accommodationOwnerGrades = new List<AccommodationOwnerGrade>();
        ////    foreach(AccommodationOwnerGrade accommodationOwnerGrade in _grades)
        ////    {
        ////        int gradedForAccommodationUser = _guestGradeController.CountGradesForAccommodationAndUser(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id);
        ////        List<AccommodationOwnerGrade> latestGrades=GetGradesForAccAndUserSkipN(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id)
        ////        bool exists = ExistsAlreadyAccommodationAndUser(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id, accommodationOwnerGrades);
        ////        accommodationOwnerGrades.AddRange(latestGrades);
        ////    }
        ////    return accommodationOwnerGrades;
        ////}
        //public List<AccommodationOwnerGrade> GradesGradedByBothSidesForOwner(int ownerId)
        //{
        //    List<AccommodationOwnerGrade> accommodationOwnerGrades = new List<AccommodationOwnerGrade>();
        //    foreach (AccommodationOwnerGrade accommodationOwnerGrade in _grades)
        //    {
        //        if (accommodationOwnerGrade.Accommodation.Owner.Id != ownerId)
        //        {
        //            continue;
        //        }
        //        int gradedForAccUser = _guestGradeController.CountGradesForAccommodationAndUser(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id);
        //        List<AccommodationOwnerGrade> latestGrades = GetGradesForAccAndUserSkipN(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id, gradedForAccUser);
        //        bool exists = ExistsAlreadyAccommodationAndUser(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id, accommodationOwnerGrades);
        //        if (exists)
        //        {
        //            continue;
        //        }
        //        accommodationOwnerGrades.AddRange(latestGrades);
        //    }

        //    return accommodationOwnerGrades;
        //}

        public List<AccommodationOwnerGrade> GetGradesForAccAndUserLastN(int accId, int userId, int n)
        {

            return _grades.Skip(Math.Max(0, _grades.Count() - n)).ToList();

        }
        private bool ExistsAlreadyAccommodationAndUser(int accId, int userId, List<AccommodationOwnerGrade> grades)
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
            foreach (AccommodationOwnerGrade accommodationOwnerGrade in _grades)
            {
                if (accommodationOwnerGrade.Accommodation.Owner.Id != ownerId)
                {
                    continue;
                }
                int gradedForAccUser = _guestGradeController.CountGradesForAccommodationAndUser(accommodationOwnerGrade.Accommodation.Id, accommodationOwnerGrade.User.Id);
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

        public void GradeUserBind()
        {

            foreach (AccommodationOwnerGrade grade in _grades)
            {
                User user = _userController.GetByID(grade.User.Id);
                grade.User = user;
            }
        }
        public void GradeAccommodationBind()
        {

            foreach (AccommodationOwnerGrade grade in _grades)
            {
                Accommodation accommodation = _accommodationController.GetByID(grade.Accommodation.Id);
                grade.Accommodation = accommodation;
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
