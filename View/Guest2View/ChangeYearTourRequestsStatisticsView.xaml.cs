using BookingProject.View.Guest2ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for ChangeYearTourStatistics.xaml
    /// </summary>
    public partial class ChangeYearTourRequestsStatisticsView : Window, INotifyPropertyChanged
    {
        public int GuestId { get; set; }    
        public ChangeYearTourRequestsStatisticsView(int guestId)
        {
            InitializeComponent();
            this.DataContext = this;

            GuestId = guestId;
        }
        public void Button_Click_ChangeYear(object sender, RoutedEventArgs e)
        {
            TourRequestStatisticsView tourRequestStatisticsView = new TourRequestStatisticsView(GuestId, EnteredYear);
            tourRequestStatisticsView.Show();

            this.Close();
        }
        public void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string _enteredYear;
        public string EnteredYear
        {
            get => _enteredYear;
            set
            {
                if (value != _enteredYear)
                {
                    _enteredYear = value;
                    OnPropertyChanged(nameof(EnteredYear));
                }
            }
        }
    }
}