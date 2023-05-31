using BookingProject.Model;
using BookingProject.View.OwnerViewModel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Paragraph = iTextSharp.text.Paragraph;

namespace BookingProject.View.OwnerView
{
    /// <summary>
    /// Interaction logic for AccommodationStatisticsByYearView.xaml
    /// </summary>
    public partial class AccommodationStatisticsByYearView : Page
    {
        public AccommodationStatisticsByYearView(Accommodation selectedAccommodation, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new AccommodationStatisticsByYearViewModel(selectedAccommodation, navigationService);
        }

        
    }
}
