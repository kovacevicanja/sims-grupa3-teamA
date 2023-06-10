using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain.Enums;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;
using BookingProject.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace BookingProject.View.Guest2ViewModel
{
    public class SecondGuestMyVouchersViewModel : INotifyPropertyChanged
    {
        public VoucherController VoucherController { get; set; }
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public List<Voucher> _vouchersList { get; set; }
        public Voucher ChosenVoucher { get; set; }
        public TourReservationController TourReservationController { get; set; }
        public Tour ChosenTour { get; set; }
        public CustomMessageBox CustomMessageBox { get; set; }
        public RelayCommand UseVoucherCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand ProfileCommand { get; }
        public RelayCommand GeneratePdfReport { get; }
        public int GuestId { get; set; }
        public NavigationService NavigationService { get; set; }
        public string NumberOfDays { get; set; }
        public User Guest { get; set; }
        public UserController UserController { get; set; }
        public TourPresenceController TourPresenceController { get; set; }
        public VoucherRepository voucherRepository { get; set; }
        public List<Voucher> GuestsVouchers { get; set; }
        public SecondGuestMyVouchersViewModel(int guestId, int chosenTourId, NavigationService navigationService)
        {
            GuestId = guestId;

            UserController = new UserController();
            Guest = UserController.GetById(GuestId);
            TourPresenceController = new TourPresenceController();

            int vonVouchersMaxNum = TourPresenceController.FindAttendedTours(Guest).Count() / 5;
            int vonVouchersNum = 0;
            int needToAdd = 0;

            VoucherController = new VoucherController();
            GuestsVouchers = VoucherController.GetAllGuestsVouchers(guestId);

            if (vonVouchersMaxNum >= 1)
            {
                foreach (Voucher voucher in GuestsVouchers)
                {
                    if (voucher.IsAward == true)
                    {
                        vonVouchersNum++;
                    }
                }

                if (vonVouchersNum < vonVouchersMaxNum)
                {
                    needToAdd = vonVouchersMaxNum - vonVouchersNum;
                }

                for (int i = 0; i < needToAdd; i++)
                {
                    VoucherController.CreatePrizeVoucher(Guest);
                }
            }

            Vouchers = new ObservableCollection<Voucher>(VoucherController.GetUserVouhers(guestId));
            CustomMessageBox = new CustomMessageBox();

            NavigationService = navigationService;

            ChosenTour = new Tour();
            ChosenTour.Id = chosenTourId;
            TourReservationController = new TourReservationController();

            VoucherController.DeleteExpiredVouchers();

            _vouchersList = new List<Voucher>();

            UseVoucherCommand = new RelayCommand(Button_UseVoucher, CanUse);
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
            ProfileCommand = new RelayCommand(Button_BackToProfile, CanExecute);
            GeneratePdfReport = new RelayCommand(Button_GeneratePdfReport, CanExecute);
        }

        private bool CanExecute(object param) { return true; }

        private bool CanUse(object param)
        {
            if (ChosenVoucher != null) { return true; }
            else { return false; }
        }

        private void Button_BackToProfile(object param)
        {
            NavigationService.GoBack();
        }

        private void Button_GeneratePdfReport(object param)
        {
            Document document = new Document();

            string currentDirectory = Directory.GetCurrentDirectory();

            string outputFilePath = @"C:\Users\sveto\Desktop\SIMS_PROJEKAT\sims-grupa3-teamA\PDFs\Guest2PDF\ValidVouchersReport.pdf";

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputFilePath, FileMode.Create));

            document.Open();

            Paragraph title = new Paragraph("Currently Valid Vouchers", new Font(Font.FontFamily.HELVETICA, 25f, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20f; 
            document.Add(title);

            PdfPTable table = new PdfPTable(2); 
            table.WidthPercentage = 100f;
            table.DefaultCell.BorderWidth = 0.5f; 
            table.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY; 
            table.HeaderRows = 1; 

            PdfPCell headerCell1 = new PdfPCell(new Phrase("Validity Start Date", new Font(Font.FontFamily.HELVETICA, 18f, Font.BOLD)));
            headerCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell headerCell2 = new PdfPCell(new Phrase("Validity End Date", new Font(Font.FontFamily.HELVETICA, 18f, Font.BOLD)));
            headerCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(headerCell1);
            table.AddCell(headerCell2);

            foreach (var voucher in Vouchers)
            {
                string startDate = voucher.StartDate.ToShortDateString();
                string startTime = voucher.StartDate.ToString("HH:00:00");
                string endDate = voucher.EndDate.ToShortDateString();
                string endTime = voucher.EndDate.ToString("HH:00:00");

                string startDateWithSpace = startDate + "  " + startTime;
                string endDateWithSpace = endDate + "  " + endTime;

                PdfPCell cell1 = new PdfPCell(new Phrase(startDateWithSpace, new Font(Font.FontFamily.HELVETICA, 18f)));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell2 = new PdfPCell(new Phrase(endDateWithSpace, new Font(Font.FontFamily.HELVETICA, 18f)));
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell1);
                table.AddCell(cell2);
            }

            document.Add(table);

            document.Close();

            CustomMessageBox.ShowCustomMessageBox("You have successfully created a pdf report on currently valid vouchers.");
        }

        private void Button_UseVoucher(object param)
        {
            _vouchersList = VoucherController.GetAll();
            if (ChosenTour.Id == -1)
            {
                CustomMessageBox.ShowCustomMessageBox("You cannot use the voucher, you have not selected any tour for booking.");
            }
            else
            {
                CustomMessageBox.ShowCustomMessageBox("You have successfully used your voucher to book this tour.");
                ChosenVoucher.State = VoucherState.USED;
                ChosenVoucher.Tour = ChosenTour;
                VoucherController.Save(_vouchersList);

                NavigationService.Navigate(new SerachAndReservationToursView(GuestId, NavigationService));
            }
        }
        private void Button_Cancel(object param)
        {
            NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private VoucherState _voucherState;
        public VoucherState VoucherState
        {
            get => _voucherState;
            set
            {
                if (value != _voucherState)
                {
                    _voucherState = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}