using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
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
    public partial class EnterKeyPoint : Window, IDataErrorInfo
    {


        public KeyPointController KeyPointController { get; set; }



        public EnterKeyPoint()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            KeyPointController = app.KeyPointController;

        }

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

        private string _keyPoint;

        public string KeyPoint
        {
            get => _keyPoint;
            set
            {
                if (value != _keyPoint)
                {
                    _keyPoint = value;
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
            KeyPoint keyPoint = new KeyPoint();
            keyPoint.Point = KeyPoint;
            KeyPointController.Create(keyPoint);

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
