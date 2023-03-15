using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for AddPhotosToAccommodationView.xaml
    /// </summary>
    public partial class AddPhotosToAccommodationView : Window
    {
       // public AccommodationController _accommodationControler;
        //public Accommodation Accommodation { get; set; }
        public AccommodationImageController _imageController;
        //public AccommodationLocationController _locationController;
        //public String Url { get; set; }
        //public AccommodationImage AccommodationImage { get; set; }
        //public ObservableCollection<AccommodationImage> Images { get; set; }


        public AddPhotosToAccommodationView()
        {
            InitializeComponent();
            //Accommodation = accommodation;
            var app = Application.Current as App;
            this.DataContext = this;
            _imageController = app.AccommodationImageController;
            //this._accommodationControler = app.AccommodationController;
            //this._locationController= app.AccommodationLocationController;
            //this._imageController = app.AccommodationImageController;

            //Accommodation = accommodation;
            //List<AccommodationImage> images = new List<AccommodationImage>();

        }
        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            //_accommodationControler.AddImageToAccommodation(Accommodation, AccommodationImage);
            //this.Close();
            AccommodationImage image = new AccommodationImage();
            image.Url = Url;
            _imageController.Create(image);
            _imageController.SaveImage();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
