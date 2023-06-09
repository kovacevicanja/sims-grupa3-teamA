using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.OwnerViewModel
{
    public class GuestGradesForOwnerViewModel
    {
        public ObservableCollection<AccommodationOwnerGrade> GuestGrades { get; set; }
        private AccommodationOwnerGradeController _accommodationOwnerGradeControler;
        public NavigationService NavigationService { get; set; }
        public AccommodationOwnerGrade SelectedGrade { get; set; }
        public GuestGradesForOwnerViewModel(NavigationService navigationService)
        {
            _accommodationOwnerGradeControler = new AccommodationOwnerGradeController();
            GuestGrades = new ObservableCollection<AccommodationOwnerGrade>(_accommodationOwnerGradeControler.GradesGradedByBothSidesForOwner(SignInForm.LoggedInUser.Id));
            NavigationService = navigationService;
        }
    }
}
