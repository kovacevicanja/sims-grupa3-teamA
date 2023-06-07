using BookingProject.Controllers;
using BookingProject.Domain;
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
    /// Interaction logic for TourRequestsAveragesChartView.xaml
    /// </summary>
    public partial class TourRequestsAveragesChartView : Page
    {
        public TourRequestController _tourRequestController { get; set; } 
        public NavigationService NavigationService { get; set; }
        public int GuestId { get; set; }
        public string EnteredYear { get; set; }
        public string DisplayYear { get; set; }
        public string DisplayMesaage { get; set; }
        public string DisplayYear2 { get; set; }

        public TourRequestsAveragesChartView(int guestId, NavigationService navigationService, string enteredYear = "")
        {
            InitializeComponent();
            this.DataContext = this;

            _tourRequestController = new TourRequestController();

            GuestId = guestId;
            NavigationService = navigationService;
            EnteredYear = enteredYear;
            DisplayYear2 = "";

            if (!enteredYear.Equals(""))
            {
                DisplayYear = "for year " + enteredYear;
            }
            else
            {
                DisplayYear = "for all times";
            }

            if (enteredYear != "")
            {
                if (_tourRequestController.AcceptedRequestsList(guestId, enteredYear).Count() == 0)
                {
                    DisplayMesaage = " - there are no requirements for ";
                    DisplayYear = "";
                    DisplayYear2 = EnteredYear;
                    return;
                }
            }

            int i = 1;

            Chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            // Customize x-axis title
            Chart1.ChartAreas[0].AxisX.Title = "Request";
            Chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Gray;

            // Customize y-axis title
            Chart1.ChartAreas[0].AxisY.Title = "Number of People";
            Chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Gray;

            foreach (TourRequest request in _tourRequestController.AcceptedRequestsList(guestId, enteredYear))
            {
                Chart1.Series[0].Points.Add(request.GuestsNumber).AxisLabel = "request " + i;
                i++;
            }

            Chart1.Series[0].Color = System.Drawing.Color.LightBlue;
            Chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; // Set the chart type to Spline

            Chart1.ChartAreas[0].AxisY.Interval = 5;
            Chart1.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gray; // Set the gray color for y-axis labels

            Chart1.ChartAreas[0].AxisY.Minimum = 0;
            Chart1.ChartAreas[0].AxisY.Maximum = 30;
            Chart1.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gray; // Set the gray color for y-axis labels

            Chart1.Series[0].IsValueShownAsLabel = false;
            Chart1.Series[0].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);

            Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            Chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            Chart1.ChartAreas[0].BackColor = System.Drawing.Color.White;
            Chart1.ChartAreas[0].ShadowColor = System.Drawing.Color.Gray;
            Chart1.ChartAreas[0].ShadowOffset = 2;

            double averageValue = _tourRequestController.GetAvarageNumberOfPeopleInAcceptedRequests(guestId, enteredYear);

            System.Windows.Forms.DataVisualization.Charting.StripLine averageLine = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            averageLine.Interval = 0;
            averageLine.IntervalOffset = averageValue;
            averageLine.StripWidth = 0.1;
            averageLine.BackColor = System.Drawing.Color.Gray;
            averageLine.Text = "Average number of people: " + averageValue;
            averageLine.TextAlignment = System.Drawing.StringAlignment.Far;
            averageLine.TextLineAlignment = System.Drawing.StringAlignment.Near; // Set the position of the label
            averageLine.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold); // Customize the font of the label
            averageLine.ForeColor = System.Drawing.Color.Gray; // Set the gray color for the text
            Chart1.ChartAreas[0].AxisY.StripLines.Add(averageLine);

            // Customize the appearance of the series
            Chart1.Series[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash; // Set the border dash style to Dash
            Chart1.Series[0].BorderWidth = 2; // Set the border width

        }
        private void Button_Click_ChangeTheYear(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeYearTourRequestsStatisticsView(GuestId, NavigationService, "averagesChart"));
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
