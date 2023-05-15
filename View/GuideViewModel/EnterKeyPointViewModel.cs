using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class EnterKeyPointViewModel
    {

        public RelayCommand CancelCommand { get; }
        public RelayCommand CreateCommand { get; }
        public KeyPointController KeyPointController { get; set; }

        public EnterKeyPointViewModel()
        {
            KeyPointController = new KeyPointController();
            CancelCommand = new RelayCommand(CancelButton_Click, CanExecute);
            CreateCommand = new RelayCommand(Button_Click_Kreiraj, CanExecute);
        }
        private bool CanExecute(object param) { return true; }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(EnterKeyPoint)) { window.Close(); }
            }
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
        private void Button_Click_Kreiraj(object param)
        {
            KeyPoint keyPoint = new KeyPoint();
            keyPoint.Point = KeyPoint;
            KeyPointController.Create(keyPoint);

        }

        private void CancelButton_Click(object param)
        {
            CloseWindow();
        }
    }
}

