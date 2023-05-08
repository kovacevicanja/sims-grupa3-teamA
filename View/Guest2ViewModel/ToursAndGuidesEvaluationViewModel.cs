using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model.Images;
using BookingProject.Model;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Web.WebPages;
using System.Windows.Media.Animation;

namespace BookingProject.View.Guest2ViewModel
{
    public class ToursAndGuidesEvaluationViewModel : INotifyPropertyChanged
    {
        public TourEvaluationController TourEvaluationController { get; set; }
        public ObservableCollection<int> GuideKnowledgeOption { get; set; }
        public int ChosenGuideKnowledge { get; set; }
        public ObservableCollection<int> GuideLanguageOption { get; set; }
        public int ChosenGuideLanguage { get; set; }
        public ObservableCollection<int> TourInterestignessOption { get; set; }
        public int ChosenInterestigness { get; set; }
        public TourEvaluation tourEvaluation { get; set; }
        public List<TourEvaluationImage> Images { get; set; }
        public ITourEvaluationRepository TourEvaluationRepository { get; set; }
        public TourEvaluationImageController TourEvaluationImageController { get; set; }
        public Tour ChosenTour { get; set; }
        public TourReservationController TourReservationController { get; set; }
        public CustomMessageBox CustomMessageBox { get; set; }
        public int GuestId { get; set; }
        public RelayCommand AddImageCommand { get; }
        public RelayCommand RateCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public ToursAndGuidesEvaluationViewModel(Tour chosenTour, int guestId)
        {
            GuestId = guestId;

            CustomMessageBox = new CustomMessageBox();

            TourEvaluationController = new TourEvaluationController();
            ChosenTour = chosenTour;

            tourEvaluation = new TourEvaluation();

            Images = new List<TourEvaluationImage>();
            TourEvaluationImageController = new TourEvaluationImageController();

            TourEvaluationRepository = new TourEvaluationRepository();
            tourEvaluation.Id = Injector.CreateInstance<ITourEvaluationRepository>().GenerateId();

            GuideKnowledgeOption = new ObservableCollection<int>();
            GuideLanguageOption = new ObservableCollection<int>();
            TourInterestignessOption = new ObservableCollection<int>();

            TourReservationController = new TourReservationController();

            for (int i = 1; i <= 5; i++)
            {
                GuideKnowledgeOption.Add(i);
                GuideLanguageOption.Add(i);
                TourInterestignessOption.Add(i);
            }

            AddImageCommand = new RelayCommand(Button_Click_AddImage, CanExecute);
            RateCommand = new RelayCommand(Button_Click_Rate, CanExecute);
            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            LogOutCommand = new RelayCommand(Button_LogOut, CanExecute);
        }

        private bool CanExecute(object param) { return true; }

        private int _guideKnowledge;
        public int GuideKnowledge
        {
            get => _guideKnowledge;
            set
            {
                if (value != _guideKnowledge)
                {
                    _guideKnowledge = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _guideLanguage;
        public int GuideLanguage
        {
            get => _guideLanguage;
            set
            {
                if (value != _guideLanguage)
                {
                    _guideLanguage = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _tourInterestigness;
        public int TourInterestigness
        {
            get => _tourInterestigness;
            set
            {
                if (value != _tourInterestigness)
                {
                    _tourInterestigness = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _additionalComment;
        public string AdditionalComment
        {
            get => _additionalComment;
            set
            {
                if (value != _additionalComment)
                {
                    _additionalComment = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_AddImage(object param)
        {
            TourEvaluationImage TourImage = new TourEvaluationImage();
            TourImage.Url = ImageUrl;

            if (TourImage.Url.IsEmpty())
            {
                CustomMessageBox.ShowCustomMessageBox("You cannot add an image if you have not entered a url.");
            }
            else
            {
                TourImage.TourEvaluation.Id = tourEvaluation.Id;
                tourEvaluation.Images.Add(TourImage);

                TourEvaluationImageController.Create(TourImage);

                CustomMessageBox.ShowCustomMessageBox("You have successfully added a picture, if you want you can add more.");
                ImageUrl = "";
            }
        }
        private void Button_Click_Rate(object param)
        {
            tourEvaluation.GuideKnowledge = ChosenGuideKnowledge;
            tourEvaluation.GuideLanguage = ChosenGuideLanguage;
            tourEvaluation.TourInterestigness = ChosenInterestigness;
            tourEvaluation.AdditionalComment = AdditionalComment;

            tourEvaluation.Tour.Id = ChosenTour.Id;
            tourEvaluation.Guest.Id = GuestId;

            if (tourEvaluation.GuideKnowledge == 0 || tourEvaluation.GuideLanguage == 0 || tourEvaluation.TourInterestigness == 0)
            {
                CustomMessageBox.ShowCustomMessageBox("Your rating cannot be accepted. You have left some rating fields empty.");
            }
            else
            {
                TourEvaluationController.Create(tourEvaluation);

                CustomMessageBox.ShowCustomMessageBox("We appreciate your opinion. Thank you for helping us improve.");

                SecondGuestProfileView profile = new SecondGuestProfileView(GuestId);
                profile.Show();
                CloseWindow();
            }
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ToursAndGuidesEvaluationView)) { window.Close(); }
            }
        }
        private void Button_Click_Cancel(object param)
        {
            TourEvaluationImageController.DeleteImageIfEvaluationNotCreated(tourEvaluation.Id);
            SecondGuestProfileView profile = new SecondGuestProfileView(GuestId);
            profile.Show();
            CloseWindow();
        }

        private void Button_LogOut(object param)
        {
            SignInForm logOut = new SignInForm();
            logOut.Show();
            CloseWindow();
        }
    }
}