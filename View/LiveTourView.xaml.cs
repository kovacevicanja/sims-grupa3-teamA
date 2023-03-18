using BookingProject.Model;
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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for LiveTourView.xaml
    /// </summary>
    /// 
    public partial class LiveTourView : Window
    {

        public Tour ChosenTour { get; set; }
        public LiveTourView(Tour chosenTour)
        {
            InitializeComponent();
            ChosenTour = chosenTour;
        }
    }
}
