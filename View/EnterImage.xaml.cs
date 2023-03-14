using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model;
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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for EnterImage.xaml
    /// </summary>
    public partial class EnterImage : Window, IDataErrorInfo
    {


        public ImageController ImageController { get; set; }



        public EnterImage()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            ImageController = app.ImageController;

        }

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

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


        private void Button_Click_Kreiraj(object sender, RoutedEventArgs e)
        {
            TourImage image = new TourImage();
            image.Url = Url;
            ImageController.Create(image);
            ImageController.Save();


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
