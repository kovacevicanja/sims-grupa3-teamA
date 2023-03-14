using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.ConversionHelp;
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
    /// Interaction logic for EnterDate.xaml
    /// </summary>
    public partial class EnterDate : Window, IDataErrorInfo
    {


        public StartingDateController StartingDateController { get; set; }

        public DateConversion DateConversion { get; set; }


        public EnterDate()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            StartingDateController= app.StartingDateController;

        }

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();

        private string _startingDate;

        public string StartingDate
        {
            get => _startingDate;
            set
            {
                if (value != _startingDate)
                {
                    _startingDate = value;
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
            StartingDate startingDate = new StartingDate();
            startingDate.StartingTime = DateConversion.StringToDate(StartingDate);
            StartingDateController.Create(startingDate);
            StartingDateController.Save();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
