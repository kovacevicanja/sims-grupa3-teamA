using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using Image = iTextSharp.text.Image;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;
using System.Windows.Media;
using Org.BouncyCastle.Asn1.Cmp;
using System.Windows.Documents;
using Paragraph = iTextSharp.text.Paragraph;
using BookingProject.Domain;

namespace BookingProject.View.OwnersViewModel
{
    public class AccommodationStatisticsByMonthViewModel
    {
        private Accommodation _selectedAccommodation;
        private int _selectedYear;
        public int[] NumberOfReservations;
        public string[] NumberOfReservationsDisplay { get; set; }
        public int[] NumberOfCancelledReservations;
        public string[] NumberOfCancelledReservationsDisplay { get; set; }
        public AccommodationReservationController _accommodationReservationController { get; set; }
        public int[] NumberOfRescheduledReservations;
        public string[] NumberOfRescheduledReservationsDisplay { get; set; }
        public RequestAccommodationReservationController _requestController { get; set; }
        public int[] NumberOfRenovationRecommendations;
        public string[] NumberOfRenovationRecommendationsDisplay { get; set; }
        public RecommendationRenovationController _renovationController { get; set; }
        public int MostBusyMonth { get; set; }
        public NavigationService NavigationService { get; set; }
        public RelayCommand GeneratePDF1 { get; set; }
        public RelayCommand BackCommand
        {
            get; set;
        }
        public string MostBusyMonthDisplay { get; set; }
        public int[] NumberOfReservationss
        {
            get { return NumberOfReservations; }
            set { NumberOfReservations = value; }
        }
        public int[] NumberOfCancelledReservationss
        {
            get { return NumberOfCancelledReservations; }
            set { NumberOfCancelledReservations = value; }
        }
        public int[] NumberOfRescheduledReservationss
        {
            get { return NumberOfRescheduledReservations; }
            set { NumberOfRescheduledReservations = value; }
        }
        public int[] NumberOfRenovationRecommendationss
        {
            get { return NumberOfRenovationRecommendations; }
            set { NumberOfRenovationRecommendations = value; }
        }
        public AccommodationStatisticsByMonthViewModel(int selectedYear, Accommodation selectedAccommodation, NavigationService navigationService)
        {
            _accommodationReservationController = new AccommodationReservationController();
            _requestController = new RequestAccommodationReservationController();
            _renovationController = new RecommendationRenovationController();
            _selectedAccommodation = new Accommodation();
            _selectedAccommodation = selectedAccommodation;
            _selectedYear = selectedYear;
            DateTimeFormatInfo dateTimeFormat = new DateTimeFormatInfo();
            MostBusyMonth = _accommodationReservationController.GetMostBusyMonth(_accommodationReservationController.GetAll(), _selectedYear);
            MostBusyMonthDisplay = dateTimeFormat.GetMonthName(MostBusyMonth+1);
            GeneratePDF1 = new RelayCommand(GeneratePDF_Click, CanExecute);
            //MostBusyMonthDisplay = MostBusyMonth.ToString();
            NumberOfReservations = new int[12];
            NumberOfCancelledReservations = new int[12];
            NumberOfRescheduledReservations = new int[12];
            NumberOfRenovationRecommendations = new int[12];
            NumberOfReservationsDisplay = new string[12];
            NumberOfCancelledReservationsDisplay = new string[12];
            NumberOfRescheduledReservationsDisplay = new string[12];
            NumberOfRenovationRecommendationsDisplay = new string[12];
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            int n = 0;
            for (int i = 0; i < 12; i++)
            {

                NumberOfReservations[n] = _accommodationReservationController.CountReservationsForSpecificMonth(_selectedYear, i, _selectedAccommodation.Id);
                NumberOfReservationsDisplay[n] = NumberOfReservations[n].ToString();
                NumberOfCancelledReservations[n] = _accommodationReservationController.CountCancelledReservationsForSpecificMonth(_selectedYear,i, _selectedAccommodation.Id);
                NumberOfCancelledReservationsDisplay[n] = NumberOfCancelledReservations[n].ToString();
                NumberOfRescheduledReservations[n] = _requestController.CountRescheduledReservationsForSpecificMonth(_selectedYear,i, _selectedAccommodation.Id);
                NumberOfRescheduledReservationsDisplay[n] = NumberOfRescheduledReservations[n].ToString();
                NumberOfRenovationRecommendations[n] = _renovationController.CountAccommodationRenovationRecommendationsForSpecificMonth(_selectedYear,i, _selectedAccommodation.Id);
                NumberOfRenovationRecommendationsDisplay[n] = NumberOfRenovationRecommendations[n].ToString();
                n++;
            }
            NavigationService = navigationService;
        }
        private void Button_Click_Back(object param)
        {
            NavigationService.GoBack();
        }
        private bool CanExecute(object param) { return true; }
        public Accommodation SelectedReservation
        {
            get { return _selectedAccommodation; }
            set { _selectedAccommodation = value; OnPropertyChanged(); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void GeneratePDF_Click(object param)
        {
            string selName = _selectedAccommodation.AccommodationName;
            Document document = new Document();

            // Set up a stream to write the PDF to a file
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("statistics2.pdf", FileMode.Create));

            // Open the document for writing
            document.Open();

            PdfContentByte content = writer.DirectContent;

            // Get the page size
            Rectangle pageSize = document.PageSize;

            // Set the border thickness and color
            float borderThickness = 3f;
            BaseColor borderColor = BaseColor.BLACK;

            // Draw the top border
            content.SetLineWidth(borderThickness);
            content.SetColorStroke(borderColor);
            content.MoveTo(pageSize.Left, pageSize.Top - borderThickness / 2);
            content.LineTo(pageSize.Right, pageSize.Top - borderThickness / 2);
            content.Stroke();

            // Draw the bottom border
            content.MoveTo(pageSize.Left, pageSize.Bottom + borderThickness / 2);
            content.LineTo(pageSize.Right, pageSize.Bottom + borderThickness / 2);
            content.Stroke();

            // Draw the left border
            content.MoveTo(pageSize.Left + borderThickness / 2, pageSize.Top);
            content.LineTo(pageSize.Left + borderThickness / 2, pageSize.Bottom);
            content.Stroke();

            // Draw the right border
            content.MoveTo(pageSize.Right - borderThickness / 2, pageSize.Top);
            content.LineTo(pageSize.Right - borderThickness / 2, pageSize.Bottom);
            content.Stroke();

            Paragraph header = new Paragraph();
            header.Alignment = Element.ALIGN_CENTER;

            // Create a font for the header text
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.NORMAL);
            Font blueFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.NORMAL, BaseColor.BLUE);

