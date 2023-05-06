using BookingProject.Model;
using BookingProject.View.Guest2ViewModel;
using System;
using System.Collections.Generic;
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

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for SeeMoreAboutTourView.xaml
    /// </summary>
    public partial class SeeMoreAboutTourView : Window
    {
        public Tour ChosenTour { get; set; }
        public SeeMoreAboutTourView(Tour chosenTour, int guestId, string previousWindow)
        {
            InitializeComponent();
            this.DataContext = new SeeMoreAboutTourViewModel(chosenTour, guestId, previousWindow);
        }
    }
}
