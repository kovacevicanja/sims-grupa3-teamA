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
using System.Text.RegularExpressions;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for EnterDate.xaml
    /// </summary>
    public partial class EnterDate : Window, IDataErrorInfo
    {


        public TourStartingTimeController StartingDateController { get; set; }

        public DateConversion DateConversion { get; set; }


        public EnterDate()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            StartingDateController = app.StartingDateController;

        }

        // public string this[string columnName] => throw new NotImplementedException();

        // public string Error => throw new NotImplementedException();

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
            TourDateTime startingDate = new TourDateTime();
            startingDate.StartingDateTime = DateConversion.StringToDateTour(StartingDate);
            StartingDateController.Create(startingDate);
            StartingDateController.Save();

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
                if (columnName == "StartingDate")
                {
                    if (!(DateTime.TryParse(StartingDate, out DateTime result)) || (StartingDate.Length != 19))
                        return "Format dd/mm/yyyy hh:mm:ss";

                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "StartingDate" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                if (DateTime.TryParse(StartingDate, out DateTime result) && StartingDate.Length == 19)
                {
                    return true;
                }

                return false;
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
