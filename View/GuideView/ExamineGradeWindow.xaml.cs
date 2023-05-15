using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.GuideViewModel;
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

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for ExamineGradeWindow.xaml
    /// </summary>
    public partial class ExamineGradeWindow : Window
    {
        public ExamineGradeWindow(TourEvaluation chosenEvaluation, TourTimeInstance chosenTour)
        {
            InitializeComponent();
            this.DataContext = new ExamineGradeViewModel(chosenEvaluation, chosenTour);
        }
    }
}
