using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.View.Guest1View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;
using System.IO;
using BookingProject.View.Guest1View.Tutorials;

namespace BookingProject.View.Guest1ViewModel
{
    public class AccommodationRequestsViewModel
    {
        public ObservableCollection<RequestAccommodationReservation> MyRequests { get; set; }
        public RequestAccommodationReservationController requestAccommodationReservationController;
        public UserController userController;
        public RelayCommand HomePageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand MyProfileCommand { get; }
        public RelayCommand PDFCommand { get; }
        public RelayCommand ViewTutorialCommand { get; }
        public RelayCommand CreateForumCommand { get; }
        public RelayCommand QuickSearchCommand { get; }
        public AccommodationRequestsViewModel()
        {
            requestAccommodationReservationController = new RequestAccommodationReservationController();
            userController = new UserController();
            MyRequests = new ObservableCollection<RequestAccommodationReservation>(requestAccommodationReservationController.GetAllForUser(userController.GetLoggedUser()));
            HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
            PDFCommand = new RelayCommand(Button_Click_GeneratePDF, CanExecute);
            ViewTutorialCommand = new RelayCommand(Button_Click_ViewTutorial, CanExecute);
            CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
            QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AccommodationRequestsView)) { window.Close(); }
            }
        }
        private void Button_Click_Homepage(object param)
        {
            var Guest1Homepage = new Guest1HomepageView();
            Guest1Homepage.Show();
            CloseWindow();
        }

        private void Button_Click_MyReservations(object param)
        {
            var Guest1Reservations = new Guest1Reservations();
            Guest1Reservations.Show();
            CloseWindow();
        }

        private void Button_Click_Logout(object param)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }

        private void Button_Click_MyReviews(object param)
        {
            var reviews = new Guest1ReviewsView();
            reviews.Show();
            CloseWindow();
        }
        private void Button_Click_MyProfile(object param)
        {
            var profile = new Guest1ProfileView();
            profile.Show();
            CloseWindow();
        }
        private void Button_Click_ViewTutorial(object param)
		{
            var tutorial = new AccommodationRequestsTutorialView();
            tutorial.Show();
            CloseWindow();

        }
        private void Button_Click_GeneratePDF(object param)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.DefaultExt = "pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (Document document = new Document())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                        document.Open();

                        Paragraph title = new Paragraph("Accommodation requests");
                        title.Alignment = Element.ALIGN_CENTER;
                        title.Font.Size = 20;
                        document.Add(title);

                        document.Add(new Paragraph("\n"));

                        foreach (var request in MyRequests)
                        {
                            List list = new List(List.UNORDERED);
                            Font boldFont = new Font(Font.FontFamily.SYMBOL, 12, Font.BOLD);
                            list.ListSymbol = new Chunk("○", boldFont);


                            list.Add(new ListItem($"Old arrival day: {request.AccommodationReservation.InitialDate}"));
                            list.Add(new ListItem($"Old departure day: {request.AccommodationReservation.EndDate}"));
                            list.Add(new ListItem($"New arrival day: {request.NewArrivalDay}"));
                            list.Add(new ListItem($"New departure day: {request.NewDeparuteDay}"));
                            list.Add(new ListItem($"Status: {request.Status}"));
                            list.Add(new ListItem($"Guest's comment: {request.GuestComment}"));
                            list.Add(new ListItem($"Owner's comment: {request.OwnerComment}"));

                            document.Add(list);

                            document.Add(new Paragraph("\n"));
                        }


                        document.Close();

						System.Windows.MessageBox.Show("PDF file generated successfully!");
                    }
                }
                catch (Exception ex)
                {
					System.Windows.MessageBox.Show($"Error generating PDF file: {ex.Message}");
                }
            }
        }

        private void Button_Click_CreateForum(object param)
        {
            var forum = new OpenForumView();
            forum.Show();
            CloseWindow();
        }

        private void Button_Click_Quick_Search(object param)
        {
            var quickS = new QuickSearchView();
            quickS.Show();
            CloseWindow();
        }

    }
}
