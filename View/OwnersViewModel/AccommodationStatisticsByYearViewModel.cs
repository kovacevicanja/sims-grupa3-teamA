using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.View.OwnersView;
using BookingProject.View.OwnerView;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Image = iTextSharp.text.Image;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;
using BookingProject.Properties;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using BookingProject.Domain;

namespace BookingProject.View.OwnerViewModel
{
    public class AccommodationStatisticsByYearViewModel
    {
        private Accommodation _selectedAccommodation { get; set; }
        public ObservableCollection<int> YearOption { get; set; }
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
        public RelayCommand BackCommand { get; set; }
        public int ChosenYear { get; set; }
        public int MostBusyYear { get; set; }
        public string MostBusyYearDisplay { get; set; }
        public RelayCommand GeneratePDF { get; set; }
        public NavigationService NavigationService { get; set; }
        public UserController UserController { get; set; }
        //private ComboBoxItem _chosenYear;
        //public ComboBoxItem ChosenYear
        //{
        //    get { return _chosenYear; }
        //    set
        //    {
        //        _chosenYear = value;
        //        OnPropertyChanged(); // Implement INotifyPropertyChanged to notify the UI of property changes
        //    }
        //}
        public RelayCommand OpenSecondWindowCommand { get; }
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
        public AccommodationStatisticsByYearViewModel(Accommodation selectedAccommodation, NavigationService navigationService)
        {
            _accommodationReservationController = new AccommodationReservationController();
            _requestController = new RequestAccommodationReservationController();
            _renovationController = new RecommendationRenovationController();
            UserController = new UserController();
            _selectedAccommodation = new Accommodation();
            _selectedAccommodation = selectedAccommodation;
            YearOption = new ObservableCollection<int>();
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            GeneratePDF = new RelayCommand(GeneratePDF_Click, CanExecute);
            MostBusyYear = _accommodationReservationController.FindTheMostBusyYear(_accommodationReservationController.GetAll());
            MostBusyYearDisplay = MostBusyYear.ToString();
            NumberOfReservations = new int[3];
            NumberOfCancelledReservations = new int[3];
            NumberOfRescheduledReservations = new int[3];
            NumberOfRenovationRecommendations= new int[3];
            NumberOfReservationsDisplay = new string[3];
            NumberOfCancelledReservationsDisplay = new string[3];
            NumberOfRescheduledReservationsDisplay = new string[3];
            NumberOfRenovationRecommendationsDisplay = new string[3];
            for (int i = 2023; i >= 2000; i--)
            {
                YearOption.Add(i);
            }
            int n = 0;
            for (int i = 2023; i >= 2021; i--)
            {
                
                NumberOfReservations[n] = _accommodationReservationController.CountReservationsForSpecificYear(i, _selectedAccommodation.Id);
                NumberOfReservationsDisplay[n] = NumberOfReservations[n].ToString();
                NumberOfCancelledReservations[n] = _accommodationReservationController.CountCancelledReservationsForSpecificYear(i, _selectedAccommodation.Id);
                NumberOfCancelledReservationsDisplay[n] = NumberOfCancelledReservations[n].ToString();
                NumberOfRescheduledReservations[n] = _requestController.CountRescheduledReservationsForSpecificYear(i, _selectedAccommodation.Id);
                NumberOfRescheduledReservationsDisplay[n] = NumberOfRescheduledReservations[n].ToString();
                NumberOfRenovationRecommendations[n] = _renovationController.CountAccommodationRenovationRecommendationsForSpecificYear(i, _selectedAccommodation.Id);
                NumberOfRenovationRecommendationsDisplay[n] = NumberOfRenovationRecommendations[n].ToString();
                n++;
            }
            OpenSecondWindowCommand = new RelayCommand(Button_Click_Open, CanExecute);
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
        private void Button_Click_Open(object param)
        {
            //var view = new AccommodationStatisticsByMonthView(ChosenYear, _selectedAccommodation);
            //view.Show();
            //CloseWindow();
            NavigationService.Navigate(new AccommodationStatisticsByMonthView(ChosenYear, _selectedAccommodation, NavigationService));
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AccommodationStatisticsByYearView)) { window.Close(); }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void GeneratePDF_Click(object param)
        {
            // Create a new PDF document
            string selName = _selectedAccommodation.AccommodationName;
            Document document = new Document();

            // Set up a stream to write the PDF to a file
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("statistics.pdf", FileMode.Create));

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

            // Add content to the document (e.g., paragraphs, images, tables, etc.)
            // ...
            Paragraph header = new Paragraph();
            header.Alignment = Element.ALIGN_CENTER;

