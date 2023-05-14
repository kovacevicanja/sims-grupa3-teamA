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
    /// Interaction logic for RequestDetailsView.xaml
    /// </summary>
    public partial class RequestDetailsView : Window
    {
        public RequestDetailsView(TourRequest chosenRequest)
        {
            InitializeComponent();
            this.DataContext = new RequestDetailsViewModel(chosenRequest);
        }
    }
}
