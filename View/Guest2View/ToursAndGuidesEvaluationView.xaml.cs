using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for ToursAndGuidesEvaluationView.xaml
    /// </summary>
    public partial class ToursAndGuidesEvaluationView : Window
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
        public TourEvaluationImageController TourEvaluationImageController { get; set; }
        public Tour ChosenTour { get; set; }
        public TourReservationController TourReservationController { get; set; }
        public CustomMessageBox CustomMessageBox { get; set; }
        public int GuestId { get; set; }
        public ToursAndGuidesEvaluationView(Tour chosenTour, int guestId)
        {
            InitializeComponent();
            this.DataContext = this;

            GuestId = guestId;

            CustomMessageBox = new CustomMessageBox();

            TourEvaluationController = new TourEvaluationController();
            ChosenTour = chosenTour;

            tourEvaluation = new TourEvaluation();

            Images = new List<TourEvaluationImage>();
            TourEvaluationImageController = new TourEvaluationImageController();

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
        }
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
        private void Button_Click_AddImage (object sender, RoutedEventArgs e)
        {
            TourEvaluationImage TourImage = new TourEvaluationImage();
            TourImage.Url = ImageUrl;
            tourEvaluation.Images.Add(TourImage);
            TourEvaluationImageController.Create(TourImage);

            CustomMessageBox.ShowCustomMessageBox("You have successfully added a picture, if you want you can add more.");
            TourImageTextBox.Clear();
        }
        private void Button_Click_Rate(object sender, RoutedEventArgs e)
        {
            tourEvaluation.GuideKnowledge = ChosenGuideKnowledge;
            tourEvaluation.GuideLanguage = ChosenGuideLanguage;
            tourEvaluation.TourInterestigness = ChosenInterestigness;
            tourEvaluation.AdditionalComment = AdditionalComment;

            tourEvaluation.Tour.Id = ChosenTour.Id;
            tourEvaluation.Guest.Id = GuestId;

            TourEvaluationController.Create(tourEvaluation);
            this.Close();
        }
    }
}
