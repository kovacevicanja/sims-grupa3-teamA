using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Images;
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
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        private AccommodationController _accommodationController;
        private AccommodationOwnerGradeController _accommodationOwnerGradeController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public OwnerView()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            _accommodationOwnerGradeController = new AccommodationOwnerGradeController();
            if (!_accommodationOwnerGradeController.IsOwnerSuperOwner(SignInForm.LoggedInUser.Id))
            {
                SuperOwnerImage.Visibility = Visibility.Hidden;
            }
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
        }
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AddAccommodationView addAccommodationsView = new AddAccommodationView();
            addAccommodationsView.Show();
        }
        private void Button_Click_Rate(object sender, RoutedEventArgs e)
        {
            NotGradedView view = new NotGradedView();
            view.Show();
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
