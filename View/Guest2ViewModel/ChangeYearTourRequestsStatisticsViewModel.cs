﻿using BookingProject.Commands;
using BookingProject.View.Guest2View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest2ViewModel
{
    public class ChangeYearTourRequestsStatisticsViewModel : INotifyPropertyChanged
    {
        public int GuestId { get; set; }
        public RelayCommand ChangeYearCommand { get; }
        public RelayCommand CancelCommand { get; }

        public ChangeYearTourRequestsStatisticsViewModel(int guestId)
        {
            GuestId = guestId;

            ChangeYearCommand = new RelayCommand(Button_Click_ChangeYear, CanExecute);
            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
        }

        private bool CanExecute(object param) { return true; }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ChangeYearTourRequestsStatisticsView)) { window.Close(); }
            }
        }

        public void Button_Click_ChangeYear(object param)
        {
            TourRequestStatisticsView tourRequestStatisticsView = new TourRequestStatisticsView(GuestId, EnteredYear);
            tourRequestStatisticsView.Show();

            CloseWindow();
        }

        public void Button_Click_Cancel(object param)
        {
            CloseWindow();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string _enteredYear;
        public string EnteredYear
        {
            get => _enteredYear;
            set
            {
                if (value != _enteredYear)
                {
                    _enteredYear = value;
                    OnPropertyChanged(nameof(EnteredYear));
                }
            }
        }
    }
}