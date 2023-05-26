using BookingProject.Controllers;
using BookingProject.Model.Enums;
using BookingProject.View.Guest2ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for TourRequestsChartView.xaml
    /// </summary>
    public partial class TourRequestsLanguageChartView : Page
    {
        TourRequestController _tourRequestController = new TourRequestController();
        public int GuestId { get; set; }    
        public NavigationService NavigationService { get; set; }
        public string EnteredYear { get; set; }
        public string DisplayYear { get; set; }
        public TourRequestsLanguageChartView(int guestId, NavigationService navigationService, string enteredYear = "")
        {
            InitializeComponent();
            this.DataContext = this;

            _tourRequestController = new TourRequestController();

            GuestId = guestId;
            NavigationService = navigationService;
            EnteredYear = enteredYear;

            if (!enteredYear.Equals(""))
            {
                DisplayYear = "for year " + enteredYear;
            }
            else
            {
                DisplayYear = "for all times";
            }

            Chart1.Series[0].Points.Add(_tourRequestController.GetNumberRequestsLanguage(guestId, LanguageEnum.ENGLISH, enteredYear)).AxisLabel = "English";
            Chart1.Series[0].Points.Add(_tourRequestController.GetNumberRequestsLanguage(guestId, LanguageEnum.SERBIAN, enteredYear)).AxisLabel = "Serbian";
            Chart1.Series[0].Points.Add(_tourRequestController.GetNumberRequestsLanguage(guestId, LanguageEnum.GERMAN, enteredYear)).AxisLabel = "German";
            Chart1.Series[0].Points.Add(_tourRequestController.GetNumberRequestsLanguage(guestId, LanguageEnum.SPANISH, enteredYear)).AxisLabel = "Spanish";

            Chart1.Series[0].Color = System.Drawing.Color.LightBlue;

            Chart1.ChartAreas[0].AxisX.Interval = 1;
            Chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
            Chart1.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);

            Chart1.ChartAreas[0].AxisY.Interval = 5;
            Chart1.ChartAreas[0].AxisY.Minimum = 0;
            Chart1.ChartAreas[0].AxisY.Maximum = 30; 
            Chart1.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);

            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);

            Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            Chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Chart1.ChartAreas[0].BackColor = System.Drawing.Color.White;
            Chart1.ChartAreas[0].ShadowColor = System.Drawing.Color.Gray;
            Chart1.ChartAreas[0].ShadowOffset = 2;
        }

        private void Button_Click_ChangeTheYear(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeYearTourRequestsStatisticsView(GuestId, NavigationService, "languageChart"));
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}