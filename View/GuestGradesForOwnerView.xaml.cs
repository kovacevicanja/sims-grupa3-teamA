using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for GuestGradesForOwnerView.xaml
    /// </summary>
    public partial class GuestGradesForOwnerView : Window
    {
        public ObservableCollection<AccommodationOwnerGrade> GuestGrades { get; set; }
        private AccommodationOwnerGradeController _accommodationOwnerGradeControler;
        public GuestGradesForOwnerView()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationOwnerGradeControler = new AccommodationOwnerGradeController();
            GuestGrades = new ObservableCollection<AccommodationOwnerGrade>(_accommodationOwnerGradeControler.GradesGradedByBothSidesForOwner(SignInForm.LoggedInUser.Id));
        }
    }
}
