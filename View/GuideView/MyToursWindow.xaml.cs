using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for MyToursWindow.xaml
    /// </summary>
    public partial class MyToursWindow : Window
    {

        private TourTimeInstanceController _tourTimeInstanceController;
        private TourStartingTimeController _tourStartingTimeController;
        private ObservableCollection<TourTimeInstance> _instances;
        public TourTimeInstance ChosenTour { get; set; }
        public MyToursWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourStartingTimeController = new TourStartingTimeController();

            _instances = new ObservableCollection<TourTimeInstance>(_tourTimeInstanceController.GetAll());
            TourDataGrid.ItemsSource = _instances;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            Close();

        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            TourCreationWindow tourCreationWindow = new TourCreationWindow();
            tourCreationWindow.Show();
        }
    }
}