            // Add the header text
            header.Add(new Chunk("Monthly statistics for ", headerFont));
            header.Add(new Chunk(selName, blueFont));


            // Add spacing after the header
            header.SpacingAfter = 20f;

            // Add the header to the document
            document.Add(header);
            string imagePath = _selectedAccommodation.Images[0].Url; // Replace with the actual path to your image file
            Image image = Image.GetInstance(imagePath);

            // Set the position and size of the image on the page
            image.SetAbsolutePosition(100, 600); // Adjust the position as needed
            image.ScaleToFit(200, 500); // Adjust the size as needed

            // Add the image to the document
            document.Add(image);
            Paragraph info = new Paragraph();

            // Create a font for the header text
            Font infoFont2 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, Font.NORMAL, BaseColor.GRAY);
            Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, Font.NORMAL);

            // Add the header text
            info.Add(new Chunk("Location:  ", infoFont2));
            info.Add(new Chunk(_selectedAccommodation.Location.City, infoFont));
            info.Add(new Chunk(", ", infoFont));
            info.Add(new Chunk(_selectedAccommodation.Location.Country, infoFont));
            info.IndentationLeft = 300;
            info.SpacingBefore = 60;

            document.Add(info);
            Paragraph info2 = new Paragraph();
            info2.Add(new Chunk("Type:  ", infoFont2));
            info2.Add(new Chunk(_selectedAccommodation.Type.ToString(), infoFont));
            info2.IndentationLeft = 300;
            info2.SpacingBefore = 10;

            document.Add(info2);
            Paragraph info3 = new Paragraph();
            info3.Add(new Chunk("Capacity:  ", infoFont2));
            info3.Add(new Chunk(_selectedAccommodation.MaxGuestNumber.ToString(), infoFont));
            info3.IndentationLeft = 300;
            info3.SpacingBefore = 10;

            document.Add(info3);
            Paragraph info4 = new Paragraph();
            info4.Add(new Chunk("Minimum days:  ", infoFont2));
            info4.Add(new Chunk(_selectedAccommodation.MinDays.ToString(), infoFont));
            info4.IndentationLeft = 300;
            info4.SpacingBefore = 10;

            document.Add(info4);
            //***********************************************************************************************************************************
            Paragraph info5 = new Paragraph();
            info5.Add(new Chunk("Total number of reservations:  ", infoFont2));
            info5.Add(new Chunk(_accommodationReservationController.CountResForAccAndYear(_selectedAccommodation.Id,_selectedYear).ToString(), infoFont));
            info5.Alignment = Element.ALIGN_CENTER;
            info5.SpacingBefore = 30;
            document.Add(info5);



            Chart chart = new Chart();
            chart.Width = 550;
            chart.Height = 180;

            // Create a chart area
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Title = "Month";
            chartArea.AxisY.Title = "Reservations";
            chart.ChartAreas.Add(chartArea);

            // Get the reservations and group them by year
            List<AccommodationReservation> reservations = _accommodationReservationController.GetAllForAccommodationAnYear(_selectedAccommodation.Id, _selectedYear);
            var reservationsByYear = reservations.GroupBy(r => r.EndDate.Month);

            // Create a series for reservations
            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            series.Name = "Reservations";
            series["PixelPointWidth"] = "30";

            // Add data to the series
            foreach (var group in reservationsByYear)
            {
                int month = group.Key;
                int count = group.Count();
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                //series.Points.AddXY(year.ToString(), count);
                series.Points.AddXY(monthName, count);
            }
            
            // Add the series to the chart
            chart.Series.Add(series);


            // Render the chart to an image
            using (var stream = new System.IO.MemoryStream())
            {
                chart.SaveImage(stream, ChartImageFormat.Png);

                // Create an iTextSharp image from the chart image
                Image chartImage = Image.GetInstance(stream.GetBuffer());
                float imageX = 10f; // Adjust the X position as needed
                float imageY = 370f; // Adjust the Y position as needed
                chartImage.SetAbsolutePosition(imageX, imageY);

                // Set the position and size of the image on the page


                // Add the image to the document
                document.Add(chartImage);
            }
            //***************************************************************************************************************************
            Paragraph info6 = new Paragraph();
            info6.Add(new Chunk("Total number of cancelled reservations:  ", infoFont2));
            info6.Add(new Chunk(_accommodationReservationController.CountCancelledForAccAndYear(_selectedAccommodation.Id, _selectedYear).ToString(), infoFont));
            info6.Alignment = Element.ALIGN_CENTER;
            info6.SpacingBefore = 250;
            document.Add(info6);
            Chart chart1 = new Chart();
            chart1.Width = 550;
            chart1.Height = 180;

            // Create a chart area
            ChartArea chartArea1 = new ChartArea();
            chartArea1.AxisX.Title = "Month";
            chartArea1.AxisY.Title = "Reservations";
            chart1.ChartAreas.Add(chartArea1);

            // Get the reservations and group them by year
            List<AccommodationReservation> reservations1 = _accommodationReservationController.GetAllCancelledForAccommodationAndYear(_selectedAccommodation.Id, _selectedYear);
            var reservationsByYear1 = reservations1.GroupBy(r => r.EndDate.Month);

            // Create a series for reservations
            Series series1 = new Series();
            series1.ChartType = SeriesChartType.Column;
            series1.Name = "Reservations1";
            series1["PixelPointWidth"] = "30";

            // Add data to the series
            foreach (var group in reservationsByYear1)
            {
                int month = group.Key;
                int count = group.Count();
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                //series.Points.AddXY(year.ToString(), count);
                series1.Points.AddXY(monthName, count);
            }

            // Add the series to the chart
            chart1.Series.Add(series1);


            // Render the chart to an image
            using (var stream = new System.IO.MemoryStream())
            {
                chart1.SaveImage(stream, ChartImageFormat.Png);

                // Create an iTextSharp image from the chart image
                Image chartImage = Image.GetInstance(stream.GetBuffer());
                float imageX = 10f; // Adjust the X position as needed
                float imageY = 100f; // Adjust the Y position as needed
                chartImage.SetAbsolutePosition(imageX, imageY);

                // Set the position and size of the image on the page


                // Add the image to the document
                document.Add(chartImage);
            }
            //*********************************************************************************************************************************
            document.NewPage();
            Paragraph info7 = new Paragraph();
            info7.Add(new Chunk("Total number of rescheduled reservations:  ", infoFont2));
            info7.Add(new Chunk(_requestController.CountResForAccAndYear(_selectedAccommodation.Id, _selectedYear).ToString(), infoFont));
            info7.Alignment = Element.ALIGN_CENTER;
            info7.SpacingBefore = 0;
            document.Add(info7);
            Chart chart2= new Chart();
            chart2.Width = 550;
            chart2.Height = 180;

            // Create a chart area
            ChartArea chartArea2 = new ChartArea();
            chartArea2.AxisX.Title = "Month";
            chartArea2.AxisY.Title = "Reservations";
            chart2.ChartAreas.Add(chartArea2);

            // Get the reservations and group them by year
            List<RequestAccommodationReservation> reservations2 = _requestController.GetAllForAccIdAndYear(_selectedAccommodation.Id, _selectedYear);
            var reservationsByYear2 = reservations2.GroupBy(r => r.NewDeparuteDay.Month);

            // Create a series for reservations
            Series series2 = new Series();
            series2.ChartType = SeriesChartType.Column;
            series2.Name = "Reservations2";
            series2["PixelPointWidth"] = "30";

            // Add data to the series
            foreach (var group in reservationsByYear2)
            {
                int month = group.Key;
                int count = group.Count();
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                //series.Points.AddXY(year.ToString(), count);
                series2.Points.AddXY(monthName, count);
            }

            // Add the series to the chart
            chart2.Series.Add(series2);


            // Render the chart to an image
            using (var stream = new System.IO.MemoryStream())
            {
                chart2.SaveImage(stream, ChartImageFormat.Png);

                // Create an iTextSharp image from the chart image
                Image chartImage1 = Image.GetInstance(stream.GetBuffer());
                float imageX = 10f; // Adjust the X position as needed
                float imageY = 580; // Adjust the Y position as needed
                chartImage1.SetAbsolutePosition(imageX, imageY);

                // Set the position and size of the image on the page


                // Add the image to the document
                document.Add(chartImage1);
            }
            //*********************************************************************************************************************************
            Paragraph info8 = new Paragraph();
            info8.Add(new Chunk("Total number of renovation recommendations:  ", infoFont2));
            info8.Add(new Chunk(_renovationController.CountResForAccAndYear(_selectedAccommodation.Id, _selectedYear).ToString(), infoFont));
            info8.Alignment = Element.ALIGN_CENTER;
            info8.SpacingBefore = 225;
            document.Add(info8);
            Chart chart3 = new Chart();
            chart3.Width = 550;
            chart3.Height = 180;

            // Create a chart area
            ChartArea chartArea3 = new ChartArea();
            chartArea3.AxisX.Title = "Month";
            chartArea3.AxisY.Title = "Reservations";
            chart3.ChartAreas.Add(chartArea3);

            // Get the reservations and group them by year
            List<RecommendationRenovation> reservations3 = _renovationController.GetAllCancelledForAccommodationAndYear(_selectedAccommodation.Id, _selectedYear);
            var reservationsByYear3 = reservations3.GroupBy(r => r.AccommodationReservation.EndDate.Month);

            // Create a series for reservations
            Series series3 = new Series();
            series3.ChartType = SeriesChartType.Column;
            series3.Name = "Reservations3";
            series3["PixelPointWidth"] = "30";

            // Add data to the series
            foreach (var group in reservationsByYear3)
            {
                int month = group.Key;
                int count = group.Count();
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                //series.Points.AddXY(year.ToString(), count);
                series3.Points.AddXY(monthName, count);
            }

            // Add the series to the chart
            chart3.Series.Add(series3);


            // Render the chart to an image
            using (var stream = new System.IO.MemoryStream())
            {
                chart3.SaveImage(stream, ChartImageFormat.Png);

                // Create an iTextSharp image from the chart image
                Image chartImage2 = Image.GetInstance(stream.GetBuffer());
                float imageX = 10f; // Adjust the X position as needed
                float imageY = 330f; // Adjust the Y position as needed
                chartImage2.SetAbsolutePosition(imageX, imageY);

                // Set the position and size of the image on the page


                // Add the image to the document
                document.Add(chartImage2);
            }

            document.Close();

            Process.Start("statistics2.pdf");
        }
    }
}
