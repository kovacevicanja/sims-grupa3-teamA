using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
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

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for TourCancellationWindow.xaml
    /// </summary>
    public partial class TourCancellationWindow : Window
    {
        private TourTimeInstanceController _tourTimeInstanceController;
        public TourTimeInstance ChosenTour;
        public TourCancellationWindow(TourTimeInstance chosenTour)
        {
            InitializeComponent();
            _tourTimeInstanceController = new TourTimeInstanceController();
            ChosenTour = chosenTour;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void No_Click(object sender, RoutedEventArgs e)
        {
            MyToursWindow myToursWindow = new MyToursWindow();
            myToursWindow.Show();
            Close();

        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            _tourTimeInstanceController.GetByID(ChosenTour.Id).State = TourState.CANCELLED;
            _tourTimeInstanceController.Save();
            MyToursWindow myToursWindow = new MyToursWindow();
            myToursWindow.Show();
            Close();
        }

    }
}
