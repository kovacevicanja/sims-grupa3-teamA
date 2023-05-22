using LiveCharts;
using LiveCharts.Defaults;
using Microsoft.Build.Framework.XamlTypes;
using Microsoft.VisualBasic.Logging;
using Sparrow.Chart;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization;
using BookingProject.Controllers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using BookingProject.Domain;

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticsPieChart.xaml
    /// </summary>
    public partial class TourRequestStatisticsPieChart : Page
    {
        TourRequestController _tourRequestController { get; set; }
        public int GuestId { get; set; }
        public string EnteredYear { get; set; }
        public NavigationService NavigationService { get; set; }
        public string DisplayMesaage { get; set; }
        public string DisplayYear { get; set; }

        public TourRequestStatisticsPieChart(int guestId, NavigationService navigationService, string enteredYear = "")
        {
            InitializeComponent();
            this.DataContext = this;

            GuestId = guestId;
            NavigationService = navigationService;
            EnteredYear = enteredYear;

            _tourRequestController = new TourRequestController();

            List<TourRequest> tourRequests = _tourRequestController.GetAll();

            if (!enteredYear.Equals(""))
            {
                DisplayYear = "for year " + enteredYear;
            }
            else
            {
                DisplayYear = "for all times";
            }
            var series = Chart1.Series[0];

            /*var series = Chart1.Series[0];

            series.Points.Add(_tourRequestController.GetUnacceptedRequestsPercentage(GuestId, EnteredYear))
                  .AxisLabel = "Rejected requests";
            series.Points[0].Color = System.Drawing.Color.Gray;

            series.Points.Add(_tourRequestController.GetAcceptedRequestsPercentage(GuestId, EnteredYear))
                  .AxisLabel = "Accepted requests";
            series.Points[1].Color = System.Drawing.Color.LightBlue;
            */
            int flagYear = 0;

            if (enteredYear != "")
            {
                foreach (TourRequest request in tourRequests)
                {
                    if (request.StartDate.Year.ToString().Equals(enteredYear) && request.EndDate.Year.ToString().Equals(enteredYear))
                    {
                        flagYear = 1;
                        break;
                    }
                }
                if (flagYear == 0)
                {
                     series.Points.Add(0).AxisLabel = "";
                     DisplayMesaage = " - there are no requirements for this year.";
                     return;
                }
            }

            double unacceptedPercentage = _tourRequestController.GetUnacceptedRequestsPercentage(GuestId, EnteredYear);
            double acceptedPercentage = _tourRequestController.GetAcceptedRequestsPercentage(GuestId, EnteredYear);
            double pendingPercentage = 100 - (unacceptedPercentage + acceptedPercentage);   

            string unnaceptedString = Math.Round(unacceptedPercentage, 2).ToString() + " %";
            string acceptedString = Math.Round(acceptedPercentage, 2).ToString() + " %";
            string pendingString = Math.Round(pendingPercentage, 2).ToString() + " %";

            int flag = 0;



            /*if (unacceptedPercentage == 0 && acceptedPercentage == 0 && pendingPercentage == 0)
            {
                series.Points.Add(unacceptedPercentage).AxisLabel = "There are no requirements for this year.";
                series.Points[0].Color = System.Drawing.Color.Gray;
            }
            */

            if (unacceptedPercentage != 0)
            {
                series.Points.Add(unacceptedPercentage).AxisLabel = unnaceptedString;
                series.Points[0].Color = System.Drawing.Color.Gray;
            }

            if (acceptedPercentage != 0) 
            { 
                series.Points.Add(acceptedPercentage).AxisLabel = acceptedString;
                if (unacceptedPercentage == 0)
                {
                    series.Points[0].Color = System.Drawing.Color.LightBlue;
                }
                else
                {
                    flag = 1;
                    series.Points[1].Color = System.Drawing.Color.LightBlue;
                }
            }

            if (pendingPercentage != 0)
            {
                series.Points.Add(pendingPercentage).AxisLabel = pendingString;
                if (unacceptedPercentage == 0 && acceptedPercentage == 0)
                {
                    series.Points[0].Color = System.Drawing.Color.LightGray;
                }
                else if (flag == 0)
                {
                    series.Points[1].Color = System.Drawing.Color.LightGray;
                }
                else
                {
                    series.Points[2].Color = System.Drawing.Color.LightGray;
                }
            }

            //Chart1.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);
            //Chart1.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);

            series.Font = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold);



            /*Chart1.Series[0].Color = System.Drawing.Color.LightBlue;

            Chart1.ChartAreas[0].AxisX.Interval = 1;
            Chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
            Chart1.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);

            Chart1.ChartAreas[0].AxisY.Interval = 1;
            Chart1.ChartAreas[0].AxisY.Minimum = 0;
            Chart1.ChartAreas[0].AxisY.Maximum = 15;
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

            //chart1.Titles.Add("Pie Chart");

            //chart1.Series["s1"].Points.AddXY("1", "30");
            //chart1.Series["s1"].IsValueShownAsLabel = true;
            //chart1.Series["s2"].Points.AddXY("1", "70");

            /*
            GuestId = guestId;
            EnteredYear = enteredYear;

            _tourRequestController = new TourRequestController();

            UnacceptedRequestsPercentage = _tourRequestController.GetUnacceptedRequestsPercentage(GuestId, EnteredYear);
            string UnacceptedRequestsPercentageDisplay = Math.Round(UnacceptedRequestsPercentage, 2).ToString() + " %";

            AccpetedRequestsPercentage = _tourRequestController.GetAcceptedRequestsPercentage(GuestId, EnteredYear);
            //string AccpetedRequestsPercentageDisplay = Math.Round(AccpetedRequestsPercentage, 2).ToString() + " %";

            /*
            SeriesCollection = new SeriesCollection()
            {
                new PieSeries
                {
                    Name = "t1",
                    PiePoints = new PointCollection(),
                    Label = true
                }
            };
            */

            /* PointCollection points1 = new PointCollection();
             points1.Add(new Point(1, 30));
             points1.Add(new Point(2, 70));
             SeriesCollection = new SeriesCollection()
             {
                 PieSeries pieSeries = new PieSeries
                 {
                     Title = "s1",
                     Values = new ChartValues<ObservablePoint>(points1.Select(p => new ObservablePoint(p.X, p.Y)))
                 }
             };

             seriesCollection.Add(pieSeries);

             // Set the SeriesCollection property of your chart control
             chart1.SeriesCollection = seriesCollection;
            */
        }

        private void Button_Click_ChangeTheYear(object  sender, EventArgs e)
        { 
            NavigationService.Navigate(new ChangeYearTourRequestsStatisticsView(GuestId, NavigationService, "pieChart"));
        }

         private void Button_Cancel(object sender, RoutedEventArgs e)
         {
            NavigationService.GoBack();
         }
}
}