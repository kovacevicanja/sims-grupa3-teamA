using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.View.OwnerViewModel
{
    public class GuestGradesForOwnerViewModel
    {
        public ObservableCollection<AccommodationOwnerGrade> GuestGrades { get; set; }
        private AccommodationOwnerGradeController _accommodationOwnerGradeControler;
        public GuestGradesForOwnerViewModel()
        {
            _accommodationOwnerGradeControler = new AccommodationOwnerGradeController();
            GuestGrades = new ObservableCollection<AccommodationOwnerGrade>(_accommodationOwnerGradeControler.GradesGradedByBothSidesForOwner(SignInForm.LoggedInUser.Id));
        }
    }
}