            // Create a font for the header text
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.NORMAL);
            Font blueFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.NORMAL, BaseColor.BLUE);

            // Add the header text
            header.Add(new Chunk("Yearly statistics for ", headerFont));
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
            info.SpacingBefore= 60; 

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

            Chart chart = new Chart();
            chart.Width = 200;
            chart.Height = 200;

            // Create a chart area
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            // Create a series for reservations
            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;
            series.Name = "Reservations";
            List<AccommodationReservation> reservations = _accommodationReservationController.GetAllForAccommodation(_selectedAccommodation.Id);
            var reservationsByYear = reservations.GroupBy(r => r.EndDate.Year);

            // Add data to the series
            foreach (var group in reservationsByYear)
            {
                int year = group.Key;
                int count = group.Count();
                string label = string.Format("{0} ({1})", year, count);

                // Add data point with formatted label
                DataPoint dataPoint = series.Points.Add(count);
                dataPoint.AxisLabel = year.ToString();
                dataPoint.Label = label;
            }

            // Add the series to the chart
            chart.Series.Add(series);

           
            float chartX = 0; // Adjust the X position as needed
            float chartY = 0; // Adjust the Y position as needed
            float chartWidth = 80; // Adjust the width as needed
            float chartHeight = 80; // Adjust the height as needed
            chart.ChartAreas[0].Position = new ElementPosition(chartX, chartY, chartWidth, chartHeight);

            // Add the chart to the document
            using (MemoryStream ms = new MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);

                // Create an iTextSharp image from the chart image
                Image chartImage = Image.GetInstance(ms.GetBuffer());
                chartImage.Alignment = Element.ALIGN_LEFT;

                // Add the image to the document
                //document.Add(chartImage);
            }
            Font infoFont3 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);
            Paragraph resNum = new Paragraph();
            resNum.Add(new Chunk("Total number of reservations:  ", infoFont3));
            resNum.Add(new Chunk(_accommodationReservationController.CountResForAcc(_selectedAccommodation.Id).ToString(), infoFont3));
            resNum.IndentationLeft = 65;
            resNum.SpacingBefore = 35;

            document.Add(resNum);
            Paragraph cancresNum = new Paragraph();
            cancresNum.Add(new Chunk("Total number of cancelled reservations:  ", infoFont3));
            cancresNum.Add(new Chunk(_accommodationReservationController.CountCancelledForAcc(_selectedAccommodation.Id).ToString(), infoFont3));
            cancresNum.IndentationLeft = 320;
            cancresNum.SpacingBefore = -16;
            document.Add(cancresNum);
            Chart chart1 = new Chart();
            chart1.Width = 215;
            chart1.Height = 190;


            // Create a chart area
            ChartArea chartArea1 = new ChartArea();
            chart1.ChartAreas.Add(chartArea1);

            // Create a series for reservations
            Series series1 = new Series();
            series1.ChartType = SeriesChartType.Pie;
            series1.Name = "Cancelled";
            List<AccommodationReservation> cancelled = _accommodationReservationController.GetAllCancelledForAccommodation(_selectedAccommodation.Id);
            var cancelledreservationsByYear = cancelled.GroupBy(r => r.EndDate.Year);

            // Add data to the series
            foreach (var group in cancelledreservationsByYear)
            {
                int year = group.Key;
                int count = group.Count();
                string label = string.Format("{0} ({1})", year, count);

                // Add data point with formatted label
                DataPoint dataPoint = series1.Points.Add(count);
                dataPoint.AxisLabel = year.ToString();
                dataPoint.Label = label;
            }

            // Add the series to the chart
            chart1.Series.Add(series1);


            float chartXx = 22; // Adjust the X position as needed
            float chartYx = 0; // Adjust the Y position as needed
            float chartWidthx = 85; // Adjust the width as needed
            float chartHeightx = 85; // Adjust the height as needed
            chart1.ChartAreas[0].Position = new ElementPosition(chartXx, chartYx, chartWidthx, chartHeightx);

            // Add the chart to the document
            using (MemoryStream ms = new MemoryStream())
            {
                chart1.SaveImage(ms, ChartImageFormat.Png);

                // Create an iTextSharp image from the chart image
                Image chartImage = Image.GetInstance(ms.GetBuffer());
                chartImage.Alignment = Element.ALIGN_RIGHT;

                // Add the image to the document
                //document.Add(chartImage);
            }
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 80;

            // Create the first cell for the first chart
            PdfPCell cell1 = new PdfPCell(CreateChartImage(chart));
            cell1.Border = Rectangle.NO_BORDER;
            table.AddCell(cell1);

            // Create the second cell for the second chart
            PdfPCell cell2 = new PdfPCell(CreateChartImage(chart1));
            cell2.Border = Rectangle.NO_BORDER;
            table.AddCell(cell2);

            table.SpacingBefore = 20;
            table.SpacingAfter = 0;
            table.HorizontalAlignment= Element.ALIGN_CENTER;

            // Add the table to the document
            document.Add(table);
            Paragraph resResc = new Paragraph();
            resResc.Add(new Chunk("Total number of rescheduled reservations:  ", infoFont3));
            resResc.Add(new Chunk(_requestController.CountResForAcc(_selectedAccommodation.Id).ToString(), infoFont3));
            resResc.IndentationLeft = 65;
            resResc.SpacingBefore = 35;

            document.Add(resResc);
            Paragraph recRen = new Paragraph();
            recRen.Add(new Chunk("Total number of renovation recommendations:  ", infoFont3));
            recRen.Add(new Chunk(_renovationController.CountResForAcc(_selectedAccommodation.Id).ToString(), infoFont3));
            recRen.IndentationLeft = 320;
            recRen.SpacingBefore = -16;
            document.Add(recRen);
            //---------------------------------------------------------------------------------------------------------------------------------------------
            Chart chart2 = new Chart();
            chart2.Width = 200;
            chart2.Height = 200;

            // Create a chart area
            ChartArea chartArea2 = new ChartArea();
            chart2.ChartAreas.Add(chartArea2);

            // Create a series for reservations
            Series series2 = new Series();
            series2.ChartType = SeriesChartType.Pie;
            series2.Name = "Rescheduled";
            List<RequestAccommodationReservation> reservations2 = _requestController.GetAllForAccId(_selectedAccommodation.Id);
            var reservationsByYear2 = reservations2.GroupBy(r => r.AccommodationReservation.EndDate.Year);

            // Add data to the series
            foreach (var group in reservationsByYear2)
            {
                int year = group.Key;
                int count = group.Count();
                string label = string.Format("{0} ({1})", year, count);

                // Add data point with formatted label
                DataPoint dataPoint = series2.Points.Add(count);
                dataPoint.AxisLabel = year.ToString();
                dataPoint.Label = label;
            }

            // Add the series to the chart
            chart2.Series.Add(series2);


            float chartXy = 0; // Adjust the X position as needed
            float chartYy = 0; // Adjust the Y position as needed
            float chartWidthy = 80; // Adjust the width as needed
            float chartHeighty = 80; // Adjust the height as needed
            chart2.ChartAreas[0].Position = new ElementPosition(chartXy, chartYy, chartWidthy, chartHeighty);

            // Add the chart to the document
            using (MemoryStream ms = new MemoryStream())
            {
                chart2.SaveImage(ms, ChartImageFormat.Png);

                // Create an iTextSharp image from the chart image
                Image chartImage = Image.GetInstance(ms.GetBuffer());
                chartImage.Alignment = Element.ALIGN_LEFT;

                // Add the image to the document
                //document.Add(chartImage);
            }
            Chart chart3 = new Chart();
            chart3.Width = 215;
            chart3.Height = 190;


            // Create a chart area
            ChartArea chartArea3 = new ChartArea();
            chart3.ChartAreas.Add(chartArea3);

            // Create a series for reservations
            Series series3 = new Series();
            series3.ChartType = SeriesChartType.Pie;
            series3.Name = "Cancelled3";
            List<RecommendationRenovation> cancelled3 = _renovationController.GetAllCancelledForAccommodation(_selectedAccommodation.Id);
            var cancelledreservationsByYear3 = cancelled3.GroupBy(r => r.AccommodationReservation.EndDate.Year);

            // Add data to the series
            foreach (var group in cancelledreservationsByYear3)
            {
                int year = group.Key;
                int count = group.Count();
                string label = string.Format("{0} ({1})", year, count);

                // Add data point with formatted label
                DataPoint dataPoint = series3.Points.Add(count);
                dataPoint.AxisLabel = year.ToString();
                dataPoint.Label = label;
            }

            // Add the series to the chart
            chart3.Series.Add(series3);


            float chartXxx = 22; // Adjust the X position as needed
            float chartYxx = 0; // Adjust the Y position as needed
            float chartWidthxx = 85; // Adjust the width as needed
            float chartHeightxx = 85; // Adjust the height as needed
            chart3.ChartAreas[0].Position = new ElementPosition(chartXxx, chartYxx, chartWidthxx, chartHeightxx);

            // Add the chart to the document
            using (MemoryStream ms = new MemoryStream())
            {
                chart3.SaveImage(ms, ChartImageFormat.Png);

                // Create an iTextSharp image from the chart image
                Image chartImage = Image.GetInstance(ms.GetBuffer());
                chartImage.Alignment = Element.ALIGN_RIGHT;

                // Add the image to the document
                //document.Add(chartImage);
            }

            PdfPTable table2 = new PdfPTable(2);
            table2.WidthPercentage = 80;

            // Create the first cell for the first chart
            PdfPCell cell11 = new PdfPCell(CreateChartImage(chart2));
            cell11.Border = Rectangle.NO_BORDER;
            table2.AddCell(cell11);

            // Create the second cell for the second chart
            PdfPCell cell22 = new PdfPCell(CreateChartImage(chart3));
            cell22.Border = Rectangle.NO_BORDER;
            table2.AddCell(cell22);

            table2.SpacingBefore = 20;
            table2.SpacingAfter = 0;
            table2.HorizontalAlignment = Element.ALIGN_CENTER;

            // Add the table to the document
            document.Add(table2);
            // Add the paragraph to the document

            // Close the document
            document.Close();

            Process.Start("statistics.pdf");
        }
        private static Image CreateChartImage(Chart chart)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                chart.SaveImage(ms, ChartImageFormat.Png);

                // Create an iTextSharp image from the chart image
                Image chartImage = Image.GetInstance(ms.GetBuffer());
                chartImage.Alignment = Element.ALIGN_CENTER;

                return chartImage;
            }
        }
    }
}
