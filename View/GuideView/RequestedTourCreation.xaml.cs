using BookingProject.Domain;
using BookingProject.View.GuideViewModel;
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
    /// Interaction logic for RequestedTourCreation.xaml
    /// </summary>
    public partial class RequestedTourCreation : Window
    {
        public RequestedTourCreation(TourRequest tourRequest)
        {
            InitializeComponent();
            this.DataContext = new RequestedTourCreationViewModel(tourRequest);

        }

    }
}
