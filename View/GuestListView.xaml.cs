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
using System.Xml.Serialization;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for GuestListView.xaml
    /// </summary>
    public partial class GuestListView : Window
    {




        public bool IsValid { get; set; }
        public TourGuest ChosenGuest { get; set; }

        private TourGuestController _tourGuestController;
        private ObservableCollection<TourGuest> _guests;
        private KeyPointController _keyPointController;


        public KeyPoint ChosenKeyPoint { get; set; }
        public TourTimeInstance ChosenTour { get; set; }

        public GuestListView(TourTimeInstance chosenTour, KeyPoint chosenKeyPoint)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourGuestController = new TourGuestController();
            _keyPointController = new KeyPointController();
            ChosenTour = chosenTour;
            ChosenKeyPoint = chosenKeyPoint;
            _guests = new ObservableCollection<TourGuest>(filterGuests(_tourGuestController.GetAll()));
            GuestDataGrid.ItemsSource = _guests;
        }

        public List<TourGuest> filterGuests(List<TourGuest> guests)
        {
            List<TourGuest> filteredGuests= new List<TourGuest>();
            foreach(TourGuest guest in guests)
            {
                if (guest.TourInstanceId == ChosenTour.Id)
                {
                    filteredGuests.Add(guest);
                }
            }
            return filteredGuests;
        }








        private void Button_Click_Mark(object sender, RoutedEventArgs e)
        {
            if (ChosenGuest != null)
            {
                GuestPreseanceCheck guestPreseanceCheck = new GuestPreseanceCheck(ChosenTour, ChosenKeyPoint, ChosenGuest);
                guestPreseanceCheck.Show();
                Close();
            
            }
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
