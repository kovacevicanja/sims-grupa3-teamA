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
using BookingProject.Model.Images;
using System.Text.RegularExpressions;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for EnterImage.xaml
    /// </summary>
    public partial class EnterImage : Window, IDataErrorInfo
    {


        public TourImageController ImageController { get; set; }



        public EnterImage()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            ImageController = app.ImageController;

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
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Url")
                {
                    if (string.IsNullOrEmpty(Url))
                        return "Enter a valid url!";

                }

                return null;
            }
        }


        private readonly string[] _validatedProperties = { "Url" };

        Regex validateUrlRegex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");


        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return validateUrlRegex.IsMatch(Url);
            }
        }

        private void Window_LayoutUpdated(object sender, System.EventArgs e)
        {
            if (IsValid)
                CreateButton.IsEnabled = true;
            else
                CreateButton.IsEnabled = false;
        }


    }

}