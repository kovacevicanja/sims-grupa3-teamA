using BookingProject.Controller;
using BookingProject.Model;
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
        public AccommodationController _accommodationControler;
        public Accommodation Accommodation { get; set; }
        public ImageController _imageController;
        public LocationController _locationController;
        public String Url { get; set; }
        public AccommodationImage AccommodationImage { get; set; }
        public ObservableCollection<AccommodationImage> Images { get; set; }


        public AddPhotosToAccommodationView(Accommodation accommodation)
        {
            InitializeComponent();
            Accommodation = accommodation;
            var app = Application.Current as App;
            this.DataContext = this;
            this._accommodationControler = app.AccommodationController;
            this._locationController= app.LocationController;
            this._imageController = app.ImageController;

            Accommodation = accommodation;
            List<AccommodationImage> images = new List<AccommodationImage>();

        }

        public void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            _accommodationControler.AddImageToAccommodation(Accommodation, AccommodationImage);
            this.Close();
        }

    }
}
