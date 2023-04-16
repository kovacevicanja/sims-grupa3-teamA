using System;
using System.Collections.Generic;
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
    /// Interaction logic for ChangeStatsYearWindow.xaml
    /// </summary>
    public partial class ChangeStatsYearWindow : Window
    {
        public ChangeStatsYearWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            PickedYear = "all";
        }

        private string _pickedYear;
        public string PickedYear
        {
            get => _pickedYear;
            set
            {
                if (value != _pickedYear)
                {
                    _pickedYear = value;
                    OnPropertyChanged();
                }
            }
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow("all");
            tourStatisticsWindow.Show();
            Close();
        }
        private void Button_Click_Set(object sender, RoutedEventArgs e)
        {
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow(PickedYear);
            tourStatisticsWindow.Show();
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
