using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.Commands;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Documents;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Paragraph = iTextSharp.text.Paragraph;
using Font = iTextSharp.text.Font;
using System.Net;
using Image = iTextSharp.text.Image;

namespace BookingProject.View.GuideViewModel
{
    public class MyToursViewModel: INotifyPropertyChanged
    {
        private TourTimeInstanceController _tourTimeInstanceController;
        private TourStartingTimeController _tourStartingTimeController;
        private TourReservationController _tourReservationController;
        private UserController _userController;
        public ObservableCollection<TourTimeInstance> _instances;
        private TourController _tourController;
        public RelayCommand CancelCommand { get; }
        public RelayCommand CancelTourCommand { get; }
        public RelayCommand CreateCommand { get; }
        public TourTimeInstance ChosenTour { get; set; }
        public MyToursViewModel()
        {
            _tourController = new TourController();
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourStartingTimeController = new TourStartingTimeController();
            _tourReservationController= new TourReservationController();
            _userController = new UserController();
            _instances = new ObservableCollection<TourTimeInstance>(FilterTours(_tourTimeInstanceController.GetAll()));
            CancelCommand = new RelayCommand(Button_Click_Close, CanExecute);
            CancelTourCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            CreateCommand = new RelayCommand(Button_Click_Report, CanExecute);
        }
        public List<TourTimeInstance> FilterTours(List<TourTimeInstance> tours)
        {
            List<TourTimeInstance> filteredTours = new List<TourTimeInstance>();
            foreach (TourTimeInstance tour in tours)
            {
                if (tour.State != TourState.CANCELLED && tour.Tour.GuideId==_userController.GetLoggedUser().Id)
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_Close(object param)
        {
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            CloseWindow();
        }

        private void Button_Click_Report(object param)
        {
            GenerateReport();
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(MyToursWindow)) { window.Close(); }
            }
        }
        private void Button_Click_Cancel(object param)
        {
            if (ChosenTour != null && IsNotLate(ChosenTour))
            {
                TourCancellationWindow tourCancellationWindow = new TourCancellationWindow(ChosenTour);
                tourCancellationWindow.Show();
                CloseWindow();
            }
        }
        private bool IsNotLate(TourTimeInstance tour)
        {
            TourDateTime tourDate = new TourDateTime();
            tourDate = _tourStartingTimeController.GetById(tour.DateId);
            TimeSpan ts = tourDate.StartingDateTime - DateTime.Now;
            if (ts > TimeSpan.FromHours(48))
            {
                return true;
            }
            return false;
        }
        private void Button_Click_Create(object param)
        {
            //TourCreationWindow tourCreationWindow = new TourCreationWindow();
            //tourCreationWindow.Show();
            //CloseWindow();
        }


        public List<User> filterGuests(List<TourReservation> reservations)
        {
            List<User> users = new List<User>();
            foreach (TourReservation reservation in reservations)
            {
                if (reservation.Tour.Id == ChosenTour.TourId)
                {
                    users.Add(_userController.GetById(reservation.Guest.Id));
                }
            }
            return users;
        }
        public void GenerateReport()
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ChosenTour.Tour.Name + "_report.pdf", FileMode.Create));
            document.Open();

            Font labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            Paragraph labelParagraph = new Paragraph("Tour Report", labelFont);
            labelParagraph.Alignment = Element.ALIGN_CENTER;
            labelParagraph.SpacingAfter = 40f;
            document.Add(labelParagraph);
            
            // Add tour information
            Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Paragraph guestParagraph = new Paragraph($"INFO:", infoFont);
            guestParagraph.SpacingAfter = 5f;
            document.Add(guestParagraph);
            Font basicFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Paragraph guestInfoParagraph = new Paragraph("Name:"+ ChosenTour.Tour.Name  + "     Country:" + ChosenTour.Tour.Location.Country + "     City:" + ChosenTour.Tour.Location.City, basicFont);
            guestInfoParagraph.SpacingAfter = 15f;
            document.Add(guestInfoParagraph);
            Paragraph datePar3 = new Paragraph($"Duration in hours:" + ChosenTour.Tour.DurationInHours, infoFont);
            datePar3.SpacingAfter = 5f;
            document.Add(datePar3);
            Paragraph datePar4 = new Paragraph($"Language:" + ChosenTour.Tour.Language, infoFont);
            datePar4.SpacingAfter = 5f;
            document.Add(datePar4);
            Paragraph datePar5 = new Paragraph($"MaxGuests:" + ChosenTour.Tour.MaxGuests, infoFont);
            datePar5.SpacingAfter = 5f;
            document.Add(datePar5);
            Paragraph datePar = new Paragraph($"Date: "+ ChosenTour.TourTime.StartingDateTime.ToString(), infoFont);
            datePar.SpacingAfter = 5f;
            document.Add(datePar);
            Paragraph datePar2 = new Paragraph($"Description: " + ChosenTour.Tour.Description, infoFont);
            datePar2.SpacingAfter = 5f;
            document.Add(datePar2);

            string imageUrl = ChosenTour.Tour.Images[0].Url; ; // Replace with the actual URL of your image
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(imageUrl);

                // Create an Image object from the downloaded bytes
                Image image = Image.GetInstance(imageBytes);
                if (image != null)
                {
                    image.ScaleToFit(200f, 200f); // Adjust the image size as needed
                    document.Add(image);
                }
            }

            // kp
            Paragraph category1Paragraph = new Paragraph($"All key points:", infoFont);
            category1Paragraph.SpacingAfter = 5f;
            document.Add(category1Paragraph);
            foreach(KeyPoint kp in ChosenTour.Tour.KeyPoints)
            {

                Paragraph KP = new Paragraph($" --- " + kp.Point, infoFont);
                KP.SpacingAfter = 5f;
                document.Add(KP);

            }

            // guests
            Paragraph category2Paragraph = new Paragraph($"All guests:", infoFont);
            List<User> guests = new List<User>(filterGuests(_tourReservationController.GetAll()));
            category2Paragraph.SpacingAfter = 5f;
            document.Add(category2Paragraph);
            foreach (User kp in guests)
            {

                Paragraph KP = new Paragraph($" --- " + kp.Name +"  "+kp.Surname, infoFont);
                KP.SpacingAfter = 5f;
                document.Add(KP);

            }

            document.Close();

            string browserPath = GetDefaultWebBrowserPath();

            // Open the PDF file with the default web browser
            if (!string.IsNullOrEmpty(browserPath))
            {
                Process.Start(new ProcessStartInfo(browserPath, $"file:///{Path.GetFullPath(ChosenTour.Tour.Name + "_report.pdf")}")
                {
                    UseShellExecute = true
                });
            }
        }
        private string GetDefaultWebBrowserPath()
        {
            // Default web browser registry key for Windows
            const string registryKey = @"HTTP\shell\open\command";

            using (Microsoft.Win32.RegistryKey browserKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(registryKey))
            {
                if (browserKey != null)
                {
                    string path = browserKey.GetValue(null) as string;
                    if (!string.IsNullOrEmpty(path))
                    {
                        // Extract the path to the browser executable
                        path = path.Replace("\"", "");
                        if (path.Contains(".exe"))
                        {
                            path = path.Substring(0, path.IndexOf(".exe") + 4);
                        }

                        return path;
                    }
                }
            }
            return string.Empty;
        }

    }
}
