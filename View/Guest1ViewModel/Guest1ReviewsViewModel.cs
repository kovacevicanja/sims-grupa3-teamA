using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.View.Guest1ViewModel
{
    public class Guest1ReviewsViewModel
    {
        public ObservableCollection<GuestGrade> Grades { get; set; }
        public GuestGradeController GuestGradeController { get; set; }

        public Guest1ReviewsViewModel()
        {
            GuestGradeController = new GuestGradeController();
            Grades = new ObservableCollection<GuestGrade>(GuestGradeController.GetSeeableGrades());
        }
    }
}
