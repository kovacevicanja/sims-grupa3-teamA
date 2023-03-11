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
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestView.xaml
    /// </summary>
    public partial class SecondGuestView : Window
    {
        private TourController _tourController;
        private ObservableCollection<Tour> _tours;

        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string ChoosenLanguage { get; set; } = string.Empty;
        public string NumOfGuests { get; set; } = string.Empty; 

        public SecondGuestView()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourController = new TourController();
            _tours = new ObservableCollection<Tour>(_tourController.GetAll());
            TourDataGrid.ItemsSource = _tours;

            languageComboBox.ItemsSource = new List<string>() { "ENGLISH", "SERBIAN", "GERMAN" };
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            _tourController.Search(_tours, City, Country, Duration, ChoosenLanguage, NumOfGuests);

        }

        private void Button_Click_Cancel_Search(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {

        }

    }
}
