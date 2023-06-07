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

            if (unacceptedPercentage != 0)
            {
                series.Points.Add(unacceptedPercentage).AxisLabel = unnaceptedString;
                series.Points[0].Color = System.Drawing.Color.Gray;
                series.Points[0].LabelForeColor = System.Drawing.Color.White; // Set the label fore color to white
            }

            if (acceptedPercentage != 0)
            {
                series.Points.Add(acceptedPercentage).AxisLabel = acceptedString;
                if (unacceptedPercentage == 0)
                {
                    series.Points[0].Color = System.Drawing.Color.LightBlue;
                    series.Points[0].LabelForeColor = System.Drawing.Color.Gray; // Set the label fore color to white
                }
                else
                {
                    flag = 1;
                    series.Points[1].Color = System.Drawing.Color.LightBlue;
                    series.Points[1].LabelForeColor = System.Drawing.Color.Gray; // Set the label fore color to white
                }
            }

            if (pendingPercentage != 0)
            {
                series.Points.Add(pendingPercentage).AxisLabel = pendingString;
                if (unacceptedPercentage == 0 && acceptedPercentage == 0)
                {
                    series.Points[0].Color = System.Drawing.Color.LightGray;
                    series.Points[0].LabelForeColor = System.Drawing.Color.Gray; // Set the label fore color to white
                }
                else if (flag == 0)
                {
                    series.Points[1].Color = System.Drawing.Color.LightGray;
                    series.Points[1].LabelForeColor = System.Drawing.Color.Gray; // Set the label fore color to white
                }
                else
                {
                    series.Points[2].Color = System.Drawing.Color.LightGray;
                    series.Points[2].LabelForeColor = System.Drawing.Color.Gray; // Set the label fore color to white
                }
            }

            series.Font = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold);
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