using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for GuestListView.xaml
    /// </summary>
    public partial class GuestListView : Window
    {


        public KeyPoint ChosenKeyPoint { get; set; }

        private TourTimeInstanceController _tourTimeInstanceController;
        private KeyPointController _keyPointController;
        private ObservableCollection<KeyPoint> _keyPoints;
        private TourTimeInstance ChosenTour { get; set; }

        public GuestListView(KeyPoint chosenKeyPoint, TourTimeInstance chosenTour)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourTimeInstanceController = new TourTimeInstanceController();
            _keyPointController = new KeyPointController();
            ChosenTour = chosenTour;
        }



        private void Button_Click_Mark(object sender, RoutedEventArgs e)
        {
            //           if (ChosenKeyPoint != null)
            //         {
            //              passedState();
            //             currentState();
            //              GuestListView guestListView = new GuestListView(ChosenKeyPoint);
            //              guestListView.Show();
            //
            //   }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Cancell(object sender, RoutedEventArgs e)
        {
            LiveTourView liveTourView = new LiveTourView(ChosenTour);
            liveTourView.Show();
            Close();

        }


    }

}
